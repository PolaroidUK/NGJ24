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

        // Global Even Manager Setup
        globalEventManager = GetComponentInChildren<GlobalEventManager>();

        // UI Manager Setup
        uiManager = GetComponentInChildren<UIManager>();

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
}
