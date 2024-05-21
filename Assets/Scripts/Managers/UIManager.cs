//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Unity.PlasticSCM.Editor.WebApi;

public class UIManager : MonoBehaviour
{
    GlobalEventManager _globalEventManager;
    [SerializeField] PlayerController playerController;
    [SerializeField] List<GameObject> introFlowCanvases;
    [SerializeField] TMP_InputField secretInputField;
    [SerializeField] GameObject introFlowCollection;
    public int currentPlayerIndex;
    public int currentUIFlowIndex;
    public GameObject currentCanvas;
    public GameObject nextCanvas;

    [SerializeField] GameOverCanvas gameOverCanvas;

    void  Start()
    {
         //healthText.text = playerController.health.ToString();
        currentCanvas = introFlowCanvases[currentUIFlowIndex];
        nextCanvas = introFlowCanvases[currentUIFlowIndex + 1];

        currentCanvas.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);

        // for (int i = currentUIFlowIndex + 1; i < introFlowCanvases.Count; i++)
        // {
        //     introFlowCanvases[i].SetActive(false);
        // }
    }
    void  OnEnable()
    {
        currentUIFlowIndex = 0;
        currentPlayerIndex = 0;
         //healthText.text = playerController.health.ToString();
        currentCanvas = introFlowCanvases[currentUIFlowIndex];
        nextCanvas = introFlowCanvases[currentUIFlowIndex + 1];

        currentCanvas.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);

        // for (int i = currentUIFlowIndex + 1; i < introFlowCanvases.Count; i++)
        // {
        //     introFlowCanvases[i].SetActive(false);
        // }
    }

    public void Reset()
    {
        currentUIFlowIndex = 0;
        currentPlayerIndex = 0;
        currentCanvas = introFlowCanvases[currentUIFlowIndex];
        nextCanvas = introFlowCanvases[currentUIFlowIndex + 1];

        currentCanvas.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        //TEST to see the game over screen.
        // if(Input.GetKey(KeyCode.Escape))
        // {
        //     Debug.Log("SPACE BAR");
        //     GameManager.Instance.GameWonByPlayer(0);
        // }
    }

    public void Initialize (GlobalEventManager globalEventManager)
    {
        _globalEventManager = globalEventManager;
        _globalEventManager.AddListener(GlobalEventManager.EventTypes.GameOver, OnGameOver);
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

            introFlowCollection.SetActive(false);

            // TODO - Start Dialogue Sequence.

            // TEST - Start Countdown.
            _globalEventManager.Dispatch(GlobalEventManager.EventTypes.BeginCountdown, null);

        } else 
        {
            currentUIFlowIndex ++;
            currentCanvas.SetActive(false);
            nextCanvas.SetActive(true);

            currentCanvas = introFlowCanvases[currentUIFlowIndex];
            nextCanvas = introFlowCanvases[currentUIFlowIndex + 1];
        }

        GameManager.Instance.audioManager.PlayUIClick();

        // Behave differently depending on the current flow index.
    }

    public void SubmitPlayerSecret ()
    {
        GameManager.Instance.SetPlayerSecret(currentPlayerIndex, secretInputField.text);
        // Clear The Text Field.
        secretInputField.text = "";
    }

    public void OnGameOver (object playerWhoLostIdObject)
    {
        gameOverCanvas.gameObject.SetActive(true);
        gameOverCanvas.SetPlayerSecret((int)playerWhoLostIdObject);
        
    }
}