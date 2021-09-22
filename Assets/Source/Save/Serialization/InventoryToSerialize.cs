using System.Collections.Generic;
using DungeonCrawl;
using DungeonCrawl.Actors.Static.Items;

namespace Assets.Source.Save.Serialization
{
    [System.Serializable]
    public class SerializedInventory
    {
        public Weapon Weapon;
        public Shield Shield;
        public Helmet Helmet;
        public List<Item> SpecialItems;
        public List<Consumable> Consumables;

        public SerializedInventory(Inventory inventory)
        {
            Weapon = inventory._weapon ? inventory._weapon : null;
            Shield = inventory._shield ? inventory._shield : null;
            Helmet = inventory._helmet ? inventory._helmet : null;
            SpecialItems = inventory._specialItems;
            Consumables = inventory._consumables;
        }
    }
}