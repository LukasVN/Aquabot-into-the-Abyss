using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject player;
    private AudioSource playerAudioSource;
    public AudioClip coinSound;
    public Text coinCounter;
    public int coins;

    private void Awake() {
        instance = this;
    } 

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerAudioSource = player.GetComponent<AudioSource>();
        coins = 0;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 61;
    }

    public void AddCoin(){
        playerAudioSource.PlayOneShot(coinSound);
        coins++;
        coinCounter.text = coins + " / 5";
    }
}
