// Created by DevTushar on 22/12/2022
// Updated by DevTushar on 27/12/2022

using UnityEngine;
using UnityEngine.SceneManagement;

namespace FruitChop
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector] public bool GameIsPlaying;

        /// <summary>
        /// A method that starts the gameplay on button press
        /// </summary>
        public void StartGame()
        {
            GameIsPlaying = true;
            Refrences.Instance.spawnManager.StartGameplay();
        }

        /// <summary>
        /// A method that Restarts the gameplay on button press
        /// </summary>
        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
