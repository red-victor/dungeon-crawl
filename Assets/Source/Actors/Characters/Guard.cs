using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;

namespace DungeonCrawl.Actors.Characters
{
    public class Guard : Character
    {
        public override int Health { get; set; } = 5 + 5 * MapLoader.CurrentMap;
        public override int BaseDamage { get; } = 2 * MapLoader.CurrentMap - 2;

        public override bool OnCollision(Actor anotherActor) => false;

        protected override void OnDeath()
        {
            AudioManager.Singleton.Play("SkeletonDeath");
        }

        public override int DefaultSpriteId => 29;
        public override string DefaultName => "Guard";
    }
}
