using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SecretScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentPlayerText;

    [SerializeField] TMP_InputField inputField;

    [SerializeField] Button submitButton;

    [SerializeField] UIManager uiManager;
    // Update is called once per frame
    void Update()
    {
        currentPlayerText.text = "Player " + uiManager.currentPlayerIndex + 1;

        if (inputField.text != null && inputField.text != "")
        {
            submitButton.interactable = true;
        } else
        {
            submitButton.interactable = false;
        }

    }
}
