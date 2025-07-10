using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
}
