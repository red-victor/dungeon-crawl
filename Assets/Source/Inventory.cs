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
        private Weapon _weapon = null;
        private List<Armour> _armor;
        private List<Consumable> _consumables;

        public int Defense;
        public int AttackPower;
        public string Message;

        public Inventory()
        {
            AttackPower = 0;
            _armor = new List<Armour>();
            _consumables = new List<Consumable>();
            Message = ToString();
        }

        public void AddItem(StaticActor item)
        {
            if (item is Weapon weapon)
            {
                if (_weapon != null)
                {
                    DiscardItem(_weapon, item.Position);
                }

                if (weapon is Axe axe)
                    _weapon = Copy<Axe>(axe);
                if (weapon is Sword sword)
                    _weapon = Copy<Sword>(sword);
            }
            if (item is Armour armour)
                if (armour is LeatherShield leatherShield)
                    _armor.Add(Copy<LeatherShield>(leatherShield));

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

        private void DiscardItem(Item item, (int x, int y)position)
        {
            if (item is Sword)
                ActorManager.Singleton.Spawn<Sword>(position);
            if (item is Axe)
                ActorManager.Singleton.Spawn<Axe>(position);
            if (item is LeatherShield)
                ActorManager.Singleton.Spawn<LeatherShield>(position);
        }

        public T Copy<T>(T item) where T : Actor
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            var component = go.AddComponent<T>();
            go.name = item.DefaultName;
            Debug.Log(component);
            component.Position = (-999, -999);
            return component;
        }

        public void RemoveItem(Item item)
        {
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
            if (_weapon == null)
                AttackPower = 0;
            else
                AttackPower = _weapon.AttackPower;
            Defense = 0;

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

            if(_weapon != null)
                sb.Append($"Weapon: {_weapon.DefaultName}\n");

            foreach (var item in _armor.Distinct())
                sb.Append($"{item.DefaultName}: {_armor.Where(x => x.DefaultName.Equals(item.DefaultName)).Count()}\n");

            foreach (var item in _consumables.Distinct())
                sb.Append($"{item.DefaultName}: {_consumables.Where(x => x.DefaultName.Equals(item.DefaultName)).Count()}\n");

            return sb.ToString();
        }
    }
}
