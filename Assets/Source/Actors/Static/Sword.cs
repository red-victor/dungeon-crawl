namespace DungeonCrawl.Actors.Static
{
    public class Sword : StaticActor
    {
        public override int DefaultSpriteId => 415;
        public override string DefaultName => "Sword";

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
