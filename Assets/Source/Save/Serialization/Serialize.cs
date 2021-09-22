using System;
using System.IO;
using Newtonsoft.Json;
using DungeonCrawl.Core;
using DungeonCrawl.Actors.Characters;
using UnityEngine;

namespace DungeonCrawl.Serialization
{
    static class Serialize
    {
        public static void SerializePlayer(Player player)
        {
            string json = JsonConvert.SerializeObject(new PlayerToSerialize(player));

            string path = Application.dataPath + @"/export-dates/" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + ".json";

            // Create and write file
            File.WriteAllText(path, json);
        }

        public static PlayerToSerialize DeserializePlayer()
        {
            string path = Application.dataPath + @"/export-dates/2021-09-22T10-37-10.json";
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<PlayerToSerialize>(json);
        }
    }
}
