using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;

using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
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

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        private void PickUp()
        {
            var item = ActorManager.Singleton.GetActorAt<InanimateObject>(Position);

            if (item != null)
                ActorManager.Singleton.DestroyActor(item);
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
