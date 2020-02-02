using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogChoiceStartedListener))]
public class ButtonChoices : DialogChoiceStartedListener
{
    public GameObject dialogChoicePrefab;
    private List<DialogChoiceButton> dialogChoices = new List<DialogChoiceButton>();
    public bool AutoSelectSingleChoices = true;
    private DialogChoiceStartedListener staredListener = null;

    private void Awake()
    {
        staredListener = GetComponent<DialogChoiceStartedListener>();
    }

    public void SpawnButtons(ChoiceData[] choices)
    {
        ClearButtons();
        foreach (ChoiceData choice in choices)
        {
            GameObject choiceButton = Instantiate(dialogChoicePrefab, transform);
            DialogChoiceButton dChoice = choiceButton.GetComponent<DialogChoiceButton>();
            dChoice.ShowText(choice.args[0]);
            dChoice.AddListenerOnClick(() => ChooseChoiceIndex(choice.index));
            dialogChoices.Add(dChoice);
        }

        if (dialogChoices.Count == 1 && AutoSelectSingleChoices)
        {
            ChooseChoiceIndex(choices[0].index);
        }
    }

    void ChooseChoiceIndex(int index)
    {
        ClearButtons();
        staredListener.Event.FinishChoice(index);
    }

    void ClearButtons()
    {
        dialogChoices.ForEach(g => Destroy(g.gameObject));
        dialogChoices.Clear();
    }
}
