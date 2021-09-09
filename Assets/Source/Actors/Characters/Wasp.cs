using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Wasp : Character
    {
        public override int Health { get; protected set; } = 4;
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
            MapLoader.RandomSpawnItem(Position);
            AudioManager.Singleton.Play("SkeletonDeath");
            Debug.Log("Bzzzzzzz");
        }

        public override int DefaultSpriteId => 265;
        public override string DefaultName => "Wasp";
    }
}