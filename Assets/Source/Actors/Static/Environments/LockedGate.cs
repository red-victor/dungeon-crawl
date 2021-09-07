using DungeonCrawl.Core;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Actors.Static.Environments
{
    public class LockedGate : Environment
    {
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        public void OpenGate()
        {
            ActorManager.Singleton.DestroyActor(this);
            ActorManager.Singleton.Spawn<OpenedGate>(this.Position);
        }

        public override int DefaultSpriteId => 540;
        public override string DefaultName => "LockedGate";
    }
}