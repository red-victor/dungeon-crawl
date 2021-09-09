namespace DungeonCrawl.Actors.Static.Items
{
    public class HealthKit : Consumable
    {
        public override int DefaultSpriteId => 570;
        public override string DefaultName => "HealthKit";
        public int Heal => 10;
    }
}
