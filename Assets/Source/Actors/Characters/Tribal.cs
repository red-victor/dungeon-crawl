using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;

namespace DungeonCrawl.Actors.Characters
{
    public class Tribal : Character
    {
        public override int Health { get; set; } = 10;
        public override int BaseDamage { get; } = 4;

        void Start()
        {
            InvokeRepeating("Pursue", 1.0f, 0.7f);
        }

        public override bool OnCollision(Actor anotherActor) => false;

        void Pursue()
        {
            var direction = GetTargetDirection((29, -25));
            TryMove(direction);
        }

        public override void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
                Position = targetPosition;
            else
                if (actorAtTargetPosition.OnCollision(this))
                    Position = targetPosition;
                else
                    if (actorAtTargetPosition is Character character)
                        Attack(character);
        }

        protected override void OnDeath()
        {
            int percent = new System.Random().Next(0, 100);

            if (percent < 3)
                ActorManager.Singleton.Spawn<Sword>(Position);
            else if (percent < 6)
                ActorManager.Singleton.Spawn<Buckler>(Position);

            AudioManager.Singleton.Play("SkeletonDeath");
        }

        public override int DefaultSpriteId => 123;
        public override string DefaultName => "Tribal";
    }
}