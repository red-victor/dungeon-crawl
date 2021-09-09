using DungeonCrawl.Actors.Static;
using DungeonCrawl.Actors.Static.Environments;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;
using System.Text;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        public CameraController Camera = CameraController.Singleton;
        public Inventory _inventory { get; private set; } = new Inventory();
        public override int Health { get; protected set; } = 20;
        public override int BaseDamage { get;} = 5;
        public bool Protected { get; private set; } = false;

        private int DamageModifier, DamageReduction;
        public int Map = 1;

        private void Start()
        {
            Camera.Position = this.Position;
            //_camera.Size -= 2;
        }

        protected override void OnUpdate(float deltaTime)
        {
            UpdateStats();
            DisplayStats();

            if (Input.GetKeyDown(KeyCode.W))
                TryMove(Direction.Up);

            if (Input.GetKeyDown(KeyCode.S))
                TryMove(Direction.Down);

            if (Input.GetKeyDown(KeyCode.A))
                TryMove(Direction.Left);

            if (Input.GetKeyDown(KeyCode.D))
                TryMove(Direction.Right);

            if (Input.GetKeyDown(KeyCode.E))
                PickUp();

            if (Input.GetKeyDown(KeyCode.Space))
                AttemptOpenGate();

            if (Input.GetKeyDown(KeyCode.Q))
                AttemptHeal();
        }

        public override bool OnCollision(Actor anotherActor) => false;

        protected override void Attack(Character enemy)
        {
            enemy.ApplyDamage(BaseDamage + DamageModifier);

            if(enemy.Health > 0 && enemy.BaseDamage - DamageReduction > 0)
                ApplyDamage(enemy.BaseDamage - DamageReduction);
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        private void PickUp()
        {
            var item = ActorManager.Singleton.GetActorAt<StaticActor>(Position);

            if (item != null && item.CanPickUp)
            {
                UserInterface.Singleton.RemoveText(UserInterface.TextPosition.BottomRight);
                AudioManager.Singleton.Play("PickUp");
                _inventory.AddItem((Item)item);
            }
        }

        private void AttemptOpenGate()
        {
            var adjecentCoordinates = new AdjecentCoordinates(Position).GetAdjecentCoordinates();

            for (int i = 0; i < 4; i++)
            {
                var nextCell = ActorManager.Singleton.GetActorAt<Actor>(adjecentCoordinates[i]);

                if (nextCell != null && nextCell is LockedGate lockedGate)
                {
                    if (_inventory.HasConsumable("Key"))
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

        private void AttemptHeal()
        {
            var healthKit = (HealthKit)_inventory.GetConsumable("HealthKit");

            if (healthKit != null)
            {
                Health += healthKit.Heal;
                _inventory.RemoveItem(healthKit);
                UserInterface.Singleton.SetText("Das gud! MEIN LEBEN!", UserInterface.TextPosition.BottomRight);
            }
            else
            {
                UserInterface.Singleton.SetText("Nein! Ich haben nicht!", UserInterface.TextPosition.BottomRight);
            }
        }

        public void UpdateStats()
        {
            DamageModifier = _inventory.AttackPower;
            DamageReduction = _inventory.Defense;
            if (!Protected && _inventory.HasSpecialItem("Curse Ward Cloak"))
                Protected = true;
        }

        public void DisplayStats()
        {
            StringBuilder sb = new StringBuilder($"{DefaultName} :\n\n");
            sb.Append($"Health: {Health}\n");
            sb.Append($"Attack Power: {BaseDamage + DamageModifier}\n");
            sb.Append($"Defense: {DamageReduction}\n");
            UserInterface.Singleton.SetText(sb.ToString(), UserInterface.TextPosition.TopRight);

            UserInterface.Singleton.SetText(_inventory.ToString(), UserInterface.TextPosition.TopLeft);
        }

        public Player Copy()
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            var component = go.AddComponent<Player>();
            go.name = component.DefaultName;
            component.Health = Health;
            component._inventory = _inventory;
            component.Map = this.Map;
            return component;
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
