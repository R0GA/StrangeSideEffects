using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class BackgroundSwitch : MonoBehaviour
{
   public RawImage backgroundImage;
  
    public Texture apartmentTexture;
    public Texture trialTexture;
    public Texture pharmacyTexture;


    public RawImage transitionPG;
           
    public float fadeDuration = 2f;     
    public float waitBeforeFadeOut = 1f; 


    public void ApartmentBKND()
    {
        backgroundImage.texture = apartmentTexture;
        StartCoroutine(FadeInOutCoroutine());

    }


    public void TrialBKND()
    {
        backgroundImage.texture = trialTexture;

    }


    public void PharmacyBKND()
    {
        backgroundImage.texture = pharmacyTexture;

    }


    private IEnumerator FadeInOutCoroutine()
    {
        
        yield return StartCoroutine(FadeToAlpha(1f));

       
        yield return new WaitForSeconds(waitBeforeFadeOut);

       
        yield return StartCoroutine(FadeToAlpha(0f));
    }

    private IEnumerator FadeToAlpha(float targetAlpha)
    {
        Color startColor = transitionPG.color;
        float startAlpha = startColor.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            transitionPG.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        transitionPG.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }


}
