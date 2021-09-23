namespace DungeonCrawl.Actors.Characters
{
    public class WispGreenDead : NPC
    {
        public bool IsAlive = true;
        public override bool OnCollision(Actor anotherActor) => false;

        protected override void OnDeath()
        {
        }

        public override int DefaultSpriteId { get; protected set; } = 454;
        public override string DefaultName => "Dead Green Wisp";
    }
}
