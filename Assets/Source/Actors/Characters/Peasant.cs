using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;
using System;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Peasant : Character
    {
        public override int Health { get; set; } = 6;
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
                var direction = GetTargetDirection(_player.Position);
                TryMove(direction);
            }
        }

        protected override void OnDeath()
        {
            int percent = new System.Random().Next(0, 100);

            if (percent < 20)
                ActorManager.Singleton.Spawn<Pike>(Position);
            else
                MapLoader.RandomSpawnItem(Position);

            AudioManager.Singleton.Play("SkeletonDeath");
        }

        public override int DefaultSpriteId => 72;
        public override string DefaultName => "Peasant";
    }
}