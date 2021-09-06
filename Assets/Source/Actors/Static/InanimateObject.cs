namespace DungeonCrawl.Actors.Static
{
    public abstract class InanimateObject : Actor
    {
        public override bool Detectable => true;

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
