using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    [SerializeField] 
    public List<DrugEffect> activeEffects = new List<DrugEffect>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddEffect(DrugEffect effect)
    {
        if (!activeEffects.Contains(effect))
        {
            activeEffects.Add(effect);
        }
    }

    public void RemoveEffect(DrugEffect effect)
    {
        if (activeEffects.Contains(effect))
        {
            activeEffects.Remove(effect);
        }
    }

    public bool TryRemoveEffect(DrugEffect effect, int cost)
    {
        if (activeEffects.Contains(effect) && Player.Instance.money >= cost) 
        {
            Player.Instance.money -= cost;
            RemoveEffect(effect);
            return true;
        }
        return false;
    }
    public string CurrentEffects
    {
        get
        {
            string currentEffects = "";
            foreach (var effect in activeEffects)
            {
                currentEffects += $"{effect.effectName}\n";
            }
            return currentEffects;
        }
    }
}
