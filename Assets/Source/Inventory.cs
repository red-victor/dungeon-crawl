using System.Collections.Generic;
using DungeonCrawl.Actors.Static;
using Assets.Source.Core;
using System.Text;

namespace Assets.Source
{
    public class Inventory
    {
        private Dictionary<string, int> _inventory;

        public Inventory()
        {
            _inventory = new Dictionary<string, int>();
        }

        public void AddItem(StaticActor item)
        {
            if (!_inventory.ContainsKey(item.name))
                _inventory[item.name] = 0;

            _inventory[item.name]++;
            UpdateMessage();
        }

        public void RemoveItem(string itemName)
        {
            if (_inventory[itemName] == 1)
                _inventory.Remove(itemName);
            else
                _inventory[itemName]--;

            UpdateMessage();
        }

        public bool HasItem(string itemName)
        {
            return _inventory.ContainsKey(itemName);
        }

        public void UpdateMessage()
        {
            UserInterface.Singleton.SetText(ToString(), UserInterface.TextPosition.BottomLeft);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Inventory :\n\n");
            foreach(var item in _inventory)
            {
                sb.Append($"{item.Key}: {item.Value}\n");
            }

            return sb.ToString();
        }
    }
}
