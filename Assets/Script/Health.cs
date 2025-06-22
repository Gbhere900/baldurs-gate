using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private  int maxHealth = 100;
    private int health;
    public Action OnDead;
    public Action OnTakenDamage;
    private void OnEnable()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        if(health == 0)
        {
            OnDead.Invoke();
        }
        OnTakenDamage.Invoke();
    }

    public float GetHealthIdentified()
    {
        return (float)health /maxHealth;
    }
}
