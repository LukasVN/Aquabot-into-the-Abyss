using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerNewSpawnpoint : MonoBehaviour
{
    private PlayerScript playerScript;
    private bool activated;
    void Start()
    {
        activated = false;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!activated && other.gameObject.tag == "Player"){
            playerScript.SetSpawnpoint(transform);
            activated = true;
        }
    }
}
