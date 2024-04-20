using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // If player reaches 0 health, die
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}