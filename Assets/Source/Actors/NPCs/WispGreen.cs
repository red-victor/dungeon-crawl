using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;

namespace DungeonCrawl.Actors.Characters
{
    public class WispGreen : NPC
    {
        private string message = "Still alive... Must continue...";

        public bool IsAlive = true;
        void Start()
        {
            InvokeRepeating("CycleSprite", 1.0f, 0.2f);
        }

        public void CycleSprite()
        {
            if (DefaultSpriteId == 453)
                DefaultSpriteId = 448;

            DefaultSpriteId++;
            SetSprite(DefaultSpriteId);

            if (!IsAlive)
            {
                CancelInvoke("CycleSprite");
                SetSprite(454);
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (IsAlive)
            {
                UserInterface.Singleton.SetText(message, UserInterface.TextPosition.BottomCenter);
                ActorManager.Singleton.Spawn<MagicGloves>((Position.x, Position.y + 1));
                IsAlive = false;
            }
            return false;
        }

        protected override void OnDeath()
        {
            AudioManager.Singleton.Play("SkeletonDeath");
        }

        public override int DefaultSpriteId { get; protected set; } = Utilities.Random.Next(449, 454);
        public override string DefaultName => "Green Wisp";
    }
}
