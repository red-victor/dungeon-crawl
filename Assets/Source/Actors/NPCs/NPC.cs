using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Actors
{
    public abstract class NPC : Actor
    {
        public override int Z => -2;

        protected abstract void OnDeath();
    }
}
