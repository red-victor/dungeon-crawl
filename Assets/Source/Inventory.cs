using System.Collections.Generic;
using DungeonCrawl.Actors.Static;
using Assets.Source.Core;
using System.Text;
using DungeonCrawl.Core;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Actors.Static.Items.Weapons;
using DungeonCrawl.Actors.Static.Items.Armour;
using DungeonCrawl.Actors.Static.Items.Consumables;
using System.Linq;
using DungeonCrawl.Actors;
using UnityEngine;

namespace Assets.Source
{
    public class Inventory
    {
        private List<Weapon> _weapons;
        private List<Armour> _armor;
        private List<Consumable> _consumables;

        public int Defense;
        public int AttackPower;
        public string Message;

        public Inventory()
        {
            _weapons = new List<Weapon>();
            _armor = new List<Armour>();
            _consumables = new List<Consumable>();
            Message = ToString();
        }

        public void AddItem(StaticActor item)
        {
            if (item is Weapon weapon)
            {
                if (weapon is Axe axe)
                    _weapons.Add(Copy(axe));

                if (weapon is Sword sword)
                    _weapons.Add(Copy(sword));
            }
            if (item is Armour armour)
                if (armour is LeatherShield leatherShield)
                    _armor.Add(Copy(leatherShield));

            if (item is Consumable consumable)
            {
                if (consumable is HealthKit healthKit)
                    _consumables.Add(Copy(healthKit));
                if (consumable is Key key)
                    _consumables.Add(Copy(key));
            }

            ActorManager.Singleton.DestroyActor(item);
            UpdateStatModifiers();
            UpdateMessage();
        }

        public T Copy<T>(T item) where T : Actor
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            var component = go.AddComponent<T>();
            go.name = component.DefaultName;
            component.Position = (-999, -999);
            return component;
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

        public Item GetItem(string itemName)
        {
            foreach (Weapon item in _weapons)
                if (item.DefaultName == itemName)
                    return item;
            foreach (Armour item in _armor)
                if (item.DefaultName == itemName)
                    return item;
            foreach (Consumable item in _consumables)
                if (item.DefaultName == itemName)
                    return item;
            return null;
        }

        public void UpdateStatModifiers()
        {
            AttackPower = 0;
            Defense = 0;

            foreach(Weapon weapon in _weapons)
                AttackPower += weapon.AttackPower;

            foreach (Armour armour in _armor)
                Defense += armour.Defense;
        }

        public void UpdateMessage()
        {
            Message = ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Inventory :\n\n");

            foreach (var item in _weapons.Distinct())
                sb.Append($"{item.DefaultName}: {_weapons.Where(x => x.DefaultName.Equals(item.DefaultName)).Count()}\n");

            foreach (var item in _armor.Distinct())
                sb.Append($"{item.DefaultName}: {_armor.Where(x => x.DefaultName.Equals(item.DefaultName)).Count()}\n");

            foreach (var item in _consumables.Distinct())
                sb.Append($"{item.DefaultName}: {_consumables.Where(x => x.DefaultName.Equals(item.DefaultName)).Count()}\n");

            return sb.ToString();
        }
    }
}
