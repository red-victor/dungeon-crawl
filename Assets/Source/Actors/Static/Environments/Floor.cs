namespace DungeonCrawl.Actors.Static.Environments
{
    public class Floor : Environment
    {
        public override int DefaultSpriteId => 1;
        public override string DefaultName => "Floor";

        public override bool Detectable => false;
    }
}
