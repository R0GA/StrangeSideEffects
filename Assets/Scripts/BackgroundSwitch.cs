using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class BackgroundSwitch : MonoBehaviour
{

    public AudioSource walking;
   

   public RawImage backgroundImage;
   public RawImage medicalFileImage;

    [Header("Bakcground Images")]
    [Space(5)]
    public Texture apartmentTexture;
    public Texture homelessTexture;
    public Texture trialTexture;
    public Texture pharmacyTexture;

    [Header("MedicalFile Images")]
    [Space(5)]
    public Texture pharmacyFile;
    public Texture trialFile;
    public Texture apartmentFile;


    [Header("Panels")]
    [Space(5)]
    public GameObject pharmacyPanel;
    public GameObject trialsPanel;
    public GameObject apartmentPanel;

    public RawImage transitionPG;
           
    public float fadeDuration = 2f;     
    public float waitBeforeFadeOut = 1f;

    private int scene;
   

    public void ApartmentBKND()
    {
        scene = 0;
        StartCoroutine(FadeInOutCoroutine());
        walking.Play();
        // backgroundImage.texture = apartmentTexture;

    }


    public void TrialBKND()
    {
        scene = 1;
        // backgroundImage.texture = trialTexture;
        StartCoroutine(FadeInOutCoroutine());
        walking.Play();

    }


    public void PharmacyBKND()
    {
        scene = 2;
        // backgroundImage.texture = pharmacyTexture;
        StartCoroutine(FadeInOutCoroutine());
        walking.Play();
    }


    private IEnumerator FadeInOutCoroutine()
    {


        transitionPG.gameObject.SetActive(true);

        yield return StartCoroutine(FadeToAlpha(1f));

        


        yield return new WaitForSeconds(waitBeforeFadeOut);
        if (scene == 0)
        {
            if (GameManager.Instance.homeless)
                backgroundImage.texture = homelessTexture;
            else if(!GameManager.Instance.homeless)
                backgroundImage.texture = apartmentTexture;

            medicalFileImage.texture = apartmentFile;
            apartmentPanel.gameObject.SetActive(true);

            pharmacyPanel.gameObject.SetActive(false);
            trialsPanel.gameObject.SetActive(false);
        }
        else if (scene == 1)
        {
            backgroundImage.texture = trialTexture;
            medicalFileImage.texture = trialFile;
            trialsPanel.gameObject.SetActive(true);

            pharmacyPanel.gameObject.SetActive(false);
            apartmentPanel.gameObject.SetActive(false);
        }

        else if (scene == 2)
        {
            backgroundImage.texture = pharmacyTexture;
            medicalFileImage.texture = pharmacyFile;
            pharmacyPanel.gameObject.SetActive(true);

            trialsPanel.gameObject.SetActive(false);
            apartmentPanel.gameObject.SetActive(false);
        }

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
