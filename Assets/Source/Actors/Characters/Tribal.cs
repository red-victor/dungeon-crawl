using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;
using System;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Tribal : Character
    {
        public override int Health { get; set; } = 10;
        public override int BaseDamage { get; } = 3;

        //private (int, int)[] _center;

        void Start()
        {
            //_center = new (int, int)[6];

            //for (int i = 0; i < 6; i++)
            //    _center[i] = (29, - 25 - i);
            
            InvokeRepeating("Pursue", 1.0f, 0.7f);
        }

        public override bool OnCollision(Actor anotherActor) => false;

        void Pursue()
        {
            Debug.Log(Position);
            //var direction = GetTargetDirection(_center[Position.y + 25]);
            var direction = GetTargetDirection((29, -25));
            TryMove(direction);
        }

        public override void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
                Position = targetPosition;
            else
                if (actorAtTargetPosition.OnCollision(this))
                    Position = targetPosition;
                else
                    if (actorAtTargetPosition is Character character)
                        Attack(character);
        }

        protected override void OnDeath()
        {
            //MapLoader.RandomSpawnItem(Position);
            AudioManager.Singleton.Play("SkeletonDeath");
        }

        public override int DefaultSpriteId => 123;
        public override string DefaultName => "Tribal";
    }
}