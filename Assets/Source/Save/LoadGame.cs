using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Serialization;
using System;
using UnityEngine;

namespace DungeonCrawl.Load
{
    public static class LoadGame
    {
        public static void Load(Player player)
        {
            try
            {
                var gameObject = Serialize.DeserializeGame();
                Load(player, gameObject);
            }
            catch (Exception)
            {
                Debug.Log("Something went wrong when you tried to import game from the local database. Make sure you have a game saved.");
            }
            finally
            {
                UserInterface.Singleton.SetText("You must Save First!", UserInterface.TextPosition.BottomRight);
            }

        }

        public static void Load(Player player, GameDataToSerialize gameObject)
        {
            Player newPlayer = player.Copy();
            ActorManager.Singleton.DestroyAllActors();

            newPlayer.Health = gameObject.Health;
            newPlayer.Position = (gameObject.x, gameObject.y);
            newPlayer.Map = gameObject.Map;
            AddPlayerInventory(newPlayer, gameObject);

            MapLoader.LoadMapFromGameObject(gameObject.Map, newPlayer, gameObject);
        }

        public static void AddPlayerInventory(Player player, GameDataToSerialize gameObject)
        {
            var inventory = new Inventory();

            // Add saved weapon
            if (gameObject.Weapon == "Dagger")
                inventory.AddItem(ActorManager.Singleton.Spawn<Dagger>((0, 0)));
            if (gameObject.Weapon == "Pike")
                inventory.AddItem(ActorManager.Singleton.Spawn<Pike>((0, 0)));
            if (gameObject.Weapon == "Sword")
                inventory.AddItem(ActorManager.Singleton.Spawn<Sword>((0, 0)));
            if (gameObject.Weapon == "Axe")
                inventory.AddItem(ActorManager.Singleton.Spawn<Axe>((0, 0)));

            // Add saved shield
            if (gameObject.Shield == "Buckler")
                inventory.AddItem(ActorManager.Singleton.Spawn<Buckler>((0, 0)));
            if (gameObject.Shield == "Heater")
                inventory.AddItem(ActorManager.Singleton.Spawn<Heater>((0, 0)));
            if (gameObject.Shield == "War Door")
                inventory.AddItem(ActorManager.Singleton.Spawn<WarDoor>((0, 0)));

            // Add saved helmet
            if (gameObject.Helmet == "IronHat")
                inventory.AddItem(ActorManager.Singleton.Spawn<IronHat>((0, 0)));
            if (gameObject.Helmet == "GreatHelm")
                inventory.AddItem(ActorManager.Singleton.Spawn<GreatHelm>((0, 0)));

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
