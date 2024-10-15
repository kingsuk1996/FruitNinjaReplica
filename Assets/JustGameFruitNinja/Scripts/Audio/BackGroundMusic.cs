/// created By Sayam sahis 13.12.2022
/// Updated By Sayam sahis 26.12.2022

using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    /// <summary>
    /// audio source for background music play
    /// </summary>
    public AudioSource backgroundMusic;
    void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = AudioManager.Instance.Play(AudioData.EAudio.BackGroundMusic, 0.2f, true, AudioManager.AudioType.Music);
        }
    }
   
    /// <summary>
    /// Stop playing the background music
    /// </summary>
    public void StopBgMusic()
    {
        int musicValue = PlayerPrefs.GetInt("music");
        backgroundMusic.Stop();
    }
    /// <summary>
    /// start playing background music
    /// </summary>
    public void StartBgMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
            return;
        }       
        backgroundMusic = AudioManager.Instance.Play(AudioData.EAudio.BackGroundMusic, .2f, true, AudioManager.AudioType.Music);
    }
    /// <summary>
    /// change background music
    /// </summary>
    /// <param name="Audio">name of the audioclip</param>
    /*public void ChangeBGMusic(AudioData.EAudio Audio )
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop(); 
        }
        backgroundMusic = AudioManager.Instance.Play(Audio, 1f, true, AudioManager.AudioType.Music);
    }*/
    
    void OnDestroy()
    {
        Destroy(backgroundMusic);
    }
}
