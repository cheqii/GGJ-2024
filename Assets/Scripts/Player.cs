using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void Update()
    {
        
    }

    public void DeceaseHealth(float value)
    {
        if (currentHealth > 0) currentHealth -= value;
        if (currentHealth <= 0) currentHealth = 0;
    }
    
}
