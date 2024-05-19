using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PressToStartScript : MonoBehaviour
{
    private Text pressText; // Reference to the Text component
    public float duration = 0.5f; // Duration of the lerp

    private void Start()
    {
        pressText = GetComponent<Text>();
        StartCoroutine(LerpTextAlphaLoop());
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("Tutorial_Scene");
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
}
