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
    public float period = 2.0f;

    // Horizontal Movement
    public bool movingHorizontally;
    public float horizontalSpeed = 2.0f;
    public float leftBound = -5.0f;
    public float rightBound = 5.0f;
    // Vertical Movement
    public bool movingVertically;
    public float verticalSpeed = 2.0f;
    public float lowerBound = -5.0f;
    public float upperBound = 5.0f;

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
        if(movingHorizontally){
            float amplitude = (rightBound - leftBound) / 2.0f;
            float offset = (rightBound + leftBound) / 2.0f;
            float horizontalMovement = amplitude * Mathf.Sin((2 * Mathf.PI * Time.time) / period) + offset;
            transform.position = new Vector2(horizontalMovement, transform.position.y);
        }
        if(movingVertically){
            float amplitude = (upperBound - lowerBound) / 2.0f;
            float offset = (upperBound + lowerBound) / 2.0f;
            float verticalMovement = amplitude * Mathf.Sin((2 * Mathf.PI * Time.time) / period) + offset;
            transform.position = new Vector2(transform.position.x, verticalMovement);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            isOnPlatform = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerIgnoringPlatforms")){
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerIgnoringPlatforms")){
            other.transform.parent = null;
        }
    }

    private IEnumerator DisableCollisionForFall()
    {
        player.layer = LayerMask.NameToLayer(playerIgnoringPlatformsLayer);

        yield return new WaitForSeconds(fallThroughDuration);

        player.layer = LayerMask.NameToLayer(playerLayer);
    }
}
