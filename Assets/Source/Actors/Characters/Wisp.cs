using DungeonCrawl.Core.Audio;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Wisp : Character
    {
        public override int Health { get; protected set; } = 99999999;
        public override int BaseDamage { get; } = 0;

        void Start()
        {
            InvokeRepeating("CycleSprite", 1.0f, 0.5f);
        }

        public void CycleSprite()
        {
            Debug.Log(DefaultSpriteId);
            if (DefaultSpriteId == 358)
                DefaultSpriteId = 354;
            else
                DefaultSpriteId++;
        }

        public override bool OnCollision(Actor anotherActor) => false;

        protected override void OnDeath()
        {
            AudioManager.Singleton.Play("SkeletonDeath");
            Debug.Log("MY LIFE FOR THE QUEEN...");
        }

        public override int DefaultSpriteId { get; protected set; } = 354;
        public override string DefaultName => "Sentinel";
    }
}
