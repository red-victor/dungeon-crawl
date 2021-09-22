using System;
using System.IO;
using Newtonsoft.Json;
using DungeonCrawl.Core;
using DungeonCrawl.Actors.Characters;
using UnityEngine;
using System.Collections.Generic;
using DungeonCrawl.Actors;
using System.Linq;

namespace DungeonCrawl.Serialization
{
    static class Serialize
    {
        public static void SerializeGame(Player player)
        {
            string json = JsonConvert.SerializeObject(new GameDataToSerialize(player));

            string path = Application.dataPath + @"/exported_saves/" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + ".json";

            // Create and write file
            File.WriteAllText(path, json);
        }

        public static void DeserializeGame(Player player)
        {
            string foldePath = Application.dataPath + @"/exported_saves";
            var directory = new DirectoryInfo(foldePath);

            string latestSaveFile = directory.GetFiles()
             .OrderByDescending(f => f.LastWriteTime)
             .ElementAt(1).FullName;

            string json = File.ReadAllText(latestSaveFile);
            var gameObject =  JsonConvert.DeserializeObject<GameDataToSerialize>(json);
            Player newPlayer = player.Copy();
            ActorManager.Singleton.DestroyAllActors();

            newPlayer.Health = gameObject.Health;
            newPlayer.Position = gameObject.Position;
            newPlayer.Map = gameObject.Map;
            MapLoader.LoadMapFromGameObject(gameObject.Map, newPlayer, gameObject);
        }
    }
}
