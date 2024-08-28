using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource source;
    [Header("AudioMixer & Buttons")]
    public AudioMixer audioMixerEffects;
    public GameObject SoundOn;
    public GameObject SoundOff;
    public int muteEffects;

    [Header("Player Sounds")]
    public AudioClip moveSound;
    public AudioClip exitSound;
    public AudioClip hitSound;

    [Header("UI Sounds")]
    public AudioClip clickSound;
    public AudioClip victorySound;
    public AudioClip victoryPanelSound;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        muteEffects = (int)PlayerPrefs.GetFloat("Effects");
        source = GetComponent<AudioSource>();
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
    public void Move()
    {
        source.PlayOneShot(moveSound);
    }
    public void Exit()
    {
        
        source.PlayOneShot(exitSound);
       
    }
    public void Click()
    {
        source.PlayOneShot(clickSound);
    }
    public void Victory()
    {
        source.PlayOneShot(victorySound);
    }
    public void VictoryPanel()
    {
        source.PlayOneShot(victoryPanelSound);
    }
    public void Hit()
    {
        source.PlayOneShot(hitSound);
        
    }

}
