using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Scream
{
	public class Scream
	{
		
		public static AudioClip s1 = ModAPI.LoadSound("Screams/S1.wav");
		public static AudioClip s2 = ModAPI.LoadSound("Screams/S2.wav");
		public static AudioClip s3 = ModAPI.LoadSound("Screams/S3.wav");
		public static AudioClip shot1 = ModAPI.LoadSound("Screams/ShotS1.wav");
		public static AudioClip shot2 = ModAPI.LoadSound("Screams/ShotS2.wav");
		public static AudioClip shot3 = ModAPI.LoadSound("Screams/ShotS3.wav");
		public static AudioClip shot4 = ModAPI.LoadSound("Screams/ShotS4.wav");
		public static AudioClip shot5 = ModAPI.LoadSound("Screams/ShotS5.wav");
		public static AudioClip shot6 = ModAPI.LoadSound("Screams/ShotS6.wav");
		public static AudioClip water1 = ModAPI.LoadSound("Screams/DS1.wav");
		public static AudioClip water2 = ModAPI.LoadSound("Screams/DS2.wav");

		public static void Main()
		{


			
			//if you can, dont call them at all in the component
			//load all sounds and images in the entry point, and refer to them in the component AKA load all sounds HERE and then refer to them in scraembehaviour


			ModAPI.OnItemSpawned += (EventHandler<UserSpawnEventArgs>)((sender, obj) =>
			{
				GameObject Ins = obj.Instance; // declared value
				PersonBehaviour component = Ins.GetComponent<PersonBehaviour>();// getting componenet for humans
																				//component.PainLevel();
																				//component.ImpactPainMultiplier();
				if (component != null)

				{
					foreach (LimbBehaviour limb in component.Limbs) //loops through limbs
					{
						if (!(bool)(UnityEngine.Object)limb.gameObject.GetComponent<HurtBehaviour>())
						{
							limb.gameObject.AddComponent<HurtBehaviour>();
						}
						if (!(bool)(UnityEngine.Object)limb.gameObject.GetComponent<ScreamBehaviour>() && !limb.IsAndroid) // funny how it says "IsAndroid" but only works for humans.
						{

							if (limb.HasBrain)// thanks kass
							{
								var scream = limb.gameObject.AddComponent<ScreamBehaviour>();
								scream.limb = limb;
								scream.Person = component;
							}
						}
					}
				}
			});


		}
	}


	class HurtBehaviour : MonoBehaviour
	{
		public LimbBehaviour limb;
		public PersonBehaviour Person;

		void Awake()
		{
			this.limb = GetComponent<LimbBehaviour>();
		}
		public void Shot(global::Shot shot)
		{
			gameObject.transform.root.FindChild("Head").GetComponent<ScreamBehaviour>().TriggerScream = true;
		}

		public void Stabbed(Stabbing stab)
		{
			gameObject.transform.root.FindChild("Head").GetComponent<ScreamBehaviour>().TriggerScream = true;
		}
		public void BreakBone(global::LimbBehaviour limb)
        {
			gameObject.transform.root.FindChild("Head").GetComponent<ScreamBehaviour>().TriggerScream = true;
		}
	}

	class ScreamBehaviour : MonoBehaviour
	{
		public LimbBehaviour limb;

		public float heat = 98f;

		public PhysicalBehaviour physicalBehaviour;
		public PersonBehaviour Person;

		public bool TriggerScream;

		public AudioSource aud;
		public AudioClip sound;

		public bool HasScreamedAboutBone = false;

		public AudioClip chosenAudioClip;

		float painTimer = 0f;

		float screamTime = 5f;

		public void CreateAudioSource() //creates an audio source
		{
			aud = this.gameObject.AddComponent<AudioSource>();
			aud.spatialBlend = 1;
			aud.minDistance = 3;
			aud.maxDistance = 50;
			aud.dopplerLevel = 0;
			aud.playOnAwake = false;
			aud.clip = sound;
			aud.loop = false;
			aud.volume = 4f;  
			Global.main.AddAudioSource(aud);
		}

		public void Awake()// instead of start
		{
				

			this.physicalBehaviour = this.GetComponent<PhysicalBehaviour>();
			ModAPI.Notify("Loaded!");
			CreateAudioSource();
			this.Person = this.GetComponent<PersonBehaviour>();
		}


		public void Update()
		{
			try
			{
				painTimer += Time.deltaTime; 
				if (Person.IsAlive())
				{
					if (TriggerScream)
					{
						
						var rand = UnityEngine.Random.Range(1,7);
						if (rand == 1)
                        {
							chosenAudioClip = Scream.shot1;
						}
						if (rand == 2)
						{
							chosenAudioClip = Scream.shot2;
						}
						if (rand == 3)
						{
							chosenAudioClip = Scream.shot3;
						}
						if (rand == 4)
						{
							chosenAudioClip = Scream.shot4;
						}
						if (rand == 5)
						{
							chosenAudioClip = Scream.shot5;
						}
						if (rand == 6)
						{
							chosenAudioClip = Scream.shot6;
						}
						aud.clip = chosenAudioClip;
						aud.Play();
						TriggerScream = false;
						ModAPI.Notify("aHHHHH");
					}

					if (painTimer >= screamTime)
					{
						if (Person.PainLevel > 0.3f)// might remove idk it links with fire and drowing which is super ironic
						{
							var rand = UnityEngine.Random.Range(1, 4);
							if (rand == 1)
                            {
								chosenAudioClip = Scream.s1;
							}
							if (rand == 2)
							{
								chosenAudioClip = Scream.s2;
							}
							if (rand == 3)
							{
								chosenAudioClip = Scream.s3;
							}
							ModAPI.Notify("Ouch");
							aud.clip = chosenAudioClip;
							aud.Play();
							painTimer = 0f;
						}
					}
					if (limb.Broken && !HasScreamedAboutBone)
                    {
						foreach (var limb in Person.Limbs)
						{
							var rand = UnityEngine.Random.Range(1, 7);
							if (rand == 1)
							{
								chosenAudioClip = Scream.shot1;
							}
							if (rand == 2)
							{
								chosenAudioClip = Scream.shot2;
							}
							if (rand == 3)
							{
								chosenAudioClip = Scream.shot3;
							}
							if (rand == 4)
							{
								chosenAudioClip = Scream.shot4;
							}
							if (rand == 5)
							{
								chosenAudioClip = Scream.shot5;
							}
							if (rand == 6)
							{
								chosenAudioClip = Scream.shot6;
							}
							aud.clip = chosenAudioClip;
							aud.Play();
							TriggerScream = false;
							HasScreamedAboutBone = true;
							ModAPI.Notify("LOL");
						}
					}
					
				}
				else
				{
					TriggerScream = false;
					aud.Stop();
				}
			}
			catch (NullReferenceException)
			{
				ModAPI.Notify("i cant scream ");
			}

		}
	}
}
