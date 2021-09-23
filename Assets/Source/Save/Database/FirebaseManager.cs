using Proyecto26;
using DungeonCrawl.Serialization;
using DungeonCrawl.Load;
using DungeonCrawl.Actors.Characters;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private string api = "https://dungeon-crawl-93563-default-rtdb.europe-west1.firebasedatabase.app/";

    public void SaveGameToDatabase()
    {
        // Find the Object with tag "player" and get data from it
        var playerObj = GameObject.FindGameObjectWithTag("Player");

        // Get player class from player gameObject;
        Player player = (Player)playerObj.GetComponent(typeof(Player));

        // Get Serializable game object
        var gameObject = Serialize.GetSerializableGame(player);

        RestClient.Put(api + "Player.json", gameObject);
    }

    public void LoadGameFromDatabase()
    {
        RestClient.Get<GameDataToSerialize>(api + "Player.json").Then(res =>
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            Player player = (Player)playerObj.GetComponent(typeof(Player));
            LoadGame.Load(player, res);
        });
    }
}
