using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Serialization
{
    [System.Serializable]
    public class ActorToSerialize
    {
        public (int x, int y) Position;
        public int Health;
        public string DefaultName;
        public ActorToSerialize() { }
        public ActorToSerialize(Actor actor)
        {
            Position = actor.Position;
            DefaultName = actor.DefaultName;

            if (actor is Character character)
                Health = character.Health;
            else
                Health = -1;
        }


    }
}
