using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DrugTrialUI : MonoBehaviour
{
    public DrugTrialGenerator drugTrialGenerator;

    public TMP_Text trial1;
    public TMP_Text trial2;
    public TMP_Text trial3;
    public TMP_Text trial4;

    public void StartTrial()
    {
        drugTrialGenerator.GenerateNewTrials();
    }

    public void UpdateTrialText()
    {
        trial1.text = $"Cures:\n{drugTrialGenerator.currentTrials[0].SideEffectsCured}" +
            $"Potential Side Effects:\n{drugTrialGenerator.currentTrials[0].SideEffectsWarning}" +
            $"Payout: {drugTrialGenerator.currentTrials[0].reward}";

        trial2.text = $"Cures:\n{drugTrialGenerator.currentTrials[1].SideEffectsCured}" +
            $"Potential Side Effects:\n{drugTrialGenerator.currentTrials[1].SideEffectsWarning}" +
            $"Payout: {drugTrialGenerator.currentTrials[1].reward}";

        trial3.text = $"Cures:\n{drugTrialGenerator.currentTrials[2].SideEffectsCured}" +
            $"Potential Side Effects:\n{drugTrialGenerator.currentTrials[2].SideEffectsWarning}" +
            $"Payout: {drugTrialGenerator.currentTrials[2].reward}";

        trial4.text = $"Cures:\n{drugTrialGenerator.currentTrials[3].SideEffectsCured}" +
            $"Potential Side Effects:\n{drugTrialGenerator.currentTrials[3].SideEffectsWarning}" +
            $"Payout: {drugTrialGenerator.currentTrials[3].reward}";
    }

    public void CompleteTrial1()
    {
        drugTrialGenerator.CompleteTrial(drugTrialGenerator.currentTrials[0]);
    }
    public void CompleteTrial2()
    {
        drugTrialGenerator.CompleteTrial(drugTrialGenerator.currentTrials[1]);
    }
    public void CompleteTrial3()
    {
        drugTrialGenerator.CompleteTrial(drugTrialGenerator.currentTrials[2]);
    }
    public void CompleteTrial4()
    {
        drugTrialGenerator.CompleteTrial(drugTrialGenerator.currentTrials[3]);
    }

}
