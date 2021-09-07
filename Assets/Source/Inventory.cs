using System.Collections.Generic;
using DungeonCrawl.Actors.Static;
using Assets.Source.Core;
using System.Text;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Actors.Static.Items.Weapons;
using DungeonCrawl.Actors.Static.Items.Armour;
using DungeonCrawl.Actors.Static.Items.Consumables;
using System.Linq;

namespace Assets.Source
{
    public class Inventory
    {
        private List<Weapon> _weapons;
        private List<Armour> _armor;
        private List<Consumable> _consumables;

        public int Defense;
        public int AttackPower;

        public Inventory()
        {
            _weapons = new List<Weapon>();
            _armor = new List<Armour>();
            _consumables = new List<Consumable>();
        }

        public void AddItem(StaticActor item)
        {
            if (item is Weapon weapon)
                _weapons.Add(weapon);
            if (item is Armour armour)
                _armor.Add(armour);
            if (item is Consumable consumable)
                _consumables.Add(consumable);

            UpdateStatModifiers();
            UpdateMessage();
        }

        public void RemoveItem(Item item)
        {
            if (item is Weapon weapon)
                _weapons.Remove(weapon);
            if (item is Armour armour)
                _armor.Remove(armour);
            if (item is Consumable consumable)
                _consumables.Remove(consumable);

            UpdateStatModifiers();
            UpdateMessage();
        }

        public void RemoveKey()
        {
            foreach (Item item in _consumables)
                if (item is Key key)
                {
                    _consumables.Remove(key);
                    break;
                }
        }

        public bool HasItem(Item item)
        {
            return _consumables.Any(inventoryItem => inventoryItem.DefaultName == item.DefaultName);
        }

        public bool HasKey()
        {
            return _consumables.Any(item => item.DefaultName == "Key");
        }

        public void UpdateStatModifiers()
        {
            AttackPower = 0;
            Defense = 0;

            foreach(Weapon weapon in _weapons)
            {
                AttackPower += weapon.AttackPower;
            }

            foreach (Armour armour in _armor)
            {
                Defense += armour.Defense;
            }
        }

        public void UpdateMessage()
        {
            UserInterface.Singleton.SetText(ToString(), UserInterface.TextPosition.TopLeft);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Inventory :\n\n");
            sb.Append("\nWeapons :\n\n");

            foreach (var item in _weapons.Distinct())
            {
                sb.Append($"{item.name}: {_weapons.Where(x => x.Equals(item)).Count()}\n");
            }

            sb.Append("\nArmor :\n\n");

            foreach (var item in _armor.Distinct())
            {
                sb.Append($"{item.name}: {_armor.Where(x => x.Equals(item)).Count()}\n");
            }

            sb.Append("\nConsumables :\n\n");

            foreach (var item in _consumables.Distinct())
            {
                sb.Append($"{item.name}: {_consumables.Where(x => x.Equals(item)).Count()}\n");
            }

            return sb.ToString();
        }
    }
}
