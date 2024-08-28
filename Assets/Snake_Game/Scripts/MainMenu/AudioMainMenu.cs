using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMainMenu : MonoBehaviour
{
    public static AudioMainMenu instance;
    [Header("AudioMixer & Buttons")]
    public AudioMixer audioMixerEffects;
    public GameObject SoundOn;
    public GameObject SoundOff;
    public int muteEffects;
    [Header("Audios")]
    public AudioClip ClickSound;
    AudioSource source;
   
    private void Awake()
    {
        instance = this;
       
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
        muteEffects = (int)PlayerPrefs.GetFloat("Effects");
        if (muteEffects == 0)
        {
            
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
            audioMixerEffects.SetFloat("VolumeEffects", 0);
        }
        else
        {
            SoundOff.SetActive(true);
            SoundOn.SetActive(false);
            audioMixerEffects.SetFloat("VolumeEffects", -80);
        }
    }
    public void AudioEffectsMute()
    {
        if (muteEffects == 0)
        {
            Click();
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
            PlayerPrefs.SetFloat("Effects", 1);
            audioMixerEffects.SetFloat("VolumeEffects", -80);
            muteEffects = (int)PlayerPrefs.GetFloat("Effects");
            return;
        }
        if (muteEffects == 1)
        {
            Click();
            SoundOff.SetActive(false);
            SoundOn.SetActive(true);
            PlayerPrefs.SetFloat("Effects", 0);
            audioMixerEffects.SetFloat("VolumeEffects", 0);
            muteEffects = (int)PlayerPrefs.GetFloat("Effects");
            return;
        }

    }

    public void Click()
    {
        source.PlayOneShot(ClickSound);
    }
}
