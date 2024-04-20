using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    GlobalEventManager _globalEventManager;
    [SerializeField] PlayerController playerController;

    [SerializeField] public int currentPlayerIndex = 0;

    [SerializeField] public int ui_Flow_Current_Index = 0;

    [SerializeField] List<Canvas> uiFlowScreens;

    private Canvas currentCanvas;
    private Canvas nextCanvas; 

    [SerializeField] TMP_InputField secretInputField;

    void  Start()
    {
        currentCanvas = uiFlowScreens[ui_Flow_Current_Index];
        nextCanvas = uiFlowScreens[ui_Flow_Current_Index + 1];
        currentCanvas.gameObject.SetActive(true);

        // Set all other Canvases False;
        for (int i = ui_Flow_Current_Index + 1; i < uiFlowScreens.Count; i++)
        {
            uiFlowScreens[i].gameObject.SetActive(false);
        }
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
    }

    private void IncreaseHealth(object o)
    {
        Debug.Log("UIManager::IncreaseHealth");
    }

    public void IncrementUIFlow ()
    {

        // Transition Canvases.

        currentCanvas.gameObject.SetActive(false);
        nextCanvas.gameObject.SetActive(true);
        ui_Flow_Current_Index ++;
        currentCanvas = uiFlowScreens[ui_Flow_Current_Index];
        nextCanvas = uiFlowScreens[ui_Flow_Current_Index + 1];

        // Handle Different cases.
    }

    public void InputPlayerSecret (string newPlayerSecret)
    {
        GameManager.Instance.SetPlayerSecret(currentPlayerIndex, secretInputField.text);
    }
    
}
