using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Wisp : Character
    {
        public override int Health { get; set; } = 99999999;
        public override int BaseDamage { get; } = 0;

        void Start()
        {
            InvokeRepeating("CycleSprite", 1.0f, 0.2f);
        }

        public void CycleSprite()
        {
            if (DefaultSpriteId == 357)
                DefaultSpriteId = 352;
            
            DefaultSpriteId++;
            SetSprite(DefaultSpriteId);
        }

        public override bool OnCollision(Actor anotherActor) => false;

        protected override void OnDeath()
        {
            AudioManager.Singleton.Play("SkeletonDeath");
        }

        public override int DefaultSpriteId { get; protected set; } = Utilities.Random.Next(353, 358);
        public override string DefaultName => "Wisp";
    }
}
