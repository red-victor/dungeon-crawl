using Assets.Source;
using Assets.Source.Core;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Actors.Static.Environments;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using System.Text;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        public override int Health { get; protected set; } = 20;

        public override int BaseDamage { get;} = 5;

        private Inventory _inventory = new Inventory();

        private int DamageModifier;

        private int DamageReduction;

        protected override void OnUpdate(float deltaTime)
        {
            UpdateStats();
            DisplayStats();

            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                // Pick up Item
                PickUp();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AttemptOpenGate();
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        public override void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
            {
                UserInterface.Singleton.RemoveText(UserInterface.TextPosition.BottomRight);
                // No obstacle found, just move
                Position = targetPosition;
            }
            else
            {
                if (actorAtTargetPosition.OnCollision(this))
                {
                    if (((StaticActor)actorAtTargetPosition).IsPickable)
                        UserInterface.Singleton.SetText("Press E to pick up", UserInterface.TextPosition.BottomRight);
                    // Allowed to move
                    Position = targetPosition;
                }
                else
                {
                    if (actorAtTargetPosition is Character enemy)
                    {
                        Attack(enemy);
                    }
                }
            }
        }

        private void Attack(Character enemy)
        {
            enemy.ApplyDamage(BaseDamage + DamageModifier);

            if(enemy.Health > 0)
            {
                ApplyDamage(enemy.BaseDamage - DamageReduction);
            }
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        private void PickUp()
        {
            var item = ActorManager.Singleton.GetActorAt<StaticActor>(Position);

            if (item != null && item.IsPickable)
            {
                UserInterface.Singleton.RemoveText(UserInterface.TextPosition.BottomRight);
                _inventory.AddItem(item);
                //ActorManager.Singleton.DestroyActor(item);
                item.Position = (-5, 1);
            }
        }

        private void AttemptOpenGate()
        {
            AdjecentCoordinates adjecentCoordinatesCreator = new AdjecentCoordinates(Position);
            var adjecentCoordinates = adjecentCoordinatesCreator.GetAdjecentCoordinates();

            for (int i = 0; i < 4; i++)
            {
                var nextCell = ActorManager.Singleton.GetActorAt<Actor>(adjecentCoordinates[i]);

                if (nextCell != null && nextCell is LockedGate lockedGate)
                {
                    if (_inventory.HasKey())
                    {
                        _inventory.RemoveKey();
                        lockedGate.OpenGate();
                    }
                    else
                    {
                        UserInterface.Singleton.SetText("Need a Key!", UserInterface.TextPosition.BottomRight);
                    }
                }
            }
        }

        public void UpdateStats()
        {
            DamageModifier = _inventory.AttackPower;
            DamageReduction = _inventory.Defense;
        }

        public void DisplayStats()
        {
            StringBuilder sb = new StringBuilder($"{DefaultName} :\n\n");
            sb.Append($"Health: {Health}\n");
            sb.Append($"Attack Power: {BaseDamage + DamageModifier}\n");
            sb.Append($"Defense: {DamageReduction}\n");
            UserInterface.Singleton.SetText(sb.ToString(), UserInterface.TextPosition.TopRight);
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
