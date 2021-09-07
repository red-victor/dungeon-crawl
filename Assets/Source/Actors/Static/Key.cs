namespace DungeonCrawl.Actors.Static
{
    public class Key : StaticActor
    {
        public override int DefaultSpriteId => 559;
        public override string DefaultName => "Key";

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
