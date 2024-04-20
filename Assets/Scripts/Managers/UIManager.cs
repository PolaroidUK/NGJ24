using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    GlobalEventManager _globalEventManager;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] PlayerController playerController;

    void  Start()
    {
         //healthText.text = playerController.health.ToString();
    }

    public void Initialize (GlobalEventManager globalEventManager)
    {
        _globalEventManager = globalEventManager;
        _globalEventManager.AddListener(GlobalEventManager.EventTypes.Player1HealthDecrease, DecreaseHealth);
        _globalEventManager.AddListener(GlobalEventManager.EventTypes.Player1HealthIncrease, IncreaseHealth);
    }

    private void DecreaseHealth(object o)
    {
        Debug.Log("UIManager::DecreaseHealth");
        healthText.text = playerController.health.ToString();
    }

    private void IncreaseHealth(object o)
    {
        Debug.Log("UIManager::IncreaseHealth");
        healthText.text = playerController.health.ToString();
    }
    
}
