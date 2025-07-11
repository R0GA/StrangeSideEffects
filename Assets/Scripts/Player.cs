using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text moneyText;
    public TMP_Text effectsText;

    public int health;
    public int money;



    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Only if player persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
      
    }

    private void Update()
    {
        moneyText.text = $"Money: {money}";
    }

    public void UpdateEffectText()
    {
        effectsText.text = $"Current Effects:\n{EffectManager.Instance.CurrentEffects}";
    } 

}
