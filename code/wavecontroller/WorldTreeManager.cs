using Editor;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyGame
{
    public class WorldManager
    {
        private const int QuadTreeSize = 4096;
        private const int NumQuadTreesPerRow = 12;  

        public List<QuadTree> QuadTrees { get; } = new List<QuadTree>();

        private QuadTree previousQuadTree;
        private QuadTree playerQuadTree;

        private int childModelType;

        //private QuadTree playerQuadTree;

        public WorldManager(Pawn pawn)
        {
            int totalSize = QuadTreeSize * NumQuadTreesPerRow;
            int offset = totalSize / 2 - QuadTreeSize / 2;

            int quadTreeId = 0;
            for (int i = 0; i < NumQuadTreesPerRow; i++)
            {
                for (int j = 0; j < NumQuadTreesPerRow; j++)
                {
                    var worldBounds = new BBox(
                        new Vector3(i * QuadTreeSize - offset, j * QuadTreeSize - offset, -QuadTreeSize),
                        new Vector3((i + 1) * QuadTreeSize - offset, (j + 1) * QuadTreeSize - offset, QuadTreeSize)
                    );
                    var quadTree = new QuadTree(pawn, worldBounds, 3, quadTreeId++);
                    QuadTrees.Add(quadTree);
                }
            }
            
            foreach (var quadTree in QuadTrees)
            {
                quadTree.InitializeNeighbors(QuadTrees);
                Log.Info("QuadTree " + quadTree.Id + " has " + quadTree.Neighbors.Length + " neighbors");
            }
        }

        public void UpdatePlayerPosition(Vector3 cameraPosition)
        {
            QuadTree playerQuadTree = null;
            foreach (var quadTree in QuadTrees)
            {
                if (quadTree.IsPositionInQuadrant(quadTree.Bounds, cameraPosition))
                {
                    playerQuadTree = quadTree;
                    break;
                }
            }

            if (playerQuadTree == null)
                return;

            QuadTreeNode playerNode = playerQuadTree.GetNodeForPlayer(playerQuadTree.rootTile, cameraPosition);

            if (playerNode == null)
                return;


            float lodThreshold = QuadTreeSize * (float)Math.Sqrt(2);  
            lodThreshold *= 1;  

            foreach (var quadTree in QuadTrees)
            {
                quadTree.UpdateLODBasedOnDistance(cameraPosition, playerNode, lodThreshold);
                bool isPlayerFullyInside = quadTree.IsPlayerFullyInside(cameraPosition);
                if(isPlayerFullyInside || !playerNode.IsBorderNode)
                {
                    quadTree.SpawnHandlersForLeafNodes(playerQuadTree.rootTile, playerQuadTree, cameraPosition);
                    if(playerQuadTree != quadTree)
                    {
                        quadTree.SpawnHandlersForLargeLOD(quadTree.rootTile, playerQuadTree, cameraPosition);
                        if (quadTree.rootTile.Handler != null)
                        {
                            quadTree.SmiteModels(quadTree.rootTile);
                        }
                    }
                }



                if(playerNode.IsBorderNode)
                {
                    quadTree.UpdateLargeLOD(cameraPosition, QuadTrees, playerNode, playerQuadTree);
                }
            }
        }
    }
}
