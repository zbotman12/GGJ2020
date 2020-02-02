using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class DialogChoiceButton : MonoBehaviour
{
    private TextMeshProUGUI text;

    public void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowText(string _optionText)
    {
        text.text = _optionText;
    }

    public void AddListenerOnClick(UnityAction action)
    {
        GetComponent<Button>().onClick.AddListener(action);
    }

    public void SelectChoice()
    {
        //controller.FinishChoice(choiceIndex);
    }
}
