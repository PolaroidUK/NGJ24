using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecretScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentPlayerText;

    [SerializeField] UIManager uiManager;
    // Update is called once per frame
    void Update()
    {
        currentPlayerText.text = "Player " + uiManager.currentPlayerIndex + 1;
    }
}
