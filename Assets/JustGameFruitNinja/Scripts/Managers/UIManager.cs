

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    [SerializeField] private Scrollbar loadingScrollBar;
    [SerializeField] private List<GameObject> Panels;
    [SerializeField] private TMP_Text CounterText;

    private int scoreCounter;
    [SerializeField] private Image mainSprite;
    [SerializeField] private Sprite SoundOn;
    [SerializeField] private Sprite SoundOff;

    public static Action<int> ScoreCount;

    [SerializeField] private int noOfLives;

    [SerializeField] private GameObject bgOfGame;
    [SerializeField] private GameObject bgOfOverlays;

    [SerializeField] private float _waitTime;

    [SerializeField] private List<Image> life;
    [SerializeField] private Sprite lifeLost;

    private bool soundIsOn;

    private void Start()
    {
        AudioManager.Instance.backGroundMusic.StartBgMusic();
        soundIsOn = true;
    }

    /// <summary>
    /// To activate the SplashPanel when the script is enbled and activate the loading 
    /// </summary>
    private void OnEnable()
    {
        PlayerPrefs.SetInt(AudioConstants.music, 0);
        PlayerPrefs.SetInt(AudioConstants.sound, 0);
        Panelhandler(GameConstants.SplashPanel);
        StartCoroutine(LoadMenu());
        ScoreCount += Score;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        ScoreCount -= Score;
    }

    /// <summary>
    /// A Coroutine is used to increase the scrool bar to give the loading effect
    /// </summary>
    /// <returns>null return with seconds gap of .1f</returns>
    IEnumerator LoadMenu()
    {
        while (loadingScrollBar.size < 1f)
        {
            loadingScrollBar.size += 5 * Time.deltaTime;
            yield return new WaitForSeconds(.1f);
        }
        Panelhandler(GameConstants.MenuPanel);
        yield return null;
    }

    /// <summary>
    /// A panel handler which enables and disables the panels in the list according to the ID in their PanelIDKeeper script
    /// </summary>
    /// <param name="PanelID">A tag taken as a string to know which panel to activate</param>
    private void Panelhandler(string PanelID)
    {
        foreach (GameObject panel in Panels)
        {
            if (PanelID == "Exit")
            {
                if (panel.GetComponent<PanelIDKeeper>().panelID.Equals(PanelID))
                    panel.SetActive(true);
                else if (panel.GetComponent<PanelIDKeeper>().panelID.Equals("Game"))
                    panel.SetActive(true);
                else
                    panel.SetActive(false);
            }
            else
            {
                if (PanelID.Equals(GameConstants.GamePanel))
                {
                    bgOfOverlays.SetActive(false);
                    bgOfGame.SetActive(true);
                }
                else
                {
                    bgOfOverlays.SetActive(true);
                    bgOfGame.SetActive(false);
                }
                panel.SetActive(panel.GetComponent<PanelIDKeeper>().panelID.Equals(PanelID));
            }
        }
    }

    /// <summary>
    /// A method to activate the Instruction Panel on Play Button clicked
    /// </summary>
    public void OnClickPlay()
    {
        OnButtonPress();
        Panelhandler(GameConstants.InstructionPanel);
    }

    /// <summary>
    /// A method to activate the game panel and start the gameplay on skip button being clicked
    /// </summary>
    public void OnClickSkip()
    {
        OnButtonPress();
        Panelhandler(GameConstants.GamePanel);
        Refrences.Instance.gameManager.StartGame();
    }


    /// <summary>
    /// A method that is called everytime the gameover state is needed
    /// </summary>
    public void GameOver()
    {
        Panelhandler(GameConstants.GameOverPanel);
        Refrences.Instance.gameManager.GameIsPlaying = false;
        Debug.Log("Final Score : " + scoreCounter);
    }

    /// <summary>
    /// A method that is called everytime the back button is pressed
    /// </summary>
    public void OnBackButtonClick()
    {
        OnButtonPress();
        Refrences.Instance.gameManager.GameIsPlaying = false;
        Panelhandler(GameConstants.ExitPanel);
    }

    /// <summary>
    /// A method that is called everytime the YES button in ExitPanel is pressed
    /// </summary>
    public void OnClickYesButton()
    {
        OnButtonPress();
        Panelhandler(GameConstants.MenuPanel);
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// A method that is called everytime the NO button in ExitPanel is pressed
    /// </summary>
    public void OnClickNoButton()
    {
        OnButtonPress();
        Refrences.Instance.gameManager.GameIsPlaying = true;
        Refrences.Instance.spawnManager.ResumeSpawn();
        Panelhandler(GameConstants.GamePanel);
    }

    /// <summary>
    /// A Method that increases and displays the score as text in the UI
    /// </summary>
    /// <param name="score">a int amount of score to add in the score counter</param>
    public void Score(int score)
    {
        scoreCounter += score;
        CounterText.text = scoreCounter.ToString();
    }

    /// <summary>
    /// A method that manages player's Lives
    /// </summary>
    public void LifeLost()
    {
        life[life.Count - noOfLives].sprite = lifeLost;
        noOfLives--;
        if (noOfLives < 1)
        {
            GameOver();
        }
    }

    /// <summary>
    /// This method used for Mute all sounds.
    /// </summary>
    public void MuteSound()
    {
        OnButtonPress();
        if (soundIsOn)
        {
            soundIsOn = false;
            mainSprite.sprite = SoundOff;
            PlayerPrefs.SetInt(AudioConstants.music, 1);
            PlayerPrefs.SetInt(AudioConstants.sound, 1);
            AudioManager.Instance.backGroundMusic.StopBgMusic();
        }
        else
        {
            soundIsOn = true;
            mainSprite.sprite = SoundOn;
            PlayerPrefs.SetInt(AudioConstants.music, 0);
            PlayerPrefs.SetInt(AudioConstants.sound, 0);
            AudioManager.Instance.backGroundMusic.StartBgMusic();
        }
    }

    /// <summary>
    /// A method that plays button press sound when called
    /// </summary>
    public void OnButtonPress()
    {
        AudioManager.Instance.Play(AudioData.EAudio.ButtonPress, 1, false, AudioManager.AudioType.Sound);
        AudioManager.Instance.Play(AudioData.EAudio.ButtonPress, 1, false, AudioManager.AudioType.Music);
    }
}
