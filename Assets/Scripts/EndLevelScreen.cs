using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelScreen : MonoBehaviour
{
    public Text orbsValueText;
    public Text timeValueText;
    public string level;
    public string difficulty;
    public GameObject loadingScreen;
    private string tweet_url;
    public AudioSource sfxSounds;
    public AudioClip clickSound;

    private void Start() {
        loadingScreen.SetActive(false);
    }
    private void OnEnable() {
        tweet_url = $"https://twitter.com/intent/tweet?text=I%20just%20beat%20level%20{level}%20on%20{difficulty}%20difficulty%20in%20{LevelTimer.instance.GetTimer()}%20in%20Aquabot%3A%20Into%20the%20Abyss!%20Made%20by%20%40LukkasVN%20for%20%23PixelJam2024.%20Can%20you%20do%20better%3F";
        orbsValueText.text = GameManager.instance.coins + " OF 5";
        timeValueText.text = LevelTimer.instance.GetTimer();
    }

    public void ResetLevel(){
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PublishStats(){
        Application.OpenURL(tweet_url);
    }

    public void NextLevel(string scene){
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(scene);
    }
}
