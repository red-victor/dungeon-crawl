namespace DungeonCrawl.Actors.Static.Items
{
    public abstract class Item : StaticActor
    {
        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
