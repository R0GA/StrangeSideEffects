using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class End : MonoBehaviour
{
    public TMP_Text nurseDialogue;
    public string message;
    public float letterDelay; 
    private void Start()
    {
        StartCoroutine(TypeText(message));
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
}
