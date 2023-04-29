using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDetectorPanel : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private float timeFade;
    [SerializeField] private float remainingTimeFade;
    [SerializeField] private Image fadeImage;
    private Coroutine fadeCoroutine; 
    private void Start()
    {
        health.takedDamage += Fade;
    }
    private void Fade()
    {
        remainingTimeFade = timeFade;
        /*if (fadeCoroutine != null)
        {
            fadeCoroutine = StartCoroutine(FadeCoroutine());
        }*/
    }
    private void Update()
    {
        if (remainingTimeFade >= 0)
        {
            remainingTimeFade -= Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, remainingTimeFade / timeFade);
        }
        else
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
            remainingTimeFade = 0;
        }
    }
    private IEnumerator FadeCoroutine()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            remainingTimeFade -= Time.deltaTime;
            if (remainingTimeFade >= 0)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, remainingTimeFade / timeFade);
            }
            else
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
                remainingTimeFade = 0;
                break;
            }
        }
    }
}
