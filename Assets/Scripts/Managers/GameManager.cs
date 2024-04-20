using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
