using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System;

public class FadeImage : MonoBehaviour
{
    [Range(0f, 1f)] public float alphaStart;
    public UnityEvent onFadeInEnd; // Exposed in Inspector
    public UnityEvent onFadeOutEnd; // Exposed in Inspector
    Image image;
    CanvasGroup canvasGroup;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (image != null)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alphaStart);
            Color color = image.color;
            image.color = new Color(color.r, color.g, color.b, alphaStart);
        }
        else if (canvasGroup != null)
        {
            canvasGroup.alpha = alphaStart;
        }
        else
        {
            Debug.LogError("No Image or CanvasGroup component found on this GameObject.");
        }
    }

    public void FadeIn(float fadeDuration)
    {
        StartCoroutine(StartFade(0f, 1f, fadeDuration));
    }

    public void FadeOut(float fadeDuration)
    {
        StartCoroutine(StartFade(1f, 0f, fadeDuration));
    }

    public void FadeInOut(float fadeDuration)
    {
        StartCoroutine(FadeInOutRoutine(fadeDuration));
    }

    private IEnumerator FadeInOutRoutine(float fadeDuration)
    {
        yield return StartCoroutine(StartFade(0f, 1f, fadeDuration)); // Fade in
        yield return StartCoroutine(StartFade(1f, 0f, fadeDuration)); // Then fade out
    }

    private IEnumerator StartFade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        if (image != null)
        {
            Color color = image.color;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                image.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }
            image.color = new Color(color.r, color.g, color.b, endAlpha);
        }
        else if (canvasGroup != null)
        {
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                yield return null;
            }
            canvasGroup.alpha = endAlpha;
        }

        if (onFadeInEnd != null && startAlpha == 0f)
        {
            onFadeInEnd.Invoke(); // Executes assigned function(s)
        }

        if (onFadeOutEnd != null && startAlpha == 1f)
        {
            onFadeOutEnd.Invoke(); // Executes assigned function(s)
        }
    }
    
    public void FadeInCanvasGroup(float fadeDuration)
    {
        StartCoroutine(FadeCanvasGroup(0f, 1f, fadeDuration));
    }

    public void FadeOutCanvasGroup(float fadeDuration)
    {
        StartCoroutine(FadeCanvasGroup(1f, 0f, fadeDuration));
    }

    public void FadeInOutCanvasGroup(float fadeDuration)
    {
        StartCoroutine(FadeInOutCanvasGroupRoutine(fadeDuration));
    }

    private IEnumerator FadeInOutCanvasGroupRoutine(float fadeDuration)
    {
        yield return StartCoroutine(FadeCanvasGroup(0f, 1f, fadeDuration)); // Fade in
        yield return StartCoroutine(FadeCanvasGroup(1f, 0f, fadeDuration)); // Then fade out
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        // Pastikan ada CanvasGroup
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                Debug.LogError("No CanvasGroup component found on this GameObject.");
                yield break;
            }
        }

        float elapsedTime = 0f;
        canvasGroup.alpha = startAlpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        // Trigger events based on fade direction
        if (onFadeInEnd != null && startAlpha == 0f)
        {
            onFadeInEnd.Invoke();
        }

        if (onFadeOutEnd != null && startAlpha == 1f)
        {
            onFadeOutEnd.Invoke();
        }
    }
}