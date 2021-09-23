using System.Collections.Generic;
using System.Text;
using System.Linq;
using DungeonCrawl.Core;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Static.Items;
using UnityEngine;
using DungeonCrawl.Core.Audio;

namespace DungeonCrawl
{
    public class Inventory
    {
        public Weapon _weapon = null;
        public Shield _shield = null;
        public Helmet _helmet = null;
        public List<Item> _specialItems = new List<Item>();
        public List<Consumable> _consumables = new List<Consumable>();

        public int Defense = 0;
        public int AttackPower = 0;
        public string Message;

        public Inventory()
        {
            Message = ToString();
        }

        public void AddItem(Item item)
        {
            if (item is Weapon weapon)
            {
                if (_weapon != null)
                    DiscardItem(_weapon, item.Position);

                if (weapon is Dagger dagger)
                    _weapon = Copy<Dagger>(dagger);
                if (weapon is Pike pike)
                    _weapon = Copy<Pike>(pike);
                if (weapon is Sword sword)
                    _weapon = Copy<Sword>(sword);
                if (weapon is Axe axe)
                    _weapon = Copy<Axe>(axe);

                AudioManager.Singleton.Play("Shing1");
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

                    AudioManager.Singleton.Play("Shield");
                }

                if (armour is Helmet helmet)
                {
                    if (_helmet != null)
                        DiscardItem(_helmet, item.Position);

                    if (helmet is IronHat ironHat)
                        _helmet = Copy<IronHat>(ironHat);
                    if (helmet is GreatHelm greatHelm)
                        _helmet = Copy<GreatHelm>(greatHelm);

                    AudioManager.Singleton.Play("Helmet");
                }

                if (armour is CurseWardCloak cloak)
                {
                    _specialItems.Add(cloak); 
                    AudioManager.Singleton.Play("Cloth2");
                }

                if (armour is MagicGloves gloves)
                {
                    _specialItems.Add(gloves);
                    AudioManager.Singleton.Play("Cloth1");
                }
            }
            if (item is Consumable consumable)
            {
                if (consumable is HealthKit healthKit)
                {
                    _consumables.Add(Copy(healthKit));
                    if (HasSpecialItem("Magic Gloves"))
                        _consumables.Add(Copy(healthKit));
                    AudioManager.Singleton.Play("PickUpBottle");
                }
                if (consumable is Key key)
                {
                    _consumables.Add(Copy(key));
                    AudioManager.Singleton.Play("PickUp");
                }
            }

            ActorManager.Singleton.DestroyActor(item);
            UpdateStatModifiers();
            UpdateMessage();
        }

        private void DiscardItem(Item item, (int x, int y)position)
        {
            if (item is Dagger)     ActorManager.Singleton.Spawn<Dagger>        (position);
            if (item is Pike)       ActorManager.Singleton.Spawn<Pike>          (position);
            if (item is Sword)      ActorManager.Singleton.Spawn<Sword>         (position);
            if (item is Axe)        ActorManager.Singleton.Spawn<Axe>           (position);
            if (item is Buckler)    ActorManager.Singleton.Spawn<Buckler>       (position);
            if (item is Heater)     ActorManager.Singleton.Spawn<Heater>        (position);
            if (item is WarDoor)    ActorManager.Singleton.Spawn<WarDoor>       (position); 
            if (item is IronHat)    ActorManager.Singleton.Spawn<IronHat>       (position);
            if (item is GreatHelm)  ActorManager.Singleton.Spawn<GreatHelm>     (position);
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

        public bool HasConsumable(string itemName)
        {
            return _consumables.Any(inventoryItem => inventoryItem.DefaultName == itemName);
        }

        public bool HasSpecialItem(string itemName)
        {
            return _specialItems.Any(inventoryItem => inventoryItem.DefaultName == itemName);
        }

        public Item GetConsumable(string itemName)
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

            if (HasConsumable("HealthKit"))
                sb.Append($"HealthKits: {_consumables.Where(x => x.DefaultName.Equals("HealthKit")).Count()}\n");

            if (HasConsumable("Key"))
                sb.Append($"Keys: {_consumables.Where(x => x.DefaultName.Equals("Key")).Count()}\n");

            return sb.ToString();
        }
    }
}
