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

    private int scene;

    public void ApartmentBKND()
    {
        scene = 0;
        StartCoroutine(FadeInOutCoroutine());

       // backgroundImage.texture = apartmentTexture;
       
    }


    public void TrialBKND()
    {
        scene = 1;
        // backgroundImage.texture = trialTexture;
        StartCoroutine(FadeInOutCoroutine());

    }


    public void PharmacyBKND()
    {
        scene = 2;
        // backgroundImage.texture = pharmacyTexture;
        StartCoroutine(FadeInOutCoroutine());

    }


    private IEnumerator FadeInOutCoroutine()
    {
        

        transitionPG.gameObject.SetActive(true);
        
        yield return StartCoroutine(FadeToAlpha(1f));

       
        yield return new WaitForSeconds(waitBeforeFadeOut);
        if (scene == 0)
            backgroundImage.texture = apartmentTexture;
        else if (scene == 1)
            backgroundImage.texture = trialTexture;
        else if (scene == 2)
            backgroundImage.texture = pharmacyTexture;

        yield return StartCoroutine(FadeToAlpha(0f));
        transitionPG.gameObject.SetActive(false);
       
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
