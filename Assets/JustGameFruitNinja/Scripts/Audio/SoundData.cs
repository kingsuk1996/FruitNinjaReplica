﻿/// created By Sayam sahis 13.12.2022
/// Updated By Sayam sahis 26.12.2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObject/SoundData")]
public class SoundData : ScriptableObject
{
    public List<AudioData> AudioDatas = new List<AudioData>();
}

[System.Serializable]
public class AudioData
{
    public enum EAudio
    {
       BackGroundMusic,
       FruitCutting,
       BombBlast,
       BladeSwing,
       ButtonPress,
       ComboSound
    }
    public EAudio AudioID;
    public AudioClip AudioClip;
}