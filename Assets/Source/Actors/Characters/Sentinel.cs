using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;

namespace DungeonCrawl.Actors.Characters
{
    public class Sentinel : Character
    {
        public override int Health { get; set; } = 7 + 5 * MapLoader.CurrentMap;
        public override int BaseDamage { get; } = 5;

        public override bool OnCollision(Actor anotherActor) => false;

        protected override void OnDeath()
        {
            ActorManager.Singleton.Spawn<Key>(Position);
            AudioManager.Singleton.Play("SkeletonDeath");
        }

        public override int DefaultSpriteId => 170;
        public override string DefaultName => "Sentinel";
    }
}
