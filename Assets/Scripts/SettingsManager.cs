using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsButton;
    public GameObject main_Menu;
    public GameObject settings_Menu;
    public AudioSource mainMenuMusic;
    public AudioSource sfxSound;
    public Slider musicSlider;
    public Text musicValueText;
    public Slider sFXSlider;
    public Text sFXValueText;


    void Start()
    {
        main_Menu.SetActive(true);
        settingsButton.SetActive(true);
        settings_Menu.SetActive(false);
        if(!PlayerPrefs.HasKey("MusicVolume")){
            PlayerPrefs.SetFloat("MusicVolume", 0.1f);
            mainMenuMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            musicValueText.text = (musicSlider.value * 100).ToString("0");
        }
        else{
            mainMenuMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            musicValueText.text = (musicSlider.value * 100).ToString("0");
        }
        if(!PlayerPrefs.HasKey("SFXVolume")){
            PlayerPrefs.SetFloat("SFXVolume", 0.1f);
            sfxSound.volume = PlayerPrefs.GetFloat("SFXVolume");
            sFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            sFXValueText.text = (sFXSlider.value * 100).ToString("0");
        }
        else{
            sfxSound.volume = PlayerPrefs.GetFloat("SFXVolume");
            sFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            sFXValueText.text = (sFXSlider.value * 100).ToString("0");
        }
    }

    public void OnSlideChangeMusicVolume(){
        mainMenuMusic.volume = musicSlider.value;
        musicValueText.text = (musicSlider.value * 100).ToString("0");
    }

    public void OnSlideChangeSFXVolume(){
        sfxSound.volume = sFXSlider.value * 10;
        sFXValueText.text = (sFXSlider.value * 100).ToString("0");
    }

    public void OnClickShowSettingsMenu(){
        main_Menu.SetActive(false);
        settingsButton.SetActive(false);
        settings_Menu.SetActive(true);
    }

    public void OnClickSavePlayerPrefs(){
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sFXSlider.value);
        settingsButton.SetActive(true);
        settings_Menu.SetActive(false);
        main_Menu.SetActive(true);
    }
}
