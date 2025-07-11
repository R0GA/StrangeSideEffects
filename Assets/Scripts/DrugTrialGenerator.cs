using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugTrialGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<DrugEffect> allPossibleEffects;
    [SerializeField] private int maxTrialsAvailable = 3;
    [SerializeField] private int maxEffectsPerTrial = 3;
    public DrugTrialUI drugTrialUI;

    [Header("Current Trials")]
    public List<DrugTrial> currentTrials = new List<DrugTrial>();

    //private void Start() => GenerateNewTrials();

    public void GenerateNewTrials()
    {
        currentTrials.Clear();
        for (int i = 0; i < maxTrialsAvailable; i++)
        {
            currentTrials.Add(CreateRandomTrial());
        }
        drugTrialUI.UpdateTrialText();
    }

    private DrugTrial CreateRandomTrial()
    {
        DrugTrial newTrial = new DrugTrial();

        // 1. Add effects to cure (1-3 random effects)
        int effectsToCureCount = Random.Range(1, maxEffectsPerTrial + 1);
        AddRandomEffects(newTrial.effectsToCure, effectsToCureCount);

        // 2. Add POTENTIAL side effects (always shown, chance applied later)
        int sideEffectsCount = Random.Range(1, maxEffectsPerTrial + 1);
        AddRandomEffects(newTrial.potentialSideEffects, sideEffectsCount);

        // 3. Calculate reward based on risk
        newTrial.reward = CalculateTrialReward(newTrial);

        return newTrial;
    }

    private void AddRandomEffects(List<DrugEffect> targetList, int count)
    {
        for (int i = 0; i < count && allPossibleEffects.Count > 0; i++)
        {
            DrugEffect randomEffect = allPossibleEffects[Random.Range(0, allPossibleEffects.Count)];
            if (!targetList.Contains(randomEffect))
            {
                targetList.Add(randomEffect);
            }
            else
            {
                i--; // Retry if we got a duplicate
            }
        }
    }

    private int CalculateTrialReward(DrugTrial trial)
    {
        int baseReward = 0; // Base payment for participation

        foreach (DrugEffect sideEffect in trial.potentialSideEffects)
        {
            baseReward += Random.Range(sideEffect.effectMinValue, sideEffect.effectMaxValue + 1);
        }

        return baseReward;
    }

    // Call this when player completes a trial
    public void CompleteTrial(DrugTrial trial)
    {
        if (!GameManager.Instance.hasDoneTrial)
        {
            // 1. Cure all promised effects
            foreach (DrugEffect cure in trial.effectsToCure)
            {
                EffectManager.Instance.RemoveEffect(cure);
            }

            // 2. Apply side effects ONLY if RNG passes their individual chance
            foreach (DrugEffect sideEffect in trial.potentialSideEffects)
            {
                if (Random.Range(0, 100) < sideEffect.effectChance)
                {
                    EffectManager.Instance.AddEffect(sideEffect);
                }
            }

            // 3. Give reward
            Player.Instance.money += trial.reward;
            Debug.Log($"Completed trial! Gained ${trial.reward}");

            // 4. Stop Further Trials
            GameManager.Instance.hasDoneTrial = true;

            // 5. Update Player UI
            Player.Instance.UpdateEffectText();

            //4. Refresh available trials
            //GenerateNewTrials();
        }
    }
}

[System.Serializable]
public class DrugTrial
{
    public string trialName;
    public List<DrugEffect> effectsToCure = new List<DrugEffect>();
    public List<DrugEffect> potentialSideEffects = new List<DrugEffect>();
    public int reward;

    public string SideEffectsCured
    {
        get
        {
            string cure = "";
            foreach (var effect in effectsToCure)
            {
                cure += $"{effect.effectName}\n";
            }
            return cure;
        }
    }
    public string SideEffectsWarning
    {
        get
        {
            string warning = "";
            foreach (var effect in potentialSideEffects)
            {
                warning += $"{effect.effectName} ({effect.effectChance}%)\n";
            }
            return warning;
        }
    }
}