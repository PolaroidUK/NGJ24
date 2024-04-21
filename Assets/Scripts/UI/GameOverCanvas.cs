using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI secretText;
    [SerializeField] TextMeshProUGUI headingText;

    public void SetPlayerSecret(int playerWhoLostId)
    {
        headingText.text = "Player " + (playerWhoLostId + 1) + "'s secret is...";

        string secret = GameManager.Instance.GetPlayerSecret(playerWhoLostId);
        secretText.text = secret;
        gameObject.SetActive(true);
    }
}
