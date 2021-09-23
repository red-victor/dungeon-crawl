using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;

namespace DungeonCrawl.Actors.Characters
{
    public class WispBlue : NPC
    {
        private string message = "Take this! You must succeed!";

        public bool IsAlive = true;
        void Start()
        {
            InvokeRepeating("CycleSprite", 1.0f, 0.2f);
        }

        public void CycleSprite()
        {
            if (DefaultSpriteId == 357)
                DefaultSpriteId = 352;
            
            DefaultSpriteId++;
            SetSprite(DefaultSpriteId);

            if (!IsAlive)
            {
                ActorManager.Singleton.DestroyActor(this);
                ActorManager.Singleton.Spawn<WispBlueDead>(Position);
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (IsAlive)
            {
                UserInterface.Singleton.SetText(message, UserInterface.TextPosition.BottomCenter);
                ActorManager.Singleton.Spawn<Dagger>((Position.x, Position.y + 1));
                IsAlive = false;
            }
            return false;
        }

        protected override void OnDeath()
        {
            AudioManager.Singleton.Play("SkeletonDeath");
        }

        public override int DefaultSpriteId { get; protected set; } = Utilities.Random.Next(353, 358);
        public override string DefaultName => "Blue Wisp";
    }
}
