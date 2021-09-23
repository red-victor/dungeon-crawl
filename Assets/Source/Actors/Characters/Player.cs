using System;
using System.Text;
using UnityEngine;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Actors.Static.Environments;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Core.Audio;
using DungeonCrawl.Serialization;
using DungeonCrawl.Load;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        public CameraController Camera = CameraController.Singleton;
        public Inventory _inventory { get; set; } = new Inventory();
        public override int Health { get; set; } = 30;
        public override int BaseDamage { get;} = 1;
        public bool Protected { get; private set; } = false;

        public int DamageModifier, DamageReduction;

        public int Map = 1;

        private void Start()
        {
            Camera.Position = this.Position;
            Camera.Size -= 2;
            gameObject.tag = "Player";
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (!PauseMenu.GameIsPaused)
            {
                UpdateStats();
                DisplayStats();
                SetAppearance();

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

                if (Input.GetKeyDown(KeyCode.T))
                    Health += 20;

                if (Input.GetKeyDown(KeyCode.Alpha1))
                    Serialize.SaveGameToFile(this);

                if (Input.GetKeyDown(KeyCode.Alpha2))
                    LoadGame.Load(this);
            }
        }

        

        public override bool OnCollision(Actor anotherActor) => false;

        protected override void Attack(Character enemy)
        {
            enemy.ApplyDamage(BaseDamage + DamageModifier);

            if(enemy.Health > 0)
                ApplyDamage(enemy.BaseDamage - enemy.BaseDamage * (DamageReduction / 100));
        }

        protected override void OnDeath()
        {
            AudioManager.Singleton.Play("GameOver");
            FindObjectOfType<State>().LoadGameOver();
            Debug.Log("Oh no, I'm dead!");
            Destroy(gameObject);
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
            var adjecentCoordinates = Utilities.GetAdjecentCoordinates(Position);

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
            sb.Append($"Deffense: {DamageReduction}%\n");
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

        private void SetAppearance()
        {
            if (_inventory._weapon != null && _inventory._weapon.DefaultName == "Pike")
                if (_inventory._shield != null && _inventory._helmet != null)
                    SetSprite(28);
                else
                    SetSprite(25);
            else if (_inventory._weapon != null && _inventory._weapon.DefaultName == "Sword")
                if (_inventory._shield != null)
                {
                    if (_inventory._helmet != null)
                        SetSprite(27);
                    else
                        SetSprite(26);
                }
            else
                SetSprite(DefaultSpriteId);
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
