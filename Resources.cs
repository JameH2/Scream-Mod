using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Scream
{
    struct Resources
    { //IM TRYING IM TRYING I REALLY AM!!!! :((((( I TOO AFRAID TO ASK FOR HELP AAAAAAAAAAAA

        public static AudioClip[] audioClips;
        public static AudioClip[] audioClipsShot;
        public static AudioClip[] audioClipsWater;
		public static ScreamBehaviour screaming;
        public static void Initalise()// i saw what zooi did in his halo mod code and tried to apply it to here
        {


            screaming.CreateAudioSource();
			audioClips = new AudioClip[]
			{
			ModAPI.LoadSound("Screams/S1.wav"),
			ModAPI.LoadSound("Screams/S2.wav"),
			ModAPI.LoadSound("Screams/S3.wav"),
			};
			audioClipsShot = new AudioClip[]
			{
				ModAPI.LoadSound("Screams/ShotS1.wav"),
				ModAPI.LoadSound("Screams/ShotS2.wav"),
				ModAPI.LoadSound("Screams/ShotS3.wav"),
				ModAPI.LoadSound("Screams/ShotS4.wav"),
				ModAPI.LoadSound("Screams/ShotS5.wav"),
				ModAPI.LoadSound("Screams/ShotS6.wav"),
			};
			audioClipsWater = new AudioClip[]
			{
				ModAPI.LoadSound("Screams/DS1.wav"),
				ModAPI.LoadSound("Screams/DS2.wav"),
			};
		}



    }
}
