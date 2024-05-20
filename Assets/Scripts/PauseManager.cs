using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource mainMenuMusic;
    public AudioSource sfxSound;
    public Slider musicSlider;
    public Text musicValueText;
    public Slider sFXSlider;
    public Text sFXValueText;
    public AudioClip clickSound;

    void Start()
    {
        pauseMenu.SetActive(false);
        if(!PlayerPrefs.HasKey("MusicVolume")){
            PlayerPrefs.SetFloat("MusicVolume", 0.1f);
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            musicValueText.text = (musicSlider.value * 100).ToString("0");
        }
        else{
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            musicValueText.text = (musicSlider.value * 100).ToString("0");
        }
        if(!PlayerPrefs.HasKey("SFXVolume")){
            PlayerPrefs.SetFloat("SFXVolume", 0.1f);
            sFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            sFXValueText.text = (sFXSlider.value * 100).ToString("0");
        }
        else{
            sFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            sFXValueText.text = (sFXSlider.value * 100).ToString("0");
        }
    }
    public void OnSlideChangeMusicVolume(){
        mainMenuMusic.volume = musicSlider.value;
        musicValueText.text = (musicSlider.value * 100).ToString("0");
    }

    public void OnSlideChangeSFXVolume(){
        sfxSound.volume = sFXSlider.value;
        sFXValueText.text = (sFXSlider.value * 100).ToString("0");
    }

    public void OnClickSavePlayerPrefs(){
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sFXSlider.value);
        sfxSound.PlayOneShot(clickSound);
        pauseMenu.SetActive(false);
    }

    public void OnClickClosePauseMenu(){
        sfxSound.PlayOneShot(clickSound);
        pauseMenu.SetActive(false);
    }

    public void OnClickOpenPauseMenu(){
        sfxSound.PlayOneShot(clickSound);
        pauseMenu.SetActive(true);
    }

    public void OnClickResetScene(){
        sfxSound.PlayOneShot(clickSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
