using System;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Static.Environments
{
    public class House : WallType
    {
        private List<int> TreeSpriteList = new List<int>() { 911, 912, 963, 965, 966, 967 };
        public override int DefaultSpriteId =>
            TreeSpriteList[Utilities.Random.Next(TreeSpriteList.Count)];
        public override string DefaultName => "House";
    }
}
