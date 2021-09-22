using DungeonCrawl;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Serialization
{
    [System.Serializable]
    public class PlayerToSerialize
    {
        public int Map;
        public (int x, int y) Position;
        public int Health;
        public string Weapon;
        public string Shield;
        public string Helmet;
        public string[] SpecialItems;
        public string[] Consumables;

        public PlayerToSerialize()
        {
        }

        public PlayerToSerialize(Player player)
        {
            PopulatePlayerFields(player);
            PopulateInventoryFields(player._inventory);
        }

        public void PopulateInventoryFields(Inventory inventory)
        {
            Weapon = inventory._weapon ? inventory._weapon.DefaultName : "";
            Shield = inventory._shield ? inventory._shield.DefaultName : "";
            Helmet = inventory._helmet ? inventory._helmet.DefaultName : "";
            SpecialItems = new string[inventory._specialItems.Count];
            Consumables = new string[inventory._consumables.Count];

            var index = 0;
            foreach (Item item in inventory._specialItems)
            {
                SpecialItems[index] = item.DefaultName;
                index++;
            }

            index = 0;
            foreach (Item item in inventory._consumables)
            {
                Consumables[index] = item.DefaultName;
                index++;
            }
        }

        public void PopulatePlayerFields(Player player)
        {
            Map = player.Map;
            Position = player.Position;
            Health = player.Health;
        }
    }
}