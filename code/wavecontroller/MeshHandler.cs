using Editor;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyGame
{
    public enum LODModelType
    {
        MiddleTile,
        Left_EdgeTile,
        Right_EdgeTile,
        Top_EdgeTile,
        Bottom_EdgeTile,
        TopLeft_CornerTile,
        TopRight_CornerTile,
        BottomLeft_CornerTile,
        BottomRight_CornerTile,
        LOD,
        LOD_Large
    }

    [Library("ocean_tile"), HammerEntity]
    [Title("Ocean Tile"), EditorModel("models/watertess_LOD.vmdl")]
    [Category("Ocean Sim")]
    public partial class MeshHandler : Prop
    {
        private static readonly Model[] lodModels = new Model[11];
        public LODModelType CurrentModelType { get; private set; }

        public Model MiddleTileModel { get; private set; } = Model.Load("models/watertess_middle.vmdl");
        public Model Left_EdgeTileModel { get; private set; } = Model.Load("models/watertess_left_edge.vmdl");
        public Model Right_EdgeTileModel { get; private set; } = Model.Load("models/watertess_right_edge.vmdl");
        public Model Top_EdgeTileModel { get; private set; } = Model.Load("models/watertess_top_edge.vmdl");
        public Model Bottom_EdgeTileModel { get; private set; } = Model.Load("models/watertess_bottom_edge.vmdl");
        public Model TopLeft_CornerTileModel { get; private set; } = Model.Load("models/watertess_top_leftcorner.vmdl");
        public Model TopRight_CornerTileModel { get; private set; } = Model.Load("models/watertess_top_rightcorner.vmdl");
        public Model BottomLeft_CornerTileModel { get; private set; } = Model.Load("models/watertess_bottom_leftcorner.vmdl");
        public Model BottomRight_CornerTileModel { get; private set; } = Model.Load("models/watertess_bottom_rightcorner.vmdl");
        public Model LODModel { get; private set; } = Model.Load("models/watertess_LOD.vmdl");
        public Model LODModel_Large { get; private set; } = Model.Load("models/watertess_LOD_large.vmdl");

  

        public override void Spawn()
        {
            base.Spawn();

            lodModels[(int)LODModelType.MiddleTile] = MiddleTileModel;
            lodModels[(int)LODModelType.Left_EdgeTile] = Left_EdgeTileModel;
            lodModels[(int)LODModelType.Right_EdgeTile] = Right_EdgeTileModel;
            lodModels[(int)LODModelType.Top_EdgeTile] = Top_EdgeTileModel;
            lodModels[(int)LODModelType.Bottom_EdgeTile] = Bottom_EdgeTileModel;
            lodModels[(int)LODModelType.TopLeft_CornerTile] = TopLeft_CornerTileModel;
            lodModels[(int)LODModelType.TopRight_CornerTile] = TopRight_CornerTileModel;
            lodModels[(int)LODModelType.BottomLeft_CornerTile] = BottomLeft_CornerTileModel;
            lodModels[(int)LODModelType.BottomRight_CornerTile] = BottomRight_CornerTileModel;
            lodModels[(int)LODModelType.LOD] = LODModel;
            lodModels[(int)LODModelType.LOD_Large] = LODModel_Large;

            Model = LODModel;
            EnableTouch = false;
            EnableAllCollisions = false;
            EnableSelfCollisions = false;
            EnableSolidCollisions = false;
            EnableShadowCasting = true;
            EnableTraceAndQueries = true;

            //SceneObject.ClipPlaneEnabled = false;

            Position = new Vector3(Position.x, Position.y, 0);
        }

        public void UpdateLOD(LODModelType modelType)
        {
            int index = (int)modelType;
            if (index >= 0 && index < lodModels.Length)
            {
                Model = lodModels[index];
                SetCurrentModelType(index);
            }
            else
            {
                Model = lodModels[(int)LODModelType.LOD];
                SetCurrentModelType((int)LODModelType.LOD);
            }
        }

        private void SetCurrentModelType(int index)
        {
            CurrentModelType = (LODModelType)index;
        }

        public int GetCurrentModelType()
        {
            return (int)CurrentModelType;
        }

        public void Smite()
        {
            Delete();
        }
        [GameEvent.Tick.Client]
        private void Tick()
        {
            SceneObject.Attributes.Set("OneDirection", WaveConfig.WaveOne.Direction);
            SceneObject.Attributes.Set("OneAmplitude", WaveConfig.WaveOne.Amplitude);
            //SceneObject.Attributes.Set("OneAmplitude", 8f);
            SceneObject.Attributes.Set("OneSteepness", WaveConfig.WaveOne.Steepness);
            SceneObject.Attributes.Set("OneSpeed", WaveConfig.WaveOne.Speed);
            SceneObject.Attributes.Set("OneWavelength", WaveConfig.WaveOne.Length);

            SceneObject.Attributes.Set("TwoDirection", WaveConfig.WaveTwo.Direction);
            SceneObject.Attributes.Set("TwoAmplitude", WaveConfig.WaveTwo.Amplitude);
            //SceneObject.Attributes.Set("TwoAmplitude", 10f);
            SceneObject.Attributes.Set("TwoSteepness", WaveConfig.WaveTwo.Steepness);
            SceneObject.Attributes.Set("TwoSpeed", WaveConfig.WaveTwo.Speed);
            SceneObject.Attributes.Set("TwoWavelength", WaveConfig.WaveTwo.Length);

            SceneObject.Attributes.Set("ThreeDirection", WaveConfig.WaveThree.Direction);
            SceneObject.Attributes.Set("ThreeAmplitude", WaveConfig.WaveThree.Amplitude);
            //SceneObject.Attributes.Set("ThreeAmplitude", 5f);
            SceneObject.Attributes.Set("ThreeSteepness", WaveConfig.WaveThree.Steepness);
            SceneObject.Attributes.Set("ThreeSpeed", WaveConfig.WaveThree.Speed);
            SceneObject.Attributes.Set("ThreeWavelength", WaveConfig.WaveThree.Length);

            SceneObject.Attributes.Set("FourDirection", WaveConfig.WaveFour.Direction);
            SceneObject.Attributes.Set("FourAmplitude", WaveConfig.WaveFour.Amplitude);
            //SceneObject.Attributes.Set("FourAmplitude", 6f);
            SceneObject.Attributes.Set("FourSteepness", WaveConfig.WaveFour.Steepness);
            SceneObject.Attributes.Set("FourSpeed", WaveConfig.WaveFour.Speed);
            SceneObject.Attributes.Set("FourWavelength", WaveConfig.WaveFour.Length);

            SceneObject.Attributes.Set("FiveDirection", WaveConfig.WaveFive.Direction);
            SceneObject.Attributes.Set("FiveAmplitude", WaveConfig.WaveFive.Amplitude);
            //SceneObject.Attributes.Set("FiveAmplitude", 3f);
            SceneObject.Attributes.Set("FiveSteepness", WaveConfig.WaveFive.Steepness);
            SceneObject.Attributes.Set("FiveSpeed", WaveConfig.WaveFive.Speed);
            SceneObject.Attributes.Set("FiveWavelength", WaveConfig.WaveFive.Length);
        }
    }    
}