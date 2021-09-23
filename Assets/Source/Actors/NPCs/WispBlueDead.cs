namespace DungeonCrawl.Actors.Characters
{
    public class WispBlueDead : NPC
    {
        public bool IsAlive = true;
        public override bool OnCollision(Actor anotherActor) => false;

        protected override void OnDeath()
        {
        }

        public override int DefaultSpriteId { get; protected set; } = 358;
        public override string DefaultName => "Dead Blue Wisp";
    }
}
