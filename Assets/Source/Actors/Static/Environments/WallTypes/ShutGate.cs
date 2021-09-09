using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Static.Environments
{
    public class ShutGate : WallType
    {
        public ShutGate()
        {
            DefaultSpriteId = MapLoader.CurrentMap switch
            {
                1 => 146,
                2 => 146,
                _ => 540,
            };
        }

        public override int DefaultSpriteId { get; protected set; }
        public override string DefaultName => "Shut Gate";
    }
}
