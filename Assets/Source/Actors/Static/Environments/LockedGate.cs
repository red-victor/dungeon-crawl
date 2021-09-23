using DungeonCrawl.Core;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core.Audio;

namespace DungeonCrawl.Actors.Static.Environments
{
    public class LockedGate : Environment
    {
        public override bool OnCollision(Actor anotherActor) => false;

        public void OpenGate()
        {
            ActorManager.Singleton.DestroyActor(this);
            ActorManager.Singleton.Spawn<OpenedGate>(this.Position);
            AudioManager.Singleton.Play("Door");
        }

        public override int DefaultSpriteId => 540;
        public override string DefaultName => "LockedGate";
    }
}