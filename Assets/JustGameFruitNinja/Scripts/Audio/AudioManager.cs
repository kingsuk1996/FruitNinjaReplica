
using System.Linq;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Two Audio Types are use for BackGround Music and Other Audios
    /// </summary>
    public enum AudioType
    {
        None = 0,
        Sound,
        Music
    }
    /// <summary>
    /// Creating Singleton Of  AudioManager
    /// </summary>
    public static AudioManager Instance;
    /// <summary>
    /// Contain all audio clips
    /// </summary>
    [SerializeField] private SoundData m_AudioDataSO;
    public BackGroundMusic backGroundMusic;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (!PlayerPrefs.HasKey(AudioConstants.music))
        {
            PlayerPrefs.SetInt(AudioConstants.music, 0);
        }
        if (!PlayerPrefs.HasKey(AudioConstants.sound))
        {
            PlayerPrefs.SetInt(AudioConstants.sound, 0);
        }

    }
    /// <summary>
    /// Play All types of Audio clips
    /// </summary>
    /// <param name="id">Name of the Audio</param>
    /// <param name="volume">How much volume you want to put</param>
    /// <param name="IsLoop">It is use for Repeating</param>
    /// <param name="audioType">Define type of Audio</param>
    /// <returns></returns>
    public AudioSource Play(AudioData.EAudio id, float volume, bool IsLoop, AudioType audioType)
    {

        if (audioType == AudioType.Music)
            if (PlayerPrefs.GetInt(AudioConstants.music) == 1)
                return null;
        if (audioType == AudioType.Sound)
            if (PlayerPrefs.GetInt(AudioConstants.sound) == 1)
                return null;

        AudioData audioData = m_AudioDataSO.AudioDatas.Where(x => x.AudioID == id).FirstOrDefault();
        if (audioData != null /*&&  m_AudioCount < 10*/)
        {
            //m_AudioCount++;
            return AudioPlayer.Play(new AudioPlayerData
            {
                audioClip = audioData.AudioClip,
                oneShot = false,
                loop = IsLoop,
                volume = volume
            }/*, () => { m_AudioCount--; }*/).GetAudioSource();
        }
        else
            return null;
    }


}
/// <summary>
/// Audio constant variables
/// </summary>
[System.Serializable]
public static class AudioConstants
{
    public const string music = "Music";
    public const string sound = "Sound";
}