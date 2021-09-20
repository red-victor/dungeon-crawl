using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;
using System;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Peasant : Character
    {
        public override int Health { get; protected set; } = 6;
        public override int BaseDamage { get; } = 2;

        private Player _player;

        void Start()
        {
            _player = ActorManager.Singleton.GetPlayer();
            InvokeRepeating("Pursue", 1.0f, 0.3f);
        }

        public override bool OnCollision(Actor anotherActor) => false;

        void Pursue()
        {
            var x = Math.Abs(this.Position.x - _player.Position.x);
            var y = Math.Abs(this.Position.y - _player.Position.y);

            if (x < 6 && y < 6)
            {
                var direction = GetPlayerDirection(_player);
                TryMove(direction);
            }
        }

        protected override void OnDeath()
        {
            MapLoader.RandomSpawnItem(Position);
            AudioManager.Singleton.Play("SkeletonDeath");
            Debug.Log("Well, I was already dead anyway also...");
        }

        public override int DefaultSpriteId => 72;
        public override string DefaultName => "Peasant";
    }
}