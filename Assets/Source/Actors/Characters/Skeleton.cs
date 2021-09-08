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
            InvokeRepeating("TryMove", 1.0f, 0.5f);
        }

        void TryMove()
        {
            var direction = (Direction)Random.Range(0, 4);
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
            {
                // No obstacle found, just move
                Position = targetPosition;
            }
            else
            {
                if (actorAtTargetPosition.OnCollision(this))
                {
                    // Allowed to move
                    Position = targetPosition;
                }
                else
                {
                    if (actorAtTargetPosition is Player player)
                    {
                        Attack(player);
                    }
                }
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
