using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Collider2D platformCollider;
    public KeyCode fallThroughKey = KeyCode.S; // The key to press to fall through the platform
    public float fallThroughDuration = 0.5f; // Time to fall through the platform
    private GameObject player;
    private string playerLayer = "Player";
    private string playerIgnoringPlatformsLayer = "PlayerIgnoringPlatforms";
    private bool isOnPlatform;

    void Start()
    {
        isOnPlatform = false;
        player = GameObject.FindWithTag("Player");
        platformCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isOnPlatform && Input.GetKeyDown(fallThroughKey))
        {
            StartCoroutine("DisableCollisionForFall");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            isOnPlatform = true;
        }
    }

    private IEnumerator DisableCollisionForFall()
    {
        player.layer = LayerMask.NameToLayer(playerIgnoringPlatformsLayer);

        yield return new WaitForSeconds(fallThroughDuration);

        player.layer = LayerMask.NameToLayer(playerLayer);
    }
}
