using DungeonCrawl.Core;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Actors.Static.Environments
{
    public class LockedGate : Environment
    {
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player player)
            {
                if (player.HasItemInInventory("Key"))
                {
                    player.RemoveItemFromInventory("Key");
                    ActorManager.Singleton.DestroyActor(this);
                    ActorManager.Singleton.Spawn<OpenedGate>(this.Position);
                }
            }
            return false;
        }

        public override int DefaultSpriteId => 540;
        public override string DefaultName => "LockedGate";
    }
}