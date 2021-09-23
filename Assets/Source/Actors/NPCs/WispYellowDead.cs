namespace DungeonCrawl.Actors.Characters
{
    public class WispYellowDead : NPC
    {
        public bool IsAlive = true;
        public override bool OnCollision(Actor anotherActor) => false;

        protected override void OnDeath()
        {
        }

        public override int DefaultSpriteId { get; protected set; } = 406;
        public override string DefaultName => "Dead Yellow Wisp";
    }
}
