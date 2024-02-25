using Editor;
using Sandbox;
using System;
using System.Collections.Generic;
using Sandbox.UI;
using System.Reflection;

namespace MyGame
{
    public static class WaveController
    {
        public static float WAVE_COUNT = 1.000f;
        public const float BASE_WAVE_LENGTH = 2.000f * 3.142f;


        public static float OneZMath;
        public static float TwoZMath;
        public static float ThreeZMath;
        public static float FourZMath;
        public static float FiveZMath;
        public static float FinalZMath;
        public static float displacementAmount;
        public static float depthBeforeSubmerged;

        public static float WaveHeight(in Vector3 worldPos)
        {
            float OnelengthProduct = BASE_WAVE_LENGTH / WaveConfig.WaveOne.Length;
            float OnedotProduct = WaveConfig.WaveOne.Direction.Dot(worldPos);
            float OneSine = (((OnelengthProduct * WaveConfig.WaveOne.Speed) * RealTime.Now) + (OnedotProduct * OnelengthProduct));
            OneZMath = WaveConfig.WaveOne.Amplitude * MathF.Sin(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine - WaveConfig.WaveOne.Steepness * MathF.Cos(OneSine))))))))))));
            // Log.Info(WaveConfig.WaveOne.Amplitude);
            // Log.Info(WaveConfig.WaveOne.Direction);
            // Log.Info(WaveConfig.WaveOne.Length);
            // Log.Info(WaveConfig.WaveOne.Speed);
            // Log.Info(WaveConfig.WaveOne.Steepness);

            float TwolengthProduct = BASE_WAVE_LENGTH / WaveConfig.WaveTwo.Length;
            float TwodotProduct = WaveConfig.WaveTwo.Direction.Dot(worldPos);
            float TwoSine = (((TwolengthProduct * WaveConfig.WaveTwo.Speed) * RealTime.Now) + (TwodotProduct * TwolengthProduct));
            TwoZMath = WaveConfig.WaveTwo.Amplitude * MathF.Sin(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine - WaveConfig.WaveTwo.Steepness * MathF.Cos(TwoSine))))))))))));

            float ThreelengthProduct = BASE_WAVE_LENGTH / WaveConfig.WaveThree.Length;
            float ThreedotProduct = WaveConfig.WaveThree.Direction.Dot(worldPos);
            float ThreeSine = (((ThreelengthProduct * WaveConfig.WaveThree.Speed) * RealTime.Now) + (ThreedotProduct * ThreelengthProduct));
            ThreeZMath = WaveConfig.WaveThree.Amplitude * MathF.Sin(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine - WaveConfig.WaveThree.Steepness * MathF.Cos(ThreeSine))))))))))));

            float FourlengthProduct = BASE_WAVE_LENGTH / WaveConfig.WaveFour.Length;
            float FourdotProduct = WaveConfig.WaveFour.Direction.Dot(worldPos);
            float FourSine = (((FourlengthProduct * WaveConfig.WaveFour.Speed) * RealTime.Now) + (FourdotProduct * FourlengthProduct));
            FourZMath = WaveConfig.WaveFour.Amplitude * MathF.Sin(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine - WaveConfig.WaveFour.Steepness * MathF.Cos(FourSine))))))))))));

            float FivelengthProduct = BASE_WAVE_LENGTH / WaveConfig.WaveFive.Length;
            float FivedotProduct = WaveConfig.WaveFive.Direction.Dot(worldPos);
            float FiveSine = (((FivelengthProduct * WaveConfig.WaveFive.Speed) * RealTime.Now) + (FivedotProduct * FivelengthProduct));
            FiveZMath = WaveConfig.WaveFive.Amplitude * MathF.Sin(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine - WaveConfig.WaveFive.Steepness * MathF.Cos(FiveSine))))))))))));

            FinalZMath = OneZMath + TwoZMath + ThreeZMath + FourZMath + FiveZMath;

            return FinalZMath;
        }




        // public static float WAVE_COUNT = 1.000f;
        // public const float BASE_WAVE_LENGTH = 2.000f * 3.142f;
        // public static readonly Vector3 Direction = new Vector3(1.000f, 0.000f, 0.000f).Normal; 
        // public static float Length { get; set; } = 4096.000f;
        // public static float Amplitude { get; set; } = 200f;
        // public static float Speed { get; set; } = 150.000f;
        // public static float Steepness { get; set; } = 0.5f;
        // public static float zmath;
        // public static float displacementAmount;
        // public static float depthBeforeSubmerged;

        // public static float WaveHeight(in Vector3 worldPos)
        // {
        //     float lengthProduct = BASE_WAVE_LENGTH / Length;
        //     float lengthProductInvert = Length / BASE_WAVE_LENGTH;
        //     float dotProduct = Direction.Dot(worldPos.x);
        //     float steepProduct = ((lengthProduct * Amplitude) / Steepness);
        //     float sine = (((lengthProduct * Speed) * RealTime.Now) + (dotProduct * lengthProduct));
        //     //zmath = Amplitude * steepProduct * MathF.Sin(sine - (steepProduct * (lengthProduct) * MathF.Cos(sine - steepProduct * (lengthProduct) * MathF.Cos(sine - steepProduct * (lengthProduct) * MathF.Cos(sine - steepProduct * (lengthProduct) * MathF.Cos(sine - steepProduct * (lengthProduct) * MathF.Cos(sine - steepProduct * (lengthProduct) * MathF.Cos(sine - steepProduct * (lengthProduct) * MathF.Cos(sine)))))))));
        //     zmath = Amplitude * MathF.Sin(sine - Steepness * MathF.Cos(sine - Steepness * MathF.Cos(sine - Steepness * MathF.Cos(sine - Steepness * MathF.Cos(sine - Steepness * MathF.Cos(sine - Steepness * MathF.Cos(sine - Steepness * MathF.Cos(sine))))))));

        //     return zmath;
        // }

		// public static Rotation GetWaveRotation(in Vector3 pos, in BBox box)
		// {
		// 	Vector3 backLeft = pos + new Vector3(box.Mins.x, box.Mins.y, box.Mins.z);
		// 	Vector3 frontLeft = pos + new Vector3(box.Maxs.x, box.Mins.y, box.Mins.z);
		// 	Vector3 backRight = pos + new Vector3(box.Mins.x, box.Maxs.y, box.Mins.z);

		// 	backLeft = backLeft.WithZ(WaveHeight(backLeft));
		// 	frontLeft = frontLeft.WithZ(WaveHeight(frontLeft));
		// 	backRight = backRight.WithZ(WaveHeight(backRight));
			
		// 	Vector3 front = frontLeft - backLeft;
		// 	Vector3 right = backRight - backLeft;
		// 	Vector3 up = right.Cross(front);

		// 	return Rotation.LookAt(front, -up);
		// }

		// public static Vector3 GetWaveRotationDebug(in Vector3 pos, in BBox box)
		// {
		// 	Vector3 backLeft = pos + new Vector3(box.Mins.x, box.Mins.y, box.Mins.z);
		// 	Vector3 frontLeft = pos + new Vector3(box.Maxs.x, box.Mins.y, box.Mins.z);
		// 	Vector3 backRight = pos + new Vector3(box.Mins.x, box.Maxs.y, box.Mins.z);

		// 	backLeft = backLeft.WithZ(WaveHeight(backLeft));
		// 	frontLeft = frontLeft.WithZ(WaveHeight(frontLeft));
		// 	backRight = backRight.WithZ(WaveHeight(backRight));
			
		// 	DebugOverlay.Box(backLeft, -Vector3.One, Vector3.One, Color.Red, 0, false);
		// 	DebugOverlay.Box(frontLeft, -Vector3.One, Vector3.One, Color.Green, 0, false);
		// 	DebugOverlay.Box(backRight, -Vector3.One, Vector3.One, Color.Blue, 0, false);
			
		// 	DebugOverlay.Line(frontLeft, backRight, Color.Yellow, 0, false);
		// 	DebugOverlay.Line(backRight, backLeft, Color.Cyan, 0, false);
			
		// 	Vector3 front = frontLeft - backLeft;
		// 	Vector3 right = backRight - backLeft;
		// 	Vector3 up = right.Cross(front).Normal;
			
		// 	DebugOverlay.Sphere(pos + front, 1, Color.Red, 0, false);
		// 	DebugOverlay.Sphere(pos + right, 1, Color.Green, 0, false);
		// 	DebugOverlay.Sphere(pos + up, 1, Color.Blue, 0, false);
			
		// 	return (backLeft);
		// }

        public static Vector3 FloatersTick(Vector3 FloaterPos)
        {
            //--------------------------------------+ WaveController.WaveHeight(FloaterPos.x)
            float waveHeight = WaveController.WaveHeight(FloaterPos);


            displacementAmount = 3f;
            float depthBeforeSubmerged = 500f;
            

            if (FloaterPos.z < waveHeight)
            {
                float floatMath01 = (waveHeight - FloaterPos.z) / depthBeforeSubmerged;
                float floatMath02 = Math.Max(0, Math.Min(floatMath01, 1));
                float floatDisplacement = floatMath02 * displacementAmount;
                Vector3 floatDisplacementVector = new Vector3(0, 0, floatDisplacement);
                
                return floatDisplacementVector;
            }
            else
            {
                return Vector3.Zero;
            }
        }
    }
}
