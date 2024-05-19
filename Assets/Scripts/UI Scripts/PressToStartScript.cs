using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PressToStartScript : MonoBehaviour
{
    private Text pressText; // Reference to the Text component
    public float duration = 0.5f; // Duration of the lerp
    public GameObject loadingScreen;
    private AudioSource audioSource;
    public AudioClip startSound;
    private bool disabledOnce;

    private void OnEnable() {
        if(disabledOnce){
            StartCoroutine(LerpTextAlphaLoop());
        }
    }
    private void OnDisable() {
        disabledOnce = true;
    }

    private void Start()
    {
        disabledOnce = false;
        loadingScreen.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        pressText = GetComponent<Text>();
        StartCoroutine(LerpTextAlphaLoop());
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            audioSource.PlayOneShot(startSound);
            loadingScreen.SetActive(true);
            Invoke("LoadScene",1f);
        }
    }

    private IEnumerator LerpTextAlphaLoop()
    {
        while (true)
        {
            // Lerp alpha from 0 to 1
            yield return StartCoroutine(LerpAlpha(0f, 1f, duration));
            // Lerp alpha from 1 to 0
            yield return StartCoroutine(LerpAlpha(1f, 0f, duration));
        }
    }

    private IEnumerator LerpAlpha(float startAlpha, float targetAlpha, float duration)
    {
        Color originalColor = pressText.color;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float blend = Mathf.Clamp01(time / duration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(startAlpha, targetAlpha, blend));
            pressText.color = newColor;
            yield return null;
        }

        // Ensure the final alpha is set correctly
        Color finalColor = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
        pressText.color = finalColor;
    }

    private void LoadScene(){
        SceneManager.LoadScene("Tutorial_Scene");
    }
}
