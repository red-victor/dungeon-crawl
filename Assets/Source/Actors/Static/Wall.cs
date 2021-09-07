namespace DungeonCrawl.Actors.Static
{
    public class Wall : StaticActor
    {
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        public override int DefaultSpriteId => 825;
        public override string DefaultName => "Wall";
    }
}
