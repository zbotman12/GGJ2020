using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Awake()
    {
        foreach (BasicMovement bm in FindObjectsOfType<BasicMovement>())
        {
            bm.enabled = false;
        }
        foreach (GrabBall bm in FindObjectsOfType<GrabBall>())
        {
            bm.enabled = false;
        }
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
