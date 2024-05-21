using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] int mainGameSceneIndex = 1;
    [SerializeField] RectTransform fadeCanvasTransform;
    [SerializeField] Animator canvasAnimator;

    [SerializeField] FMODUnity.EventReference startclickRef;

    void Start()
    {
        fadeCanvasTransform.gameObject.SetActive(true);
    }

    public void OnPressStart()
    {
        // Fade Canvas
        StartCoroutine(LoadMainGame());
    }

    IEnumerator LoadMainGame()
    {
        // Fade in Canvas
        canvasAnimator.SetTrigger("Start");
        FMODUnity.RuntimeManager.PlayOneShot(startclickRef);
        yield return new WaitForSeconds(1);
        // Start Game.
        SceneManager.LoadScene(mainGameSceneIndex);
    }
}
