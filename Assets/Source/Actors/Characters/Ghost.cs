using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;
using System;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Ghost : Character
    {
        public override int Health { get; protected set; } = 12;
        public override int BaseDamage { get; } = 3;

        private Player _player;

        void Start()
        {
            _player = ActorManager.Singleton.GetPlayer();
            InvokeRepeating("TryMove", 1.0f, 0.3f);
        }

        public override bool OnCollision(Actor anotherActor) => false;

        void TryMove()
        {
            var x = Math.Abs(this.Position.x - _player.Position.x);
            var y = Math.Abs(this.Position.y - _player.Position.y);

            if (x < 10 && y < 10 && !_player.Protected)
            {
                var direction = GetPlayerDirection(_player);
                var vector = direction.ToVector();
                (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

                var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

                if (actorAtTargetPosition is Player)
                    Attack(_player);
                else
                    Position = targetPosition;
            }
        }

        protected override void OnDeath()
        {
            MapLoader.RandomSpawnItem(Position);
            AudioManager.Singleton.Play("SkeletonDeath");
            Debug.Log("Well, I was already dead anyway also...");
        }

        public override int DefaultSpriteId => 314;
        public override string DefaultName => "Ghost";
    }
}