using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Static.Environments
{
    public class OpenedGate : Environment
    {
        public override int DefaultSpriteId => 539;
        public override string DefaultName => "OpenedGate";

        public override bool OnCollision(Actor player)
        {
            var playerCopy = ((Player)player).Copy();
            playerCopy.Map++;
            ActorManager.Singleton.DestroyAllActors();
            Debug.Log(playerCopy.Map);
            MapLoader.LoadMap(playerCopy.Map, playerCopy);

            return true;
        }
    }
}