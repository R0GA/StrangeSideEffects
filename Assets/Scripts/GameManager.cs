using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    public int dayCount;
    public bool hasDoneTrial;
    public DrugTrialGenerator drugTrialGenerator;

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
        EndDay();
    }

    public void EndDay()
    {
        dayCount++;
        hasDoneTrial = false;
        foreach (DrugEffect sideEffect in EffectManager.Instance.activeEffects)
        {
            Player.Instance.health -= sideEffect.effectDamage;
        }
        drugTrialGenerator.GenerateNewTrials();
    }

}
