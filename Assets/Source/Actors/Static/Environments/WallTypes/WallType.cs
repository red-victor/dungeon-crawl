namespace DungeonCrawl.Actors.Static.Environments
{
    public abstract class WallType : Environment
    {
        public override bool OnCollision(Actor anotherActor) => false;
    }
}
