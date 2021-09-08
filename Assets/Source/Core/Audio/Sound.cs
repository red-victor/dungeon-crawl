using UnityEngine.Audio;
using UnityEngine;

namespace DungeonCrawl.Core.Audio
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(0f, 3f)]
        public float pitch;

        [HideInInspector]
        public AudioSource source;
    }
}
