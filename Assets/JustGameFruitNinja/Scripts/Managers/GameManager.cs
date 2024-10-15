
using UnityEngine;
using UnityEngine.SceneManagement;


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

