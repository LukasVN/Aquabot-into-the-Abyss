using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEndScreen : MonoBehaviour
{
    public GameObject[] goToDisable;
    public GameObject endScreen;
    private bool triggerActivated;
    void Start()
    {
        endScreen.SetActive(false);
        triggerActivated = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(triggerActivated && other.gameObject.tag == "Player" ||triggerActivated &&  other.gameObject.tag == "PlayerIgnoringPlatforms"){
            triggerActivated = false;
            DisableGameObjects();
            endScreen.SetActive(true);
        }
    }

    private void DisableGameObjects(){
        foreach (GameObject go in goToDisable){
            go.SetActive(false);
        }
    }
}
