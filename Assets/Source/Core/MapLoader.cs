using System;
using System.Text.RegularExpressions;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl;
using DungeonCrawl.Actors.Static.Environments;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Serialization;
using UnityEngine;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     MapLoader is used for constructing maps from txt files
    /// </summary>
    public static class MapLoader
    {
        public static int CurrentMap;
        /// <summary>
        ///     Constructs map from txt file and spawns actors at appropriate positions
        /// </summary>
        /// <param name="id"></param>
        public static void LoadMap(int id, Player player = null)
        {
            if (id == 6)
            {
                bool isGameFinished = true;
                State.FindObjectOfType<State>().LoadGameOver(isGameFinished);
                return;
            }
            CurrentMap = id;
            var lines = Regex.Split(Resources.Load<TextAsset>($"map_{id}").text, "\r\n|\r|\n");

            // Read map size from the first line
            var split = lines[0].Split(' ');
            var width = int.Parse(split[0]);
            var height = int.Parse(split[1]);

            // Create actors
            for (var y = 0; y < height; y++)
            {
                var line = lines[y + 1];
                for (var x = 0; x < width; x++)
                {
                    var character = line[x];

                    if (player != null && character == 'p')
                        SpawnPlayer(player, (x, -y));
                    else                        
                        SpawnActor(character, (x, -y));
                }
            }

            // Set default camera size and position
            CameraController.Singleton.Size = 10;
            CameraController.Singleton.Position = (width / 2, -height / 2);
        }

        /// <summary>
        ///     Constructs map from deserialized game object and spawns actors at appropriate positions
        /// </summary>
        /// <param name="id"></param>
        public static void LoadMapFromGameObject(int id, Player player, GameDataToSerialize gameObject)
        {
            CurrentMap = id;
            var lines = Regex.Split(Resources.Load<TextAsset>($"map_{id}").text, "\r\n|\r|\n");

            // Read map size from the first line
            var split = lines[0].Split(' ');
            var width = int.Parse(split[0]);
            var height = int.Parse(split[1]);

            foreach (var actor in gameObject.AllActors)
            {
                SpawnActorFromGameObject(actor.DefaultName, actor.Position);
            }

            SpawnPlayer(player, player.Position, true);

            // Set default camera size and position
            CameraController.Singleton.Size = 10;
            CameraController.Singleton.Position = (width / 2, -height / 2);
        }

        private static void SpawnActor(char c, (int x, int y) position)
        {
            switch (c)
            {
                case '#': ActorManager.Singleton.Spawn<Wall>            (position); return;
                case 'V': ActorManager.Singleton.Spawn<WallWindow>      (position); return;
                case '$': ActorManager.Singleton.Spawn<Trees>           (position); return;
                case '{': ActorManager.Singleton.Spawn<DeadTree>        (position); return;
                case '@': ActorManager.Singleton.Spawn<ShutGate>        (position); return;
                case '^': ActorManager.Singleton.Spawn<House>           (position); return;
                case '.': ActorManager.Singleton.Spawn<Floor>           (position); return;
                case ',': ActorManager.Singleton.Spawn<Grass>           (position); return;
                case '|': ActorManager.Singleton.Spawn<RoadVertical>    (position); return;
                case 'd': ActorManager.Singleton.Spawn<LockedGate>      (position); return;
                case 'p': ActorManager.Singleton.Spawn<Player>          (position);
                    SpawnPlayerFloor(position);                                     return;
                case 'W': ActorManager.Singleton.Spawn<Wasp>            (position); break;
                case 'P': ActorManager.Singleton.Spawn<Peasant>         (position); break;
                case 'w': ActorManager.Singleton.Spawn<WispBlue>        (position); break;
                case 'G': ActorManager.Singleton.Spawn<WispGreen>       (position); break;
                case 'y': ActorManager.Singleton.Spawn<WispYellow>      (position); break;
                case 's': ActorManager.Singleton.Spawn<Skeleton>        (position); break;
                case 'T': ActorManager.Singleton.Spawn<Tribal>          (position); break;
                case 'k': ActorManager.Singleton.Spawn<Key>             (position);
                    SpawnPlayerFloor(position);                                     return;
                case '!': ActorManager.Singleton.Spawn<Dagger>          (position); break;
                case '%': ActorManager.Singleton.Spawn<Sword>           (position); break;
                case 'A': ActorManager.Singleton.Spawn<Axe>             (position); break;
                case 'h': ActorManager.Singleton.Spawn<Buckler>         (position); break;
                case 'H': ActorManager.Singleton.Spawn<Heater>          (position); break;
                case 'D': ActorManager.Singleton.Spawn<WarDoor>         (position); break;
                case 'o': ActorManager.Singleton.Spawn<IronHat>         (position); break;
                case 'O': ActorManager.Singleton.Spawn<GreatHelm>       (position); break;
                case 'L': ActorManager.Singleton.Spawn<Sentinel>        (position); break;
                case 'g': ActorManager.Singleton.Spawn<Ghost>           (position); break;
                case 'c': ActorManager.Singleton.Spawn<CurseWardCloak>  (position); break;
                case 'm': ActorManager.Singleton.Spawn<MagicGloves>     (position); break;
                case 'K': ActorManager.Singleton.Spawn<HealthKit>       (position); break;
                case ' ': return;
                default : throw new ArgumentOutOfRangeException();
            }

            SpawnItemAndEnemyFloor(position);
        }

        private static void SpawnActorFromGameObject(string defaultName, (int x, int y) position)
        {
            bool works = defaultName switch
            {
                "Wall" => ActorManager.Singleton.Spawn<Wall>(position),
                "WallWindow" => ActorManager.Singleton.Spawn<WallWindow>(position),
                "Tree" => ActorManager.Singleton.Spawn<Trees>(position),
                "Dead Tree" => ActorManager.Singleton.Spawn<DeadTree>(position),
                "Shut Gate" => ActorManager.Singleton.Spawn<ShutGate>(position),
                "House" => ActorManager.Singleton.Spawn<House>(position),
                "Floor" => ActorManager.Singleton.Spawn<Floor>(position),
                "Grass" => ActorManager.Singleton.Spawn<Grass>(position),
                "Vertical Road" => ActorManager.Singleton.Spawn<RoadVertical>(position),
                "LockedGate" => ActorManager.Singleton.Spawn<LockedGate>(position),
                "Wasp" => ActorManager.Singleton.Spawn<Wasp>(position),
                "Peasant" => ActorManager.Singleton.Spawn<Peasant>(position),
                "Blue Wisp" => ActorManager.Singleton.Spawn<WispBlue>(position),
                "Dead Blue Wisp" => ActorManager.Singleton.Spawn<WispBlueDead>(position),
                "Green Wisp" => ActorManager.Singleton.Spawn<WispGreen>(position),
                "Dead Green Wisp" => ActorManager.Singleton.Spawn<WispGreenDead>(position),
                "Yellow Wisp" => ActorManager.Singleton.Spawn<WispYellow>(position),
                "Dead Yellow Wisp" => ActorManager.Singleton.Spawn<WispYellowDead>(position),
                "Skeleton" => ActorManager.Singleton.Spawn<Skeleton>(position),
                "Tribal" => ActorManager.Singleton.Spawn<Tribal>(position),
                "Key" => ActorManager.Singleton.Spawn<Key>(position),
                "Dagger" => ActorManager.Singleton.Spawn<Dagger>(position),
                "Sword" => ActorManager.Singleton.Spawn<Sword>(position),
                "Axe" => ActorManager.Singleton.Spawn<Axe>(position),
                "Buckler" => ActorManager.Singleton.Spawn<Buckler>(position),
                "Heater" => ActorManager.Singleton.Spawn<Heater>(position),
                "War Door" => ActorManager.Singleton.Spawn<WarDoor>(position),
                "IronHat" => ActorManager.Singleton.Spawn<IronHat>(position),
                "GreatHelm" => ActorManager.Singleton.Spawn<GreatHelm>(position),
                "Sentinel" => ActorManager.Singleton.Spawn<Sentinel>(position),
                "Ghost" => ActorManager.Singleton.Spawn<Ghost>(position),
                "Curse Ward Cloak" => ActorManager.Singleton.Spawn<CurseWardCloak>(position),
                "Magic Gloves" => ActorManager.Singleton.Spawn<MagicGloves>(position),
                "HealthKit" => ActorManager.Singleton.Spawn<HealthKit>(position),
                _ => false,
            };
        }

        public static void SpawnPlayer(Player player, (int x, int y) position, bool loadedGame = false)
        {
            // ActorManager.Singleton.SpawnPlayer(position, player);
            player.Position = position;
            ActorManager.Singleton.AddPlayerToAllActorsList(player);
            if (!loadedGame)
                SpawnPlayerFloor(player.Position);
        }

        public static void SpawnItemAndEnemyFloor((int x, int y)position)
        {
            switch (CurrentMap)
            {   
                case 1: ActorManager.Singleton.Spawn<Grass>(position); break;
                case 2: ActorManager.Singleton.Spawn<Grass>(position); break;
                default:ActorManager.Singleton.Spawn<Floor>(position); break;
            }
        }
        public static void SpawnPlayerFloor((int x, int y) position)
        {
            switch (CurrentMap)
            {
                case 1: ActorManager.Singleton.Spawn<RoadVertical>(position); break;
                case 2: ActorManager.Singleton.Spawn<RoadVertical>(position); break;
                default: ActorManager.Singleton.Spawn<Floor>(position); break;
            }
        }

        public static void RandomSpawnItem((int x, int y) position)
        {
            int percent = new System.Random().Next(0, 1000);

            if (percent < 10)
                ActorManager.Singleton.Spawn<CurseWardCloak>(position);
            else if (percent < 20)
                ActorManager.Singleton.Spawn<MagicGloves>(position);
            else if (percent < 50)
                ActorManager.Singleton.Spawn<WarDoor>(position);
            else if (percent < 100)
                ActorManager.Singleton.Spawn<Axe>(position);
            else if (percent < 170)
                ActorManager.Singleton.Spawn<GreatHelm>(position);
            else if (percent < 400)
                ActorManager.Singleton.Spawn<HealthKit>(position);
        }
    }
}
