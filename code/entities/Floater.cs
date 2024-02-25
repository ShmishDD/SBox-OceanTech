using Editor;
using Sandbox;
using System;
using System.Collections.Generic;

namespace MyGame
{
	[Library("floater"), HammerEntity]
    [Title("Floater"), EditorModel("models/floater.vmdl")]
    public class Floater : ModelEntity
    {
        private static readonly Model FloaterModel = Model.Load("models/floater.vmdl");
        public override void Spawn()
        {
            base.Spawn();

            Model = FloaterModel;
            EnableTouch = false;
            EnableAllCollisions = false;
            EnableSelfCollisions = false;
            EnableSolidCollisions = false;
            EnableShadowCasting = false;
            EnableTraceAndQueries = true;
            
            
            //SetupPhysicsFromModel(PhysicsMotionType.Static);

            //PhysicsBody.GravityEnabled = false;
        }

        // [GameEvent.Tick.Server]
        // private void Tick()
        // public Vector3 FloatersTick(Vector3 FloaterPos)
        // {
        //     //--------------------------------------
        //     float waveHeight = WaveController.WaveHeight(FloaterPos.z) + WaveController.WaveHeight(FloaterPos.y);
        //     displacementAmount = 50f;
        //     depthBeforeSubmerged = 30f;

        //     if (FloaterPos.z < waveHeight)
        //     {
        //         float floatMath01 = (waveHeight - FloaterPos.z) / depthBeforeSubmerged;
        //         float floatMath02 = Math.Max(0, Math.Min(floatMath01, 1));
        //         float floatDisplacement = floatMath02 * displacementAmount;
                
        //         Vector3 floatDisplacementVector = new Vector3(0, 0, -floatDisplacement);
                
        //         return floatDisplacementVector;
        //     }
        //     else
        //     {
        //         return Vector3.Zero;
        //     }
        // }
    }
}


            // // Vector3 displacementAmount = Vector3.Up * 20f;
            // Position = Position.WithZ(WaveController.WaveHeight(Position));
            // Angles redthing = WaveController.GetWaveRotationDebug(Position, CollisionBounds).Angles();
            // Rotation redthing2 = WaveController.GetWaveRotationDebug(Position, CollisionBounds);
            // Vector3 VectorAngles = redthing.AsVector3();
            // Log.Info(VectorAngles);
            // //Rotation = WaveController.GetWaveRotationDebug(Position, CollisionBounds);

            // float waveHeight = WaveController.WaveHeight(Position);
            // if (Rotation != redthing2)
            // {
            //     PhysicsBody.AngularVelocity = VectorAngles;
            // }

            // PhysicsBody.LinearDamping = 0f;