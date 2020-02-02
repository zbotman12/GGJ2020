using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{    
    public void Awake()
    {
        FinishGame();
    }
    public void FinishGame()
    {
        GameManager.instance.player1.GetComponent<BasicMovement>().enabled = false;
        GameManager.instance.player2.GetComponent<BasicMovement>().enabled = false;
        GameManager.instance.player1.GetComponent<GrabBall>().enabled = false;
        GameManager.instance.player2.GetComponent<GrabBall>().enabled = false;

        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }
    public void StartGame()
    {
        GameManager.instance.StartGame();
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
              Application.Quit();
        #endif
    }
}
