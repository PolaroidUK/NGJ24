using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PreSecretScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI warningText;

    [SerializeField] UIManager uiManager;

    // Update is called once per frame
    void Update()
    {

        warningText.text = "Player " + (uiManager.currentPlayerIndex + 1) +  " Shield the screen from prying eyes...";
    }
}
