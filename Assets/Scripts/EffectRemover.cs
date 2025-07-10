using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectRemover : MonoBehaviour
{
    [SerializeField]
    private DrugEffect drugEffect;
    public void ClearEffect()
    {
        bool success = EffectManager.Instance.TryRemoveEffect(drugEffect, drugEffect.effectClearValue);

        if (success)
        {
            Debug.Log($"Removed {drugEffect.effectName}!");
        }
        else
        {
            Debug.Log("Not enough money or effect not active!");
        }
    }

}
