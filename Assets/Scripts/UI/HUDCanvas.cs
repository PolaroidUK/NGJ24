using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDCanvas : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI countdownNumberText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.globalEventManager.AddListener(GlobalEventManager.EventTypes.BeginCountdown, StartCountdown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCountdown (object o)
    {
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        countdownNumberText.gameObject.SetActive(true);
       
        while (count > 0) {
           
            // display something...
            countdownNumberText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count --;
        }
       
        countdownNumberText.gameObject.SetActive(false);
        // count down is finished...

        // SET GAME CAN START
        // TODO Call Game Manager and Let Game Start. 
        // TODO StartGame();
        Debug.Log("COUNTDOWN FINISHED - BEGIN GAME");
    }

}
