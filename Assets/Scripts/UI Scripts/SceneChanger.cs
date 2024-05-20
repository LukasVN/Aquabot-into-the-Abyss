using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject loadingScreen;
    public string sceneToLoad;
    void Start()
    {
        loadingScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerIgnoringPlatforms" && !loadingScreen.activeSelf){
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void LoadScene(string sceneToLoad){
        SceneManager.LoadScene(sceneToLoad);
    }
}
