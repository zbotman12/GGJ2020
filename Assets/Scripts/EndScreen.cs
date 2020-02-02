using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI winningText;

    public void OnEnable()
    {
        winningText.text = (GameManager.instance.leftWinner ? "Player2" : "Player1") + " Wins!";
    }

    public void Back()
    {
        GameManager.instance.ResetGame();
    }
}
