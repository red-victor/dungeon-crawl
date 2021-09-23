using System;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Static.Environments
{
    public class Trees : WallType
    {
        private List<int> TreeSpriteList = new List<int>() { 47, 49, 50, 51, 52};
        public override int DefaultSpriteId => 
            TreeSpriteList[Utilities.Random.Next(TreeSpriteList.Count)];
        public override string DefaultName => "Tree";
    }
}
