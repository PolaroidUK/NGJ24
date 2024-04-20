using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;

public class UIManager : MonoBehaviour
{
    GlobalEventManager _globalEventManager;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] PlayerController playerController;
    [SerializeField] List<GameObject> introFlowCanvases;
    [SerializeField] TMP_InputField secretInputField;

    public int currentPlayerIndex;
    public int currentUIFlowIndex;
    public GameObject currentCanvas;
    public GameObject nextCanvas;

    public int currentPlayerNumber;
    public int currentOpponentNumber;

    void  Start()
    {
         //healthText.text = playerController.health.ToString();
        currentCanvas = introFlowCanvases[currentUIFlowIndex];
        nextCanvas = introFlowCanvases[currentUIFlowIndex + 1];

        currentCanvas.SetActive(true);

        // for (int i = currentUIFlowIndex + 1; i < introFlowCanvases.Count; i++)
        // {
        //     introFlowCanvases[i].SetActive(false);
        // }
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

    public void IncrementUIFlow()
    {
        // After Player 1 submits Secret
        if (currentUIFlowIndex == 1 && currentPlayerIndex == 0)
        {
            currentPlayerIndex ++;
        } 
        
        if ( currentUIFlowIndex == introFlowCanvases.Count - 1) // End Of Flow
        {
            // Switch off canvas and end flow
            currentCanvas.SetActive(false);
            // Load Dialogue into HUD Canvas.
            GameManager.Instance.PrintPlayerSecrets();

        } else 
        {
            currentUIFlowIndex ++;
            currentCanvas.SetActive(false);
            nextCanvas.SetActive(true);

            currentCanvas = introFlowCanvases[currentUIFlowIndex];
            nextCanvas = introFlowCanvases[currentUIFlowIndex + 1];
        }

        // Behave differently depending on the current flow index.
    }

    public void SubmitPlayerSecret ()
    {
        GameManager.Instance.SetPlayerSecret(currentPlayerIndex, secretInputField.text);
        // Clear The Text Field.
        secretInputField.text = "";
    }
    
}
