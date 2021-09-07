using DungeonCrawl.Core;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Actors.Static
{
    public class OpenedGate : Actor
    {
        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        public override int DefaultSpriteId => 539;
        public override string DefaultName => "OpenedGate";
    }
}