using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    public int dayCount;
    public int rentCount;
    public int rentCost;
    public int currentRentCost;
    public bool hasDoneTrial = true;
    public bool homeless;
    public DrugEffect homelessness;
    public DrugTrialGenerator drugTrialGenerator;
    public PlayerHealthBar healthBar;
    public TMP_Text dayText;
    public TMP_Text rentText;
    

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
        rentCount = 7;
        currentRentCost = rentCost;
        EndDay();
        currentRentCost = rentCost;
        homeless = false;
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
            CheckRent();
        }  
    }
    
    public void PayRent()
    {
        if (!homeless)
        {
            if (currentRentCost > 0)
            {
                if (Player.Instance.money >= currentRentCost)
                {
                    Player.Instance.money -= currentRentCost;
                    currentRentCost -= currentRentCost;
                    rentText.text = $"You're all up to date, good job!";
                    EffectManager.Instance.RemoveEffect(homelessness);
                    Player.Instance.UpdateEffectText();
                }
            }
        }
        else if (homeless)
        {
            if (currentRentCost > 0)
            {
                if (Player.Instance.money >= currentRentCost)
                {
                    Player.Instance.money -= currentRentCost;
                    currentRentCost -= currentRentCost;
                    homeless = false;
                    rentCount = 7;
                    currentRentCost += rentCost;
                    rentText.text = $"Welcome back. Days until rent is due: {rentCount}\nCost: {currentRentCost}";
                    EffectManager.Instance.RemoveEffect(homelessness);
                    Player.Instance.UpdateEffectText();
                }
            }
        }
    }
    
    public void CheckRent()
    {
        if (!homeless)
        {
            if (rentCount > 0)
            {
                if (currentRentCost <= 0)
                {
                    rentCount--;
                    rentText.text = $"You're all up to date, good job!";
                }
                else if (currentRentCost > 0)
                {
                    rentCount--;
                    rentText.text = $"Days until rent is due: {rentCount}\nCost: {currentRentCost}";
                }
            }
            else if (rentCount <= 0)
            {
                if (currentRentCost > 0)
                {
                    homeless = true;
                    rentCount = 7;
                    EffectManager.Instance.AddEffect(homelessness);
                    rentText.text = $"Oh No! You've been kicked out! Pay Your Rent!\nCost: {currentRentCost}";
                    Player.Instance.UpdateEffectText();
                }
                else if (currentRentCost <= 0)
                {
                    homeless = false;
                    currentRentCost += rentCost;
                    rentCount = 7;
                    EffectManager.Instance.RemoveEffect(homelessness);
                    rentText.text = $"Days until rent is due: {rentCount}\nCost: {currentRentCost}";
                    Player.Instance.UpdateEffectText();
                }
            }
        }
       else if (homeless)
        {
            rentText.text = $"Oh No! You've been kicked out! Pay Your Rent!\nCost: {currentRentCost}";
        }
    }
}
