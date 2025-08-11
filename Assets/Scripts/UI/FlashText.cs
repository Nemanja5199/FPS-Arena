using System.Collections;
using TMPro;
using UnityEngine;

public class FlashText : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    [Header("Flash Settings")]
    [SerializeField] private FlashType flashType = FlashType.Fade;
    [SerializeField] private float flashSpeed = 1f;
    [SerializeField] private bool autoStart = true;

    [Header("Color Flash Settings")]
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private bool returnToOriginalColor = true;
    private Color originalColor;

    [Header("Scale Flash Settings")]
    [SerializeField] private float maxScale = 1.2f;
    [SerializeField] private float minScale = 0.9f;

    private Coroutine currentFlashCoroutine;

    public enum FlashType
    {
        Fade,
        ColorSwitch,
        Scale,
        FadeInOut
    }

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            originalColor = textComponent.color;

            if (autoStart)
            {
                StartFlashing();
            }
        }
    }

    public void StartFlashing()
    {
        if (currentFlashCoroutine != null)
        {
            StopCoroutine(currentFlashCoroutine);
        }

        switch (flashType)
        {
            case FlashType.Fade:
                currentFlashCoroutine = StartCoroutine(FadeFlash());
                break;
            case FlashType.ColorSwitch:
                currentFlashCoroutine = StartCoroutine(ColorFlash());
                break;
            case FlashType.Scale:
                currentFlashCoroutine = StartCoroutine(ScaleFlash());
                break;
            case FlashType.FadeInOut:
                currentFlashCoroutine = StartCoroutine(FadeInOutFlash());
                break;
        }
    }

    public void StopFlashing()
    {
        if (currentFlashCoroutine != null)
        {
            StopCoroutine(currentFlashCoroutine);
            currentFlashCoroutine = null;

            // Reset to original state
            textComponent.color = originalColor;
            transform.localScale = Vector3.one;
        }
    }

    // Fade alpha in and out
    IEnumerator FadeFlash()
    {
        while (true)
        {
            // Fade out
            float elapsedTime = 0f;
            Color currentColor = textComponent.color;

            while (elapsedTime < flashSpeed)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0.2f, elapsedTime / flashSpeed);
                textComponent.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                yield return null;
            }

            // Fade in
            elapsedTime = 0f;
            while (elapsedTime < flashSpeed)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0.2f, 1f, elapsedTime / flashSpeed);
                textComponent.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                yield return null;
            }
        }
    }

    // Switch between two colors
    IEnumerator ColorFlash()
    {
        while (true)
        {
            textComponent.color = flashColor;
            yield return new WaitForSeconds(flashSpeed);

            if (returnToOriginalColor)
            {
                textComponent.color = originalColor;
                yield return new WaitForSeconds(flashSpeed);
            }
        }
    }

    // Scale up and down
    IEnumerator ScaleFlash()
    {
        while (true)
        {
            // Scale up
            float elapsedTime = 0f;
            while (elapsedTime < flashSpeed)
            {
                elapsedTime += Time.deltaTime;
                float scale = Mathf.Lerp(1f, maxScale, elapsedTime / flashSpeed);
                transform.localScale = Vector3.one * scale;
                yield return null;
            }

            // Scale down
            elapsedTime = 0f;
            while (elapsedTime < flashSpeed)
            {
                elapsedTime += Time.deltaTime;
                float scale = Mathf.Lerp(maxScale, minScale, elapsedTime / flashSpeed);
                transform.localScale = Vector3.one * scale;
                yield return null;
            }

            // Back to normal
            elapsedTime = 0f;
            while (elapsedTime < flashSpeed)
            {
                elapsedTime += Time.deltaTime;
                float scale = Mathf.Lerp(minScale, 1f, elapsedTime / flashSpeed);
                transform.localScale = Vector3.one * scale;
                yield return null;
            }
        }
    }

    // Complete fade in and out
    IEnumerator FadeInOutFlash()
    {
        while (true)
        {
            // Fade out completely
            float elapsedTime = 0f;
            Color currentColor = textComponent.color;

            while (elapsedTime < flashSpeed)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / flashSpeed);
                textComponent.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(0.2f); // Brief pause while invisible

            // Fade in completely
            elapsedTime = 0f;
            while (elapsedTime < flashSpeed)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / flashSpeed);
                textComponent.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(0.2f); // Brief pause while visible
        }
    }

    // Public method to flash a specific number of times
    public void FlashTimes(int times)
    {
        if (currentFlashCoroutine != null)
        {
            StopCoroutine(currentFlashCoroutine);
        }
        currentFlashCoroutine = StartCoroutine(FlashSpecificTimes(times));
    }

    IEnumerator FlashSpecificTimes(int times)
    {
        for (int i = 0; i < times; i++)
        {
            textComponent.color = flashColor;
            yield return new WaitForSeconds(flashSpeed / 2);
            textComponent.color = originalColor;
            yield return new WaitForSeconds(flashSpeed / 2);
        }
        currentFlashCoroutine = null;
    }
}
