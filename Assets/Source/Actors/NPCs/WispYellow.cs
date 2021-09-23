using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;

namespace DungeonCrawl.Actors.Characters
{
    public class WispYellow : NPC
    {
        private string message = "Hurry! Not...much...time...leeeeft...";

        public bool IsAlive = true;
        void Start()
        {
            InvokeRepeating("CycleSprite", 1.0f, 0.2f);
        }

        public void CycleSprite()
        {
            if (DefaultSpriteId == 405)
                DefaultSpriteId = 400;

            DefaultSpriteId++;
            SetSprite(DefaultSpriteId);

            if (!IsAlive)
            {
                CancelInvoke("CycleSprite");
                SetSprite(406);
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (IsAlive)
            {
                UserInterface.Singleton.SetText(message, UserInterface.TextPosition.BottomCenter);
                ActorManager.Singleton.Spawn<Heater>((Position.x, Position.y + 1));
                IsAlive = false;
            }
            return false;
        }

        protected override void OnDeath()
        {
            AudioManager.Singleton.Play("SkeletonDeath");
        }

        public override int DefaultSpriteId { get; protected set; } = Utilities.Random.Next(401, 406);
        public override string DefaultName => "Yellow Wisp";
    }
}
