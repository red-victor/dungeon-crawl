namespace DungeonCrawl.Actors.Static
{
    public class Sword : Actor
    {
        public override int DefaultSpriteId => 415;
        public override string DefaultName => "Sword";

        public override bool Detectable => true;

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
