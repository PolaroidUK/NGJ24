using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Search;

public class PlayerJoinScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] TextMeshProUGUI pressStartPrompt;

    [SerializeField] UIManager uiManager;

    [SerializeField] PlayerManagerEvents playerManagerEvents;

    [SerializeField] GameObject joinGroup;
    [SerializeField] GameObject proceedGroup;

    void Start ()
    {
        // playerManagerEvents.EnableJoining();
        GameManager.Instance.globalEventManager.AddListener(GlobalEventManager.EventTypes.PlayerJoined, OnPlayerJoined);
        // Subscribe to Event For When Player is Joined.
        // joinGroup.SetActive(true);
        // proceedGroup.SetActive(false);
    }

    void OnEnable ()
    {
        joinGroup.SetActive(true);
        proceedGroup.SetActive(false);
        playerManagerEvents.EnableJoining();
    }

    // Update is called once per frame
    void Update()
    {
        // TEST PLAYER JOIN

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPlayerJoined(null);
        }

        // Set the Text to current player.
        pressStartPrompt.text = "Player " + (uiManager.currentPlayerIndex + 1) + ": Press Start to Join";
        shieldText.text = "Player " + (uiManager.currentPlayerIndex + 1) +  " Shield the screen from prying eyes...";
    }

    public void OnPlayerJoined (object o)
    {
        joinGroup.SetActive(false);
        proceedGroup.SetActive(true);
        GameManager.Instance.audioManager.PlayConfirmUI();
        // Remove Text asking player to join
        // Show eye closing and Shield Text.

        // Set Next Button Available
    }
}
