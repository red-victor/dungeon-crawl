using Assets.Source;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public override int Health { get; protected set; } = 10;
        public override int BaseDamage { get; } = 2;

        private int Count = 0;

        protected override void OnUpdate(float deltaTime)
        {
            var direction = (Direction)Random.Range(0, 4);
            Count++;

            if (Count > 150)
            {
                TryMove(direction);
                Count = 0;
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
