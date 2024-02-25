using Editor;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace MyGame
{
    public static class WaveConfig
    {
        public struct WaveOptions
        {
            public Vector3 Direction;
            public float Amplitude;
            public float Steepness;
            public float Speed;
            public float Length;
        }
        
        public static WaveOptions WaveOne = new WaveOptions()
        {
            Direction = new Vector3(0.300f, 0.450f, 0.0f).Normal,
            Amplitude = 8f,
            Steepness = 0.1f,
            Speed = 31.700f,
            Length = 640f
        };

        public static WaveOptions WaveTwo = new WaveOptions()
        {
            Direction = new Vector3(0.733f, 0.337f, 0.0f).Normal,
            Amplitude = 10f,
            Steepness = 0.370f,
            Speed = 34f,
            Length = 407f
        };
        
        public static WaveOptions WaveThree = new WaveOptions()
        {
            Direction = new Vector3(0.258f, 0.770f, 0.0f).Normal,
            Amplitude = 5f,
            Steepness = 0.030f,
            Speed = 50f,
            Length = 235f
        };
        
        public static WaveOptions WaveFour = new WaveOptions()
        {
            Direction = new Vector3(1f, 0.413f, 0.0f).Normal,
            Amplitude = 6f,
            Steepness = 0.4f,
            Speed = 31.700f,
            Length = 317f
        };
        
        public static WaveOptions WaveFive = new WaveOptions()
        {
            Direction = new Vector3(0.148f, 0.754f, 0.0f).Normal,
            Amplitude = 3f,
            Steepness = 0.2f,
            Speed = 40f,
            Length = 307f
        };
    }
}