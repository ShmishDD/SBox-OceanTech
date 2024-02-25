using Editor;
using Sandbox;
using System;
using System.Collections.Generic;

namespace MyGame
{
	[Library("pirate_ship"), HammerEntity]
    [Title("Pirate Ship"), EditorModel("models/pirateship.vmdl")]
    public partial class PirateShip : ModelEntity
    {
        private static readonly Model PirateShipModel = Model.Load("models/pirateship.vmdl");
        public Vector3 DisplacementVector1;
        public Vector3 DisplacementVector2;
        public Vector3 DisplacementVector3;
        public Vector3 DisplacementVector4;

        public static int FloaterCount = 4;
        public static float WaterDrag = 0.99f;
        public static float WaterAngularDrag = 0.5f;

        public static Vector3 Gravity = new Vector3(0, 0, -800);

        public ModelEntity floater1;
        public ModelEntity floater2;
        public ModelEntity floater3;
        public ModelEntity floater4;
        public Vector3 DebugPosition = new Vector3(0f, 0f, 0f); // debug

        public override void Spawn()
        {
            base.Spawn();

            Model = PirateShipModel;
            EnableTouch = true;
            EnableAllCollisions = true;
            EnableSelfCollisions = true;
            EnableSolidCollisions = true;
            EnableShadowCasting = true;
            EnableTraceAndQueries = true;

            Tags.Add("ocean");
            Tags.Add("solid");
            
            SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
            
            PhysicsBody.GravityEnabled = false;
            
            var backLeft = GetAttachment( "FloaterPos1" ) ?? default;
            var frontLeft = GetAttachment( "FloaterPos2" ) ?? default;
            var backRight = GetAttachment( "FloaterPos3" ) ?? default;
            var frontRight = GetAttachment( "FloaterPos4" ) ?? default;

            floater1 = new Floater();
            floater1.Spawn();
            floater1.Transform = backLeft;
            floater2 = new Floater();
            floater2.Spawn();
            floater2.Transform = frontLeft;
            floater3 = new Floater();
            floater3.Spawn();
            floater3.Transform = backRight;
            floater4 = new Floater();
            floater4.Spawn();
            floater4.Transform = frontRight;

            floater1.Parent = this;
            floater2.Parent = this;
            floater3.Parent = this;
            floater4.Parent = this;

            PhysicsBody.GravityEnabled = false;
            //PhysicsBody.Mass = 40f;
            
            ResetBones();

        }

        [GameEvent.Tick.Server]
        private void Tick()
        {
            PhysicsBody.Mass = 50f;
            
            Gravity = new Vector3(0f, 0f, -800f * PhysicsBody.Mass);

            float DebugOutput = WaveController.WaveHeight(DebugPosition); // debug
            Vector3 DebugPositionUpdated = new Vector3(0f, 0f, DebugOutput); // debug
            DebugOverlay.Sphere(DebugPositionUpdated, 10, Color.Red, 0, false); //debug
            //Log.Info("Wave Height at 0: " + DebugOutput); //debug

            PhysicsBody.ApplyForceAt(floater1.Position, Gravity / 4);
            PhysicsBody.ApplyForceAt(floater2.Position, Gravity / 4);
            PhysicsBody.ApplyForceAt(floater3.Position, Gravity / 4);
            PhysicsBody.ApplyForceAt(floater4.Position, Gravity / 4);


            //---------
            float F1Check = WaveController.WaveHeight(floater1.Position);
            float F2Check = WaveController.WaveHeight(floater2.Position);
            float F3Check = WaveController.WaveHeight(floater2.Position);
            float F4Check = WaveController.WaveHeight(floater4.Position);
            //---------

            WaterDrag = 1f;
            WaterAngularDrag = 2f;


            //PhysicsBody.DragEnabled = false;
            PhysicsBody.LinearDrag = 1f;
            PhysicsBody.AngularDrag = 20f;

            //PhysicsBody.GravityEnabled = true;

            Vector3 negativeAngularVelocity = this.PhysicsBody.AngularVelocity;


            DisplacementVector1 = WaveController.FloatersTick(floater1.Position);
            if(floater1.Position.z < F1Check)
            {
                this.PhysicsBody.ApplyForceAt(floater1.Position, DisplacementVector1 * -Gravity);
                this.PhysicsBody.ApplyForce(DisplacementVector1 * -Velocity * WaterDrag * Time.Delta);
                this.PhysicsBody.ApplyTorque(DisplacementVector1 * negativeAngularVelocity * WaterAngularDrag * Time.Delta);
                //Log.Info("Floater 01 Displacement:" + DisplacementVector1);
            }

            DisplacementVector2 = WaveController.FloatersTick(floater2.Position);
            if(floater2.Position.z < F2Check)
            {
                this.PhysicsBody.ApplyForceAt(floater2.Position, DisplacementVector2 * -Gravity);
                this.PhysicsBody.ApplyForce(DisplacementVector2 * -Velocity * WaterDrag * Time.Delta);
                this.PhysicsBody.ApplyTorque(DisplacementVector2 * negativeAngularVelocity * WaterAngularDrag * Time.Delta);
                //Log.Info("Floater 02 Displacement:" + DisplacementVector2);
            }

            DisplacementVector3 = WaveController.FloatersTick(floater3.Position);
            if(floater3.Position.z < F3Check)
            {
                this.PhysicsBody.ApplyForceAt(floater3.Position, DisplacementVector3 * -Gravity);
                this.PhysicsBody.ApplyForce(DisplacementVector3 * -Velocity * WaterDrag * Time.Delta);
                this.PhysicsBody.ApplyTorque(DisplacementVector3 * negativeAngularVelocity * WaterAngularDrag * Time.Delta);
                //Log.Info("Floater 03 Displacement:" + DisplacementVector3);
            }

            DisplacementVector4 = WaveController.FloatersTick(floater4.Position);
            if(floater3.Position.z < F4Check)
            {
                this.PhysicsBody.ApplyForceAt(floater4.Position, DisplacementVector4 * -Gravity);
                this.PhysicsBody.ApplyForce(DisplacementVector4 * -Velocity * WaterDrag * Time.Delta);
                this.PhysicsBody.ApplyTorque(DisplacementVector4 * negativeAngularVelocity * WaterAngularDrag * Time.Delta);
                //Log.Info("Floater 04 Displacement:" + DisplacementVector4);
            }

            //Log.Info("Velocity:" + Velocity);
        }
    }
}