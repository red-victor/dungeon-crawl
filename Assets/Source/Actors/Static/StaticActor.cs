namespace DungeonCrawl.Actors.Static
{
    public abstract class StaticActor : Actor
    {
        public override bool Detectable => true;

        public virtual bool IsPickable => false;
    }
}
