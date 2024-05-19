using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public AudioSource[] musicSources;
    public AudioSource[] sfxSources;
    void Start()
    {
        // Music Audio
        if(!PlayerPrefs.HasKey("MusicVolume")){
            PlayerPrefs.SetFloat("MusicVolume", 0.1f);
            setVolumePrefs(musicSources,"MusicVolume");
        }
        else{
            setVolumePrefs(musicSources,"MusicVolume");
        }
        // SFX Audio
        if(!PlayerPrefs.HasKey("SFXVolume")){
            PlayerPrefs.SetFloat("SFXVolume", 0.1f);
            setVolumePrefs(sfxSources,"SFXVolume");
        }
        else{
            setVolumePrefs(sfxSources,"SFXVolume");
        }
    }

    private void setVolumePrefs(AudioSource[] audiosources, string prefKey){
        foreach (AudioSource aSource in audiosources){
            aSource.volume = PlayerPrefs.GetFloat(prefKey);
        }
    }

}
