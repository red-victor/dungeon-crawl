﻿using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static.Environments;
using DungeonCrawl.Actors.Static.Items.Armour;
using DungeonCrawl.Actors.Static.Items.Armour.Helmets;
using DungeonCrawl.Actors.Static.Items.Armour.Shields;
using DungeonCrawl.Actors.Static.Items.Consumables;
using DungeonCrawl.Actors.Static.Items.Weapons;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     MapLoader is used for constructing maps from txt files
    /// </summary>
    public static class MapLoader
    {
        /// <summary>
        ///     Constructs map from txt file and spawns actors at appropriate positions
        /// </summary>
        /// <param name="id"></param>
        public static void LoadMap(int id, Player player = null)
        {
            
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

        private static void SpawnActor(char c, (int x, int y) position)
        {
            switch (c)
            {
                case '#':
                    ActorManager.Singleton.Spawn<Wall>(position);
                    break;
                case '$':
                    ActorManager.Singleton.Spawn<BigTree>(position);
                    break;
                case '&':
                    ActorManager.Singleton.Spawn<DoubleTree>(position);
                    break;
                case '{':
                    ActorManager.Singleton.Spawn<DeadTree>(position);
                    break;
                case '.':
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case ',':
                    ActorManager.Singleton.Spawn<Grass>(position);
                    break;
                case 'd':
                    ActorManager.Singleton.Spawn<LockedGate>(position);
                    break;
                case 'p':
                    ActorManager.Singleton.Spawn<Player>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 's':
                    ActorManager.Singleton.Spawn<Skeleton>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'k':
                    ActorManager.Singleton.Spawn<Key>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case '%':
                    ActorManager.Singleton.Spawn<Sword>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'A':
                    ActorManager.Singleton.Spawn<Axe>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'h':
                    ActorManager.Singleton.Spawn<Buckler>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'H':
                    ActorManager.Singleton.Spawn<Heater>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'D':
                    ActorManager.Singleton.Spawn<WarDoor>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'o':
                    ActorManager.Singleton.Spawn<IronHat>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'O':
                    ActorManager.Singleton.Spawn<GreatHelm>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'c':
                    ActorManager.Singleton.Spawn<CurseWardCloak>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'm':
                    ActorManager.Singleton.Spawn<MagicGloves>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'K':
                    ActorManager.Singleton.Spawn<HealthKit>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'L':
                    ActorManager.Singleton.Spawn<Sentinel>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'g':
                    ActorManager.Singleton.Spawn<Ghost>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case ' ':
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public static void SpawnPlayer(Player player, (int x, int y) position)
        {
            // ActorManager.Singleton.SpawnPlayer(position, player);
            player.Position = position;
            ActorManager.Singleton.AddPlayerToAllActorsList(player);
            ActorManager.Singleton.Spawn<Floor>(position);
        }
    }
}
