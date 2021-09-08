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
using DungeonCrawl.Actors.Static.Items.Armour.Shields;
using DungeonCrawl.Actors.Static.Items.Armour.Helmets;

namespace Assets.Source
{
    public class Inventory
    {
        private Weapon _weapon = null;
        private Shield _shield = null;
        private Helmet _helmet = null;
        private List<Item> _specialItems = new List<Item>();
        private List<Consumable> _consumables = new List<Consumable>();

        public int Defense = 0;
        public int AttackPower = 0;
        public string Message;

        public Inventory()
        {
            Message = ToString();
        }

        public void AddItem(StaticActor item)
        {
            if (item is Weapon weapon)
            {
                if (_weapon != null)
                    DiscardItem(_weapon, item.Position);

                if (weapon is Axe axe)
                    _weapon = Copy<Axe>(axe);
                if (weapon is Sword sword)
                    _weapon = Copy<Sword>(sword);
            }
            if (item is Armour armour)
            { 
                if (armour is Shield shield)
                {
                    if (_shield != null)
                        DiscardItem(_shield, item.Position);

                    if (shield is Buckler buckler)
                        _shield = Copy<Buckler>(buckler);
                    if (shield is Heater heater)
                        _shield = Copy<Heater>(heater);
                    if (shield is WarDoor warDoor)
                        _shield = Copy<WarDoor>(warDoor);
                }

                if (armour is Helmet helmet)
                {
                    if (_helmet != null)
                        DiscardItem(_helmet, item.Position);

                    if (helmet is IronHat ironHat)
                        _helmet = Copy<IronHat>(ironHat);
                    if (helmet is GreatHelm greatHelm)
                        _helmet = Copy<GreatHelm>(greatHelm);
                }

                if (armour is CurseWardCloak cloak)
                    _specialItems.Add(cloak);

            }
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
            if (item is Buckler)
                ActorManager.Singleton.Spawn<Buckler>(position);
            if (item is Heater)
                ActorManager.Singleton.Spawn<Heater>(position);
            if (item is WarDoor)
                ActorManager.Singleton.Spawn<WarDoor>(position); 
            if (item is IronHat)
                ActorManager.Singleton.Spawn<IronHat>(position);
            if (item is GreatHelm)
                ActorManager.Singleton.Spawn<GreatHelm>(position);
        }

        public T Copy<T>(T item) where T : Actor
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            var component = go.AddComponent<T>();
            go.name = item.DefaultName;
            component.Position = (-999, -999);
            return component;
        }

        public void RemoveItem(Item item)
        {
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

        public bool HasSpecialItem(string itemName)
        {
            return _specialItems.Any(inventoryItem => inventoryItem.DefaultName == itemName);
        }

        public bool HasKey()
        {
            return _consumables.Any(item => item.DefaultName == "Key");
        }

        public Item GetItem(string itemName)
        {
            foreach (Consumable item in _consumables)
                if (item.DefaultName == itemName)
                    return item;
            return null;
        }

        public void UpdateStatModifiers()
        {
            AttackPower = 0;
            Defense = 0;

            if (_weapon != null)
                AttackPower += _weapon.AttackPower;

            if (_shield != null)
                Defense += _shield.Defense;

            if (_helmet != null)
                Defense += _helmet.Defense;
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

            if (_shield != null)
                sb.Append($"Shield: {_shield.DefaultName}\n");

            if (_helmet != null)
                sb.Append($"Helmet: {_helmet.DefaultName}\n");

            foreach (var item in _specialItems)
                sb.Append($"{item.DefaultName}\n");

            foreach (var item in _consumables.Distinct())
                sb.Append($"{item.DefaultName}: {_consumables.Where(x => x.DefaultName.Equals(item.DefaultName)).Count()}\n");

            return sb.ToString();
        }
    }
}
