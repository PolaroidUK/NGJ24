using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Static (Singleton) reference to this Manager Locator class
    public static GameManager Instance {get; private set;}

    // Services / Managers
    public GlobalEventManager globalEventManager {get; private set;}
    public UIManager uiManager {get; private set;}
    public AudioManager audioManager {get; private set;}

    public Dictionary<int,string> playerSecrets = new Dictionary<int, string>();

    private void Awake()
    {
        // Check if already created.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Debug.LogError("GameManager tried to create itself twice!");
        }

        DontDestroyOnLoad(this);

        // Set the static Instance to this.
        Instance = this;

        // Global Event Manager Setup
        globalEventManager = GetComponentInChildren<GlobalEventManager>();
        audioManager = GetComponentInChildren<AudioManager>();

        // UI Manager Setup
        uiManager = GetComponentInChildren<UIManager>();
        uiManager.Initialize(globalEventManager);

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnDestroy()
    {
        Debug.Log("Manager Locator OnDestroy!");
        // Destroy(audioManager);
    }

    public void QuitApplication()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void ReturnToStartScreen()
    {
        Debug.Log("Return To Start Screen");
        SceneManager.LoadScene(0);
    }


    public void SetPlayerSecret(int playerIndex, string playerSecretString)
    {
        Debug.Log("GameManager::SetPlayerSecret index - " + playerIndex + " secret = " + playerSecretString);
        if (playerSecrets.ContainsKey(playerIndex))
        {
            Debug.LogError("Player Secret already exists for this player! index = " + playerIndex); 
        } else {
            // create the secret.
            Debug.Log("Adding Player Secret - " + playerSecretString);
            playerSecrets.Add(playerIndex,playerSecretString);
        }
    }
    public string GetPlayerSecret(int playerIndex)
    {
        Debug.Log("GameManager::GetPlayerSecret index - " + playerIndex);
        if (playerSecrets.ContainsKey(playerIndex))
        {
            return playerSecrets[playerIndex];
        } else {
            // create the secret.
            Debug.LogError("Player Secret DOES NOT EXIST for player = " + playerIndex); 
            return null;
        }
    }


    public void PrintPlayerSecrets ()
    {
        foreach (string secret in playerSecrets.Values)
        {
            Debug.Log("Player Secret - " + secret);
        }
    }

    public void PlayersJoined()
    {
        
    }

    public void GameWonByPlayer(int playerIDWhoLost)
    {
        Debug.Log("Game Over - Who lost? " + playerIDWhoLost);
        // Set Game Over with player who lost Id.
        globalEventManager.Dispatch(GlobalEventManager.EventTypes.GameOver, playerIDWhoLost);
    }

    void OnSceneLoaded (Scene sceneloaded, LoadSceneMode mode)
    {
        if (sceneloaded.buildIndex == 0)
        {
            OnStartScreenLoaded();
        } 
        else if (sceneloaded.buildIndex == 1)
        {
            OnMainGameLoaded();
        }
    }


    public void OnMainGameLoaded()
    {
        Debug.Log("On Main Game Loaded");
        uiManager.gameObject.SetActive(true);
    }
    public void OnStartScreenLoaded()
    {
        Debug.Log("On Start Screen Loaded");
        uiManager.gameObject.SetActive(false);
        playerSecrets = new  Dictionary<int, string>();
    }
}
