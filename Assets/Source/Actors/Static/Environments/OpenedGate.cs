namespace DungeonCrawl.Actors.Static.Environments
{
    public class OpenedGate : Environment
    {
        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        public override int DefaultSpriteId => 539;
        public override string DefaultName => "OpenedGate";
    }
}