using DungeonCrawl.Core.Audio;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Sentinel : Character
    {
        public override int Health { get; protected set; } = 25;
        public override int BaseDamage { get; } = 5;

        public override bool OnCollision(Actor anotherActor) => false;

        protected override void OnDeath()
        {
            AudioManager.Singleton.Play("SkeletonDeath");
            Debug.Log("MY LIFE FOR THE QUEEN...");
        }

        public override int DefaultSpriteId => 170;
        public override string DefaultName => "Sentinel";
    }
}
