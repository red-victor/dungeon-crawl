using Assets.Source;
using Assets.Source.Core;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;

using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private Inventory _inventory = new Inventory();

        protected override void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                // Pick up Item
                PickUp();
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        public override void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
            {
                UserInterface.Singleton.SetText("", UserInterface.TextPosition.BottomRight);
                // No obstacle found, just move
                Position = targetPosition;
            }
            else
            {
                if (actorAtTargetPosition.OnCollision(this))
                {
                    UserInterface.Singleton.SetText("Press E to pick up", UserInterface.TextPosition.BottomRight);
                    // Allowed to move
                    Position = targetPosition;
                }
            }
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        private void PickUp()
        {
            var item = ActorManager.Singleton.GetActorAt<InanimateObject>(Position);

            if (item != null)
            {
                UserInterface.Singleton.SetText("", UserInterface.TextPosition.BottomRight);
                _inventory.AddItem(item);
                ActorManager.Singleton.DestroyActor(item);
            }
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
