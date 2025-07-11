using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EffectRemover : MonoBehaviour
{
    [SerializeField]
    private DrugEffect drugEffect;
    [SerializeField]
    private TMP_Text drugName;

    private void Start()
    {
        drugName.text = $"{drugEffect.effectName}\nCost: {drugEffect.effectClearValue}";
    }
    public void ClearEffect()
    {
        bool success = EffectManager.Instance.TryRemoveEffect(drugEffect, drugEffect.effectClearValue);

        if (success)
        {
            Debug.Log($"Removed {drugEffect.effectName}!");
            Player.Instance.UpdateEffectText();
        }
        else
        {
            Debug.Log("Not enough money or effect not active!");
        }
    }

}
