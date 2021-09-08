using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Character : Actor
    {
        public abstract int Health { get; protected set; }
        
        public abstract int BaseDamage {get;}

        public void ApplyDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                OnDeath();
                ActorManager.Singleton.DestroyActor(this);
            }
        }

        public override void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
            {
                // No obstacle found, just move
                Position = targetPosition;
            }
            else
            {
                if (actorAtTargetPosition.OnCollision(this))
                {
                    // Allowed to move
                    Position = targetPosition;
                }
                else
                {
                    if (actorAtTargetPosition is Player player)
                    {
                        Attack(player);
                    }
                }
            }
        }

        protected virtual void Attack(Character player)
        {
            player.ApplyDamage(BaseDamage);

            if (player.Health > 0)
                ApplyDamage(player.BaseDamage);
        }

        protected abstract void OnDeath();

        /// <summary>
        ///     All characters are drawn "above" floor and above items
        /// </summary>
        public override int Z => -2;
    }
}
