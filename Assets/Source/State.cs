using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonCrawl
{
    public class State : MonoBehaviour
    {
        private float _delayInSeconds = 2f;

        public void LoadStartMenu()
        {
            SceneManager.LoadScene("Start Menu");
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void LoadGameOver(bool gameIsFinished=false)
        {
            if (gameIsFinished)
                SceneManager.LoadScene("Game Over");
            else
                StartCoroutine(WaitAndLoadGameOver());
        }

        IEnumerator WaitAndLoadGameOver()
        {
            yield return new WaitForSeconds(_delayInSeconds);
            SceneManager.LoadScene("Game Over");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
