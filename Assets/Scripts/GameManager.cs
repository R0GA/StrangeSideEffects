using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    public int dayCount;
    public bool hasDoneTrial = true;
    public DrugTrialGenerator drugTrialGenerator;
    public PlayerHealthBar healthBar;
    public TMP_Text dayText;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EndDay();
    }

    public void EndDay()
    {
        if (hasDoneTrial)
        {
            dayCount++;
            hasDoneTrial = false;
            foreach (DrugEffect sideEffect in EffectManager.Instance.activeEffects)
            {
                healthBar.currentHealth -= sideEffect.effectDamage;
            }
            if(EffectManager.Instance.activeEffects.Count <= 0)
            {
                healthBar.currentHealth += 10;
            }
            drugTrialGenerator.GenerateNewTrials();
            dayText.text = dayCount.ToString();
        }  
    }

}
