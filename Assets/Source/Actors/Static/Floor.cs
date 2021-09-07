namespace DungeonCrawl.Actors.Static
{
    public class Floor : StaticActor
    {
        public override int DefaultSpriteId => 1;
        public override string DefaultName => "Floor";

        public override bool Detectable => false;
    }
}
