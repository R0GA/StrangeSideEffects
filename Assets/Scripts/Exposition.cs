using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Exposition : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public RawImage backgroundImage;
    public RawImage medicalFileImage;


    public RawImage transitionPG;
    public float fadeDuration = 2f;
    public float waitBeforeFadeOut = 1f;

    public GameObject startButton;

    public GameObject nextButton1;
    public GameObject nextButton2;
    public GameObject nextButton3;


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

    private int scene;

    [Header("Typing")]
    [Space(5)]
    public TextMeshProUGUI nurseDialogue;

    public float letterDelay = 0.05f;  
    public string[] messages;          
    private int currentMessageIndex = 0;
    private Coroutine typingCoroutine;




    public void LetsGetIt()

    {
        scene = 0;
        StartCoroutine(FadeInOutCoroutine());
        animator.SetTrigger("Open");
        audioSource.Play();
    }

    public void ToPharmcy()

    {
        scene = 1;
        StartCoroutine(FadeInOutCoroutine());
    }

    public void ToTrials()

    {
        scene = 2;
        StartCoroutine(FadeInOutCoroutine());
    }


    public void LetsPlay()

    {
        scene = 3;
        StartCoroutine(FadeInOutCoroutine());
        StartCoroutine(NextScene());
    }


    private IEnumerator FadeInOutCoroutine()
    {


        transitionPG.gameObject.SetActive(true);

        yield return StartCoroutine(FadeToAlpha(1f));

        print("transition page on");


        yield return new WaitForSeconds(waitBeforeFadeOut);
        if (scene == 0)
        {
           
            backgroundImage.texture = apartmentTexture;

            medicalFileImage.texture = apartmentFile;
            apartmentPanel.gameObject.SetActive(true);

            pharmacyPanel.gameObject.SetActive(false);
            trialsPanel.gameObject.SetActive(false);

            startButton.gameObject.SetActive(false);
            nextButton1.gameObject.SetActive(true);

            medicalFileImage.gameObject.SetActive(true);

            TypeNextText();

        }
        else if (scene == 2)
        {
            backgroundImage.texture = trialTexture;
            medicalFileImage.texture = trialFile;
            trialsPanel.gameObject.SetActive(true);

            pharmacyPanel.gameObject.SetActive(false);
            apartmentPanel.gameObject.SetActive(false);

            nextButton3.gameObject.SetActive(false);
            nextButton1.gameObject.SetActive(false);
            nextButton2.gameObject.SetActive(true);
        }

        else if (scene == 1)
        {
            backgroundImage.texture = pharmacyTexture;
            medicalFileImage.texture = pharmacyFile;
            pharmacyPanel.gameObject.SetActive(true);

            trialsPanel.gameObject.SetActive(false);
            apartmentPanel.gameObject.SetActive(false);

            nextButton1 .gameObject.SetActive(false);
            nextButton2.gameObject.SetActive(false);
            nextButton3.gameObject.SetActive(true);
            TypeNextText ();
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

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainGameScene");

    }




    #region
    public void TypeNextText()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        if (messages.Length == 0)
            return;

        string message = messages[currentMessageIndex];
        typingCoroutine = StartCoroutine(TypeText(message));

        // Cycle to next message
        currentMessageIndex = (currentMessageIndex + 1) % messages.Length;
    }

    IEnumerator TypeText(string message)
    {
        nurseDialogue.text = "";
        foreach (char c in message)
        {
            nurseDialogue.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    #endregion

}
