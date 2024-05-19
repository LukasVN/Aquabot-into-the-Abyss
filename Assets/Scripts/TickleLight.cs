using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TickleLight : MonoBehaviour
{
    private Light2D light2D;

    // Target values for the outer radius
    public float targetRadius = 10f;
    public float duration = 3f; // Duration to complete one cycle (increase and decrease)
    public float waitTime = 2f; // Time to wait after a full cycle

    private float originalRadius;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        if (light2D == null)
        {
            Debug.LogError("Light2D component not found!");
            return;
        }

        originalRadius = light2D.pointLightOuterRadius;
        StartCoroutine(BlinkingLight());
    }

    private IEnumerator BlinkingLight()
    {
        float elapsedTime = 0f;

        // Increase radius to targetRadius
        while (elapsedTime < duration / 2)
        {
            light2D.pointLightOuterRadius = Mathf.Lerp(originalRadius, targetRadius, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it reaches the exact target radius
        light2D.pointLightOuterRadius = targetRadius;

        elapsedTime = 0f;

        // Decrease radius back to originalRadius
        while (elapsedTime < duration / 2)
        {
            light2D.pointLightOuterRadius = Mathf.Lerp(targetRadius, originalRadius, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it reaches the exact original radius
        light2D.pointLightOuterRadius = originalRadius;

        // Wait for the specified wait time before restarting the cycle
        yield return new WaitForSeconds(waitTime);

        // Restart the coroutine to loop the effect
        StartCoroutine(BlinkingLight());
    }
}
