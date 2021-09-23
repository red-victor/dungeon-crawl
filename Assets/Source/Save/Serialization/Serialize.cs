using System;
using System.IO;
using Newtonsoft.Json;
using DungeonCrawl.Actors.Characters;
using UnityEngine;
using System.Linq;

namespace DungeonCrawl.Serialization
{
    static class Serialize
    {
        public static string SerializeGame(Player player)
        {
            return JsonConvert.SerializeObject(new GameDataToSerialize(player));
        }

        public static void SaveGameToFile(Player player)
        {
            string json = SerializeGame(player);
            string path = Application.dataPath + @"/exported_saves/" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + ".json";
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

        public static GameDataToSerialize GetSerializableGame(Player player)
        {
            return new GameDataToSerialize(player);
        }
    }
}
