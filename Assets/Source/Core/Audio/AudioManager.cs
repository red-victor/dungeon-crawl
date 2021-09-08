using System;
using UnityEngine.Audio;
using UnityEngine;

namespace DungeonCrawl.Core.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Singleton { get; private set; }

        public Sound[] Sounds;
        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }

            Singleton = this;

            foreach (Sound s in Sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }

        public void Play(string soundName)
        {
            Sound s = Array.Find(Sounds, sound => sound.name == soundName);
            s.source.Play();
        }
    }
}
