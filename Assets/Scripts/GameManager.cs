using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake() {
        instance = this;
    } 

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 61;
    }

    void Update()
    {
        
    }
}
