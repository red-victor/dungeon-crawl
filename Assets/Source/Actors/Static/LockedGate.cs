namespace DungeonCrawl.Actors.Static
{
    public class LockedGate : Actor
    {
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        public override int DefaultSpriteId => 540;
        public override string DefaultName => "LockedGate";
    }
}