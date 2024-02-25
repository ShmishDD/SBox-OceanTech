using Editor;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace MyGame
{
    public class QuadTreeNode
    {
    
        public BBox Bounds { get; set; }
        public int Level { get; set; }
        public QuadTreeNode[] Children { get; set; }
        public QuadTree ParentTree { get; set; }
        public MeshHandler Handler { get; set; }
        public bool IsBorderNode { get; set; }
        public List<QuadTreeNode> AdjacentNodes { get; set; }
        private static int nextId = 0;
        public int Id { get; private set; }

        

        public QuadTreeNode(BBox bounds, int level, QuadTree parentTree, bool isBorderNode = false)
        {
            Bounds = bounds;
            Level = level;
            IsBorderNode = isBorderNode;
            AdjacentNodes = new List<QuadTreeNode>();
            this.Id = nextId++;
            this.ParentTree = parentTree;
        }

        public void SpawnHandlerIfLeaf()
        {
            if ((Children == null || Children.Length == 0) && Handler == null)
            {
                Handler = new MeshHandler();
                Handler.Spawn();
                Handler.Position = Bounds.Center + new Vector3(0,0,0.1f);
                //Log.Info(Handler.SceneObject);
                //Handler.Position = Bounds.Center;
            }
        }

        public void SpawnHandlerIfLargeLOD()
        {
            if (Handler == null && Level == 0)
            {
                Handler = new MeshHandler();
                Handler.Position = Bounds.Center;
                Handler.Spawn();
                Handler.UpdateLOD(LODModelType.LOD_Large);
            }
        }

        public void Smite()
        {
            if (Handler != null)
            {
                Handler.Smite();
                Handler = null;
            }
        }
    }

    public class QuadTree
    {
        public QuadTreeNode rootTile;
        private Pawn Owner;
        public BBox Bounds { get; private set; }
        private int Depth;
        private Vector3 playerPosition;
        private QuadTreeNode playerNode;
        private int childModelType;
        public bool ContainsPlayer { get; set; } = false;
        private List<QuadTreeNode> nodesInRange = new List<QuadTreeNode>();
        public QuadTree[] Neighbors { get; set; }
        private List<QuadTreeNode> borderNodes = new List<QuadTreeNode>();
        public const int lodRange = 2048;
        public int Id { get; }

        public QuadTree(Pawn owner, BBox bounds, int depth, int id)
        {
            this.Owner = owner;
            this.Bounds = bounds;
            this.Depth = depth;
            this.Id = id;

            rootTile = new QuadTreeNode(Bounds, 0, this); 
            Initialize();
        }

        private BBox CalculateChildBounds(BBox parentBounds, int quadrant)
        {
            float halfWidth = parentBounds.Size.x * 0.5f;
            float halfHeight = parentBounds.Size.y * 0.5f;

            Vector3 center = parentBounds.Center;

            switch (quadrant)
            {
                case 0: 
                    return new BBox(center + new Vector3(-halfWidth, halfHeight, 0), center);
                case 1: 
                    return new BBox(center, center + new Vector3(halfWidth, halfHeight, 0));
                case 2: 
                    return new BBox(center + new Vector3(-halfWidth, 0, 0), center + new Vector3(0, -halfHeight, 0));
                case 3: 
                    return new BBox(center + new Vector3(0, 0, 0), center + new Vector3(halfWidth, -halfHeight, 0));
                default:
                    throw new ArgumentException("Invalid quadrant value.");
            }
        }

        public void UpdateLODBasedOnDistance(Vector3 playerPosition, QuadTreeNode playerNode, float lodThreshold)
        {
            List<QuadTreeNode> leafNodes = GetAllLeafNodes(rootTile);

            foreach (QuadTreeNode leaf in leafNodes)
            {
                float distance = Vector3.DistanceBetween(playerPosition, leaf.Bounds.Center);
                if (distance > lodThreshold && leaf.Handler != null)
                {
                    leaf.Handler.UpdateLOD(LODModelType.LOD);
                }
                else
                {
                    UpdateLOD(leaf, playerNode);
                }
            }
        }

        public void UpdateLargeLOD(Vector3 playerPosition, List<QuadTree> quadTrees, QuadTreeNode playerNode, QuadTree playerQuadTree)
        {
            float distanceThreshold = 4100.0f;

            foreach (QuadTree quadTree in quadTrees)
            {
                bool isPlayerQuadTree = (quadTree == playerQuadTree);

                float distanceToQuadTree = Vector3.DistanceBetween(playerPosition, quadTree.Bounds.Center);

                if (distanceToQuadTree > distanceThreshold)
                {
                    continue;
                }

                if(quadTree.rootTile.Handler != null && quadTree.rootTile.Handler.GetCurrentModelType() == 10)
                {
                    quadTree.rootTile.Smite();
                }
                quadTree.SpawnHandlersForLeafNodes(quadTree.rootTile, playerQuadTree, playerPosition);
            }
        }

        public bool IsPlayerFullyInside(Vector3 playerPosition)
        {
            float shrinkedQuadTreeSize = 4096 - 512f;

            Vector3 shrinkedQuadTreeCenter = Bounds.Center;

            shrinkedQuadTreeCenter.x -= 256f;
            shrinkedQuadTreeCenter.y -= 256f;

            BBox shrinkedBounds = new BBox(
                shrinkedQuadTreeCenter - new Vector3(shrinkedQuadTreeSize / 2f, shrinkedQuadTreeSize / 2f, 0f),
                shrinkedQuadTreeCenter + new Vector3(shrinkedQuadTreeSize / 2f, shrinkedQuadTreeSize / 2f, 0f)
            );

            return shrinkedBounds.Contains(playerPosition);
        }
    

        public List<QuadTreeNode> GetAllLeafNodes(QuadTreeNode node)
        {
            List<QuadTreeNode> leafNodes = new List<QuadTreeNode>();

            if (node == null)
            {
                return leafNodes;
            }

            if (node.Children != null && node.Children.Length > 0)
            {
                foreach (QuadTreeNode child in node.Children)
                {
                    leafNodes.AddRange(GetAllLeafNodes(child));
                }
            }
            else
            {
                leafNodes.Add(node);
            }

            return leafNodes;
        }

        public void InitializeNeighbors(List<QuadTree> allQuadTrees)
        {
            Neighbors = new QuadTree[8];  // 8 possible neighbors (top-left, top, top-right, right, bottom-right, bottom, bottom-left, left)

            foreach (var quadTree in allQuadTrees)
            {
                if (quadTree == this) continue;  // Skip self

                if (quadTree.Bounds.Maxs.x < this.Bounds.Mins.x && quadTree.Bounds.Maxs.y < this.Bounds.Mins.y)
                {
                    Neighbors[0] = quadTree;
                }
                else if (quadTree.Bounds.Mins.x <= this.Bounds.Maxs.x && quadTree.Bounds.Maxs.x >= this.Bounds.Mins.x && quadTree.Bounds.Maxs.y < this.Bounds.Mins.y)
                {
                    Neighbors[1] = quadTree;
                }
                else if (quadTree.Bounds.Mins.x > this.Bounds.Maxs.x && quadTree.Bounds.Maxs.y < this.Bounds.Mins.y)
                {
                    Neighbors[2] = quadTree;
                }
                else if (quadTree.Bounds.Mins.x > this.Bounds.Maxs.x && quadTree.Bounds.Mins.y <= this.Bounds.Maxs.y && quadTree.Bounds.Maxs.y >= this.Bounds.Mins.y)
                {
                    Neighbors[3] = quadTree;
                }
                else if (quadTree.Bounds.Mins.x > this.Bounds.Maxs.x && quadTree.Bounds.Mins.y > this.Bounds.Maxs.y)
                {
                    Neighbors[4] = quadTree;
                }
                else if (quadTree.Bounds.Mins.x <= this.Bounds.Maxs.x && quadTree.Bounds.Maxs.x >= this.Bounds.Mins.x && quadTree.Bounds.Mins.y > this.Bounds.Maxs.y)
                {
                    Neighbors[5] = quadTree;
                }
                else if (quadTree.Bounds.Maxs.x < this.Bounds.Mins.x && quadTree.Bounds.Mins.y > this.Bounds.Maxs.y)
                {
                    Neighbors[6] = quadTree;
                }
                else if (quadTree.Bounds.Maxs.x < this.Bounds.Mins.x && quadTree.Bounds.Mins.y <= this.Bounds.Maxs.y && quadTree.Bounds.Maxs.y >= this.Bounds.Mins.y)
                {
                    Neighbors[7] = quadTree;
                }
            }
        }

        private void UpdateLOD(QuadTreeNode node, QuadTreeNode playerNode, bool updateNeighbors = true)
        {
            if (node == null || node.Handler == null || node.Children != null && node.Children.Length > 0)
            {
                return;
            }

            Vector3 nodePos = node.Bounds.Center;
            Vector3 playerPos = playerNode.Bounds.Center;
            Vector3 playerSize = playerNode.Bounds.Size;

            bool isBeyondNeighboringNodes = Math.Abs(nodePos.x - playerPos.x) > playerSize.x || Math.Abs(nodePos.y - playerPos.y) > playerSize.y;

            if (node == playerNode)
            {
                node.Handler.UpdateLOD(LODModelType.MiddleTile);
            }
            else if (isBeyondNeighboringNodes)
            {
                node.Handler.UpdateLOD(LODModelType.LOD);
            }
            else
            {
                bool isCornerNode = Math.Abs(nodePos.x - playerPos.x) == playerSize.x && Math.Abs(nodePos.y - playerPos.y) == playerSize.y;
                if (isCornerNode)
                {
                    if (nodePos.x < playerPos.x && nodePos.y > playerPos.y)
                    {
                        node.Handler.UpdateLOD(LODModelType.TopLeft_CornerTile);
                    }
                    else if (nodePos.x > playerPos.x && nodePos.y > playerPos.y)
                    {
                        node.Handler.UpdateLOD(LODModelType.TopRight_CornerTile);
                    }
                    else if (nodePos.x < playerPos.x && nodePos.y < playerPos.y)
                    {
                        node.Handler.UpdateLOD(LODModelType.BottomLeft_CornerTile);
                    }
                    else if (nodePos.x > playerPos.x && nodePos.y < playerPos.y)
                    {
                        node.Handler.UpdateLOD(LODModelType.BottomRight_CornerTile);
                    }
                }
                else
                {
                    bool isEdgeNode = Math.Abs(nodePos.x - playerPos.x) == playerSize.x || Math.Abs(nodePos.y - playerPos.y) == playerSize.y;
                    if (isEdgeNode)
                    {
                        if (nodePos.x < playerPos.x)
                        {
                            node.Handler.UpdateLOD(LODModelType.Left_EdgeTile);
                        }
                        else if (nodePos.x > playerPos.x)
                        {
                            node.Handler.UpdateLOD(LODModelType.Right_EdgeTile);
                        }
                        else if (nodePos.y > playerPos.y)
                        {
                            node.Handler.UpdateLOD(LODModelType.Top_EdgeTile);
                        }
                        else if (nodePos.y < playerPos.y)
                        {
                            node.Handler.UpdateLOD(LODModelType.Bottom_EdgeTile);
                        }
                    }
                }
            }
        }

        public QuadTreeNode GetNodeForPlayer(QuadTreeNode node, Vector3 playerPosition)
        {
            if (IsPositionInQuadrant(node.Bounds, playerPosition))
            {
                if (node.Children != null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (node.Children[i] != null)
                        {
                            var result = GetNodeForPlayer(node.Children[i], playerPosition);
                            if (result != null)
                            {
                                return result;
                            }
                        }
                    }
                }
                else
                {
                    return node;
                }
            }

            return null;
        }

        public bool IsPositionInQuadrant(BBox quadrantBounds, Vector3 position)
        {
            return position.x >= quadrantBounds.Mins.x && position.x <= quadrantBounds.Maxs.x &&
                position.y >= quadrantBounds.Mins.y && position.y <= quadrantBounds.Maxs.y;
        }

        private void Subdivide(QuadTreeNode node, int depth)
        {
            if (depth <= 0)
                return;

            SubdivideQuadTree(node);

            for (int i = 0; i < 4; i++)
            {
                if (node.Children[i] != null)
                {
                    Subdivide(node.Children[i], depth - 1);
                }
            }
        }

        public List<QuadTreeNode> GetBorderNodes(QuadTreeNode node)
        {
            List<QuadTreeNode> borderNodes = new List<QuadTreeNode>();

            if (node == null)
            {
                return borderNodes;
            }

            if (node.IsBorderNode)
            {
                borderNodes.Add(node);
            }

            if (node.Children != null && node.Children.Length > 0)
            {
                foreach (QuadTreeNode child in node.Children)
                {
                    borderNodes.AddRange(GetBorderNodes(child));
                }
            }

            return borderNodes;
        }

        private void SubdivideQuadTree(QuadTreeNode node)
        {
            if (node.Children == null)
            {
                node.Children = new QuadTreeNode[4];
                for (int i = 0; i < 4; i++)
                {
                    var childBounds = CalculateChildBounds(node.Bounds, i);
                    node.Children[i] = new QuadTreeNode(childBounds, node.Level + 1, this,  false);

                    node.Children[i].IsBorderNode = IsBorderNode(node.Children[i], rootTile);

                    if (node.IsBorderNode)
                    {
                        
                    }

                    if (node.Children[i].IsBorderNode)
                    {
                        borderNodes.Add(node.Children[i]);
                    }
                }
            }
            else
            {
                node.Handler = null;
            }
        }

        bool IsBorderNode(QuadTreeNode node, QuadTreeNode rootNode)
        {
            return node.Bounds.Mins.x == rootNode.Bounds.Mins.x ||
                node.Bounds.Mins.y == rootNode.Bounds.Mins.y ||
                node.Bounds.Maxs.x == rootNode.Bounds.Maxs.x ||
                node.Bounds.Maxs.y == rootNode.Bounds.Maxs.y;
        }

        public void UpdateLODForAllNodes(QuadTreeNode playerNode, bool updateNeighbors)
        {
            List<QuadTreeNode> leafNodes = GetAllLeafNodes(rootTile);

            foreach (QuadTreeNode leaf in leafNodes)
            {
                UpdateLOD(leaf, playerNode, updateNeighbors);
            }
        }

        public void Initialize()
        {
            Subdivide(rootTile, Depth);
        }

        public void SpawnHandlersForLeafNodes(QuadTreeNode node, QuadTree playerQuadTree, Vector3 playerPosition)
        {
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    SpawnHandlersForLeafNodes(child, playerQuadTree, playerPosition);
                }
            }
            // if(node.Level == 0)
            // {
            //     node.SmiteLODs();
            // }
            node.SpawnHandlerIfLeaf();
        }
        public void SpawnHandlersForLargeLOD(QuadTreeNode node, QuadTree playerQuadTree, Vector3 playerPosition)
        {
            if (node.Children != null && !playerQuadTree.IsPlayerFullyInside(playerPosition))
            {
                node.SpawnHandlerIfLargeLOD();
            }
        }
        public void SmiteModels(QuadTreeNode node)
        {
            //Log.Info("Smite Models");
            if (node.Children != null)
            {
                //Log.Info("Children: " + node.Children.Length);
                foreach (QuadTreeNode child in node.Children)
                {
                    if(child.Handler != null)
                    {
                        childModelType = child.Handler.GetCurrentModelType();
                    }
                    if(child.Handler != null && childModelType == 9)
                    {
                        child.Smite();
                    }
                    SmiteModels(child);
                }
            }
        }
    }
}