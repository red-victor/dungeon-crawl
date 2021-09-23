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

        public static GameDataToSerialize DeserializeGame()
        {
            string foldePath = Application.dataPath + @"/exported_saves";
            var directory = new DirectoryInfo(foldePath);

            string latestSaveFile = directory.GetFiles()
             .Where(f => f.Extension == ".json")
             .OrderByDescending(f => f.LastWriteTime)
             .First().FullName;

            string json = File.ReadAllText(latestSaveFile);
            return JsonConvert.DeserializeObject<GameDataToSerialize>(json);
        }
    }
}
