namespace DungeonCrawl.Actors.Static.Environments
{
    public class Wall : Environment
    {
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        public override int DefaultSpriteId => 825;
        public override string DefaultName => "Wall";
    }
}
