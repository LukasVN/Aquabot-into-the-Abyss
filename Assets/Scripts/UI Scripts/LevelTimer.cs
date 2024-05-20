using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer instance;
    private Text timer;
    private int minutes;
    private float seconds;
    private string currentSceneName;
    private const int perfectTime = 0;
    private const int standardTime = 1;
    private const int mediumTime = 4;
    private const int lateTime = 8;
    private const int maxTime = 10;

    private void Awake() {
        instance = this;
    }


    void Start()
    {
        minutes = 0;
        seconds = 0;
        timer = GetComponent<Text>();
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    
    void Update()
    {
        seconds += Time.deltaTime;
        if(seconds >= 60){
            minutes++;
            seconds = 0;
        }

        switch (minutes){
            case perfectTime: if(timer.color != Color.green){timer.color = Color.green;} 
            break;
            case standardTime: if(timer.color != Color.white){timer.color = Color.white;} 
            break;
            case mediumTime: if(timer.color != Color.yellow){timer.color = Color.yellow;} 
            break;
            case lateTime: if(timer.color != Color.red){timer.color = Color.red;} 
            break;
            case maxTime:  SceneManager.LoadScene(currentSceneName);
            break;
        }

        timer.text = GetTimer();
    }

    public string GetTimer(){
        return $"{minutes:D2}:{(int)seconds:D2}";
    }

}
