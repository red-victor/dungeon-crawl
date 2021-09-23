using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Wasp : Character
    {
        public override int Health { get; set; } = 4;
        public override int BaseDamage { get; } = 1;

        private Direction _direction;

        void Start()
        {
            InvokeRepeating("TryMove", 1.0f, 0.4f);
        }

        public override bool OnCollision(Actor anotherActor) => false;

        void TryMove()
        {
            _direction = (Direction)Random.Range(2, 4);
            //_direction = _direction != Direction.Left ? Direction.Left : Direction.Right;
            TryMove(_direction);
        }

        protected override void OnDeath()
        {
            int percent = Utilities.Random.Next(0, 100);

            if (percent < 30)
                ActorManager.Singleton.Spawn<HealthKit>(Position);

            string[] waspDeathSounds = new string[] { "Bite1", "Bite2", "Bite3"};
            var index = Utilities.Random.Next(0, waspDeathSounds.Length);
            AudioManager.Singleton.Play(waspDeathSounds[index]);
        }

        public override int DefaultSpriteId => 265;
        public override string DefaultName => "Wasp";
    }
}