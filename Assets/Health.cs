using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        GlobalEventManager globalEventManager = GameManager.Instance.globalEventManager;


        globalEventManager.AddListener(GlobalEventManager.EventTypes.Player1HealthIncrease, IncreaseHealth);
        globalEventManager.AddListener(GlobalEventManager.EventTypes.Player1HealthDecrease, DecreaseHealth);

        globalEventManager.AddListener(GlobalEventManager.EventTypes.Player2HealthIncrease, IncreaseHealth);
        globalEventManager.AddListener(GlobalEventManager.EventTypes.Player2HealthDecrease, DecreaseHealth);



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

    void DecreaseHealth(object o)
    {
        currentHealth--;
    }

    void IncreaseHealth(object o)
    {
        currentHealth++;
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