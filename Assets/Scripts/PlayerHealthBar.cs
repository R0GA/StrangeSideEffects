using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image healthFill;

    public float currentHealth;
    public float maxHealth = 100;
    public TMP_Text healthText;
   
   // public GameObject player;

    public bool dead = false;

    public void Start()
    {
        currentHealth = maxHealth;
        dead = false;

        //  healthFill.fillAmount = 1f;
    }


    public void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
           dead = true;
        }
    }
    private void UpdateHealthBar()

    {
        float targetFillAmount = currentHealth / maxHealth;
        healthFill.fillAmount = targetFillAmount;
        healthText.text = currentHealth.ToString();
    }
}
