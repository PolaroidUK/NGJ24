using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GlobalEventManager _globalEventManager;

    public void Initialize (GlobalEventManager globalEventManager)
    {
        _globalEventManager = globalEventManager;
        _globalEventManager.AddListener(GlobalEventManager.EventTypes.Player1HealthDecrease, DecreaseHealth);
        _globalEventManager.AddListener(GlobalEventManager.EventTypes.Player1HealthDecrease, IncreaseHealth);
    }

    private void DecreaseHealth(object o)
    {
        Debug.Log("UIManager::DecreaseHealth");
    }

    private void IncreaseHealth(object o)
    {
        Debug.Log("UIManager::IncreaseHealth");
    }
    
}
