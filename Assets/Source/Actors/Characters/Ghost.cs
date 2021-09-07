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

        public bool IsPursuing = false;

        private int Count = 0;

        protected override void OnUpdate(float deltaTime)
        {
            var player = ActorManager.Singleton.GetPlayer();

            var x = Math.Abs(this.Position.x - player.Position.x);
            var y = Math.Abs(this.Position.y - player.Position.y);

            if (x > 4 || y > 4)
                IsPursuing = true;

            if (IsPursuing)
            {
                var direction = GetPlayerDirection(player);
                Count++;

                if (Count > 150)
                {
                    TryMove(direction);
                    Count = 0;
                }
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        public Direction GetPlayerDirection(Player player)
        {
            if (Position.x <= player.Position.x && Position.y <= player.Position.y)
                return Position.x <= Position.y ? Direction.Down : Direction.Right;
            else if (Position.x <= player.Position.x && Position.y >= player.Position.y)
                return Position.x <= Position.y ? Direction.Down : Direction.Left;
            else if (Position.x >= player.Position.x && Position.y <= player.Position.y)
                return Position.x >= Position.y ? Direction.Up : Direction.Right;
            else if (Position.x >= player.Position.x && Position.y >= player.Position.y)
                return Position.x >= Position.y ? Direction.Up : Direction.Left;
            else
                return Direction.Down;
        }

        public override void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            Position = targetPosition;

            if (actorAtTargetPosition is Player player)
            {
                Attack(player);
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