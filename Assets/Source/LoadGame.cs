using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Serialization;

namespace DungeonCrawl.Load
{
    public static class LoadGame
    {
        public static void LoadLastSavedGame(Player player)
        {
            var gameObject = Serialize.DeserializeGame();
            Player newPlayer = player.Copy();
            ActorManager.Singleton.DestroyAllActors();

            newPlayer.Health = gameObject.Health;
            newPlayer.Position = gameObject.Position;
            newPlayer.Map = gameObject.Map;
            AddPlayerInventory(newPlayer, gameObject);

            MapLoader.LoadMapFromGameObject(gameObject.Map, newPlayer, gameObject);
        }

        public static void AddPlayerInventory (Player player, GameDataToSerialize gameObject)
        {
            var inventory = new Inventory();

            foreach (string itemDefaultName in gameObject.SpecialItems)
            {
                if (itemDefaultName == "Curse Ward Cloak")
                    inventory.AddItem(ActorManager.Singleton.Spawn<CurseWardCloak>((0, 0)));
                if (itemDefaultName == "Magic Gloves")
                    inventory.AddItem(ActorManager.Singleton.Spawn<MagicGloves>((0, 0)));
            }

            foreach (string itemDefaultName in gameObject.Consumables)
            {
                if (itemDefaultName == "Key")
                    inventory.AddItem(ActorManager.Singleton.Spawn<Key>((0, 0)));
                if (itemDefaultName == "HealthKit")
                    inventory.AddItem(ActorManager.Singleton.Spawn<HealthKit>((0, 0)));
            }

            player._inventory = inventory;
        }
    }
}
