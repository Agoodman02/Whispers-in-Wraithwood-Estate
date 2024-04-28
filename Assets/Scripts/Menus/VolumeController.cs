using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer SEMixer;

    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSELevel(float sliderValue)
    {
        SEMixer.SetFloat("SoundEffectVol", Mathf.Log10(sliderValue) * 20);
    }
}
