using DungeonCrawl;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Actors.Characters;
using System.Collections.Generic;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;

namespace DungeonCrawl.Serialization
{
    [System.Serializable]
    public class GameDataToSerialize
    {
        public int Map;
        public int x;
        public int y;
        public int Health;
        public string Weapon;
        public string Shield;
        public string Helmet;
        public List<string> SpecialItems = new List<string>();
        public List<string> Consumables = new List<string>();
        public List<ActorToSerialize> AllActors = new List<ActorToSerialize>();

        public GameDataToSerialize(){ }

        public GameDataToSerialize(Player player)
        {
            PopulatePlayerFields(player);
            PopulateInventoryFields(player._inventory);
            PopulateActorList();
        }

        public void PopulateInventoryFields(Inventory inventory)
        {
            Weapon = inventory._weapon ? inventory._weapon.DefaultName : "";
            Shield = inventory._shield ? inventory._shield.DefaultName : "";
            Helmet = inventory._helmet ? inventory._helmet.DefaultName : "";
            foreach (Item item in inventory._specialItems)
                SpecialItems.Add(item.DefaultName);
            foreach (Item item in inventory._consumables)
                Consumables.Add(item.DefaultName);
        }

        public void PopulatePlayerFields(Player player)
        {
            Map = player.Map;
            x = player.Position.x;
            y = player.Position.y;
            Health = player.Health;
        }

        public void PopulateActorList()
        {
            HashSet<Actor> allActors = ActorManager.Singleton.GetAllActors();
            foreach (Actor actor in allActors)
                AllActors.Add(new ActorToSerialize(actor));
        }
    }
}