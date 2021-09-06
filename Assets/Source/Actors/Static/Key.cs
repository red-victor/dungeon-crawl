namespace DungeonCrawl.Actors.Static
{
    public class Key : Actor
    {
        public override int DefaultSpriteId => 559;
        public override string DefaultName => "Floor";

        public override bool Detectable => true;

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
