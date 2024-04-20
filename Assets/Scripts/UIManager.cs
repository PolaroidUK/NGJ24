using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    GlobalEventManager _globalEventManager;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] HealthSO healthScriptableObject;

    void  Start()
    {
         healthText.text = healthScriptableObject.health.ToString();
    }

    public void Initialize (GlobalEventManager globalEventManager)
    {
        _globalEventManager = globalEventManager;
        _globalEventManager.AddListener(GlobalEventManager.EventTypes.Player1HealthDecrease, DecreaseHealth);
        _globalEventManager.AddListener(GlobalEventManager.EventTypes.Player1HealthDecrease, IncreaseHealth);
    }

    private void DecreaseHealth(object o)
    {
        Debug.Log("UIManager::DecreaseHealth");
        healthScriptableObject.health -= 10;
        healthText.text = healthScriptableObject.health.ToString();
    }

    private void IncreaseHealth(object o)
    {
        Debug.Log("UIManager::IncreaseHealth");
        healthScriptableObject.health += 10;
        healthText.text = healthScriptableObject.health.ToString();
    }
    
}
