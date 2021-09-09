using System;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Static.Environments
{
    public class House : WallType
    {
        private List<int> TreeSpriteList = new List<int>() { 965, 966, 967 };
        public override int DefaultSpriteId =>
            TreeSpriteList[new Random().Next(TreeSpriteList.Count)];
        public override string DefaultName => "House";
    }
}
