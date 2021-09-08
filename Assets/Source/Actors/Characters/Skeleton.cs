using Assets.Source;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public override int Health { get; protected set; } = 10;
        public override int BaseDamage { get; } = 2;

        void Start()
        {
            InvokeRepeating("RandomMove", 1.0f, 1.0f);
        }

        void RandomMove()
        {
            var directionCount = Direction.GetNames(typeof(Direction)).Length;
            var direction = (Direction)Random.Range(0, directionCount);
            TryMove(direction);
        }

        public override bool OnCollision(Actor anotherActor) => false;

        protected override void OnDeath()
        {
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
