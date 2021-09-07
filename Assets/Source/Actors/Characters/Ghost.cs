using Assets.Source;
using DungeonCrawl.Core;
using System;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Ghost : Character
    {
        public override int Health { get; protected set; } = 15;
        public override int BaseDamage { get; } = 4;

        private Player _player;

        void Start()
        {
            _player = ActorManager.Singleton.GetPlayer();
            InvokeRepeating("TryMove", 1.0f, 0.5f);
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        public Direction GetPlayerDirection()
        {
            //if (Position.x <= player.Position.x && Position.y <= player.Position.y)
            //    return Position.x <= Position.y ? Direction.Down : Direction.Right;
            //else if (Position.x <= player.Position.x && Position.y >= player.Position.y)
            //    return Position.x <= Position.y ? Direction.Down : Direction.Left;
            //else if (Position.x >= player.Position.x && Position.y <= player.Position.y)
            //    return Position.x >= Position.y ? Direction.Up : Direction.Right;
            //else if (Position.x >= player.Position.x && Position.y >= player.Position.y)
            //    return Position.x >= Position.y ? Direction.Up : Direction.Left;
            //else
            //    return Direction.Down;

            if (this.Position.y > _player.Position.y)
                return Direction.Down;
            else if (this.Position.y < _player.Position.y)
                return Direction.Up;
            else if (this.Position.x > _player.Position.x)
                return Direction.Left;
            else if (this.Position.x < _player.Position.x)
                return Direction.Right;
            return Direction.Down;
        }

        void TryMove()
        {
            var x = Math.Abs(this.Position.x - _player.Position.x);
            var y = Math.Abs(this.Position.y - _player.Position.y);

            if (x < 10 && y < 10)
            {
                var direction = GetPlayerDirection();
                var vector = direction.ToVector();
                (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

                var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

                if (actorAtTargetPosition is Player)
                {
                    Attack(_player);
                }
                else
                {
                    Position = targetPosition;
                }
            }
        }

        protected override void OnDeath()
        {
            Debug.Log("Well, I was already dead anyway also...");
        }

        public override int DefaultSpriteId => 312;
        public override string DefaultName => "Ghost";
    }
}