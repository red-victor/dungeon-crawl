using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Serialization
{
    [System.Serializable]
    public class ActorToSerialize
    {
        public int x;
        public int y;
        public int Health;
        public string DefaultName;
        public ActorToSerialize() { }
        public ActorToSerialize(Actor actor)
        {
            x = actor.Position.x;
            y = actor.Position.y;
            DefaultName = actor.DefaultName;

            if (actor is Character character)
                Health = character.Health;
            else
                Health = -1;
        }


    }
}
