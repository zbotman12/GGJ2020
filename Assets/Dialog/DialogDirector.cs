using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public class DialogDirector : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkJSONAsset = null;
    [SerializeField] private CharacterDirector _characterDirector = null;
    [SerializeField] private DialogEventLibrary _tagEventsLibrary = null;
    [SerializeField] private DialogChoiceTypeLibrary _choiceTypesLibrary = null;

    public float nextContinueTime = 0.5f;

    public static Story story;

    private List<DialogBox> dialogBoxes;

    private DialogBox selectedDialogBox;
    private string currentDialogText;
    private float timer = 0;
    private bool canContinue = true;

    bool waitingForTime = false;
    bool waitingForEvent = false;
    float waitToContinueTimer = 0;

    private void WaitForSeconds(float time)
    {
        waitingForTime = true;
        waitToContinueTimer = time;
    }

    public void WaitForSeconds(string time)
    {
        waitingForTime = true;
        waitToContinueTimer = float.Parse(time);
    }

    public void WaitForSomeEvent()
    {
        //If we wait for time and an event at the same time, this breaks.
        waitingForEvent = true;
        canContinue = false;
        selectedDialogBox.SetActiveState(false);
    }

    public void AwaitedEventInvoked()
    {
        waitingForEvent = false;
        canContinue = true;
        //This line has to come before finish continue right now 
        //or else the text type coroutine will cry.
        selectedDialogBox.SetActiveState(true);
        FinishContinue();
    }

    public void HandleTagsSOEvents()
    {
        foreach (string inkTag in story.currentTags)
        {
            List<string> tokens = new List<string>(inkTag.Trim().Split(':'));
            _tagEventsLibrary.InvokeEvent(tokens[0].Trim().ToLower(), 
                tokens.GetRange(1, tokens.Count - 1).ToArray());
        }
    }

    public virtual void HandleChoices()
    {
        canContinue = false;

        string choiceType = story.currentChoices[0].text.Trim().Split(':')[0];
        List<ChoiceData> choiceDatas = new List<ChoiceData>();

        foreach (Choice choice in story.currentChoices)
        {
            List<string> tokens = new List<string>(choice.text.Trim().Split(':'));
            choiceDatas.Add(new ChoiceData
            {
                index = choice.index,
                namedIndex = tokens[1],
                args = tokens.GetRange(2, story.currentChoices.Count - 1).ToArray()
            });
        }
        _choiceTypesLibrary.StartDialogChoice(choiceType, choiceDatas.ToArray());
    }

    public virtual void HandleDialog()
    {
        DialogLine dLine = new DialogLine();
        string[] tokens = currentDialogText.Trim().Split(':');
        List<string> tempTokens = new List<string>();

        for (int i = 0; i < tokens.Length; i++)
        {
            if(tokens[i].EndsWith("\\"))
            {
                //Debug.Log(i);
                //LOL IDK
                ///tempTokens.Add((tokens[i] + ":" + tokens[i + i]).Trim());
            }
            else
            {
                tempTokens.Add(tokens[i].Trim());
            }
        }

        tokens = tempTokens.ToArray();
        //Debug.Log(tokens.Length);
        switch (tokens.Length)
        {
            case 4:
                dLine.character = tokens[1];
                dLine.emote = tokens[2];
                dLine.line = tokens[3];
                break;
            case 3:
                dLine.character = tokens[0];
                dLine.emote = tokens[1];
                dLine.line = tokens[2];
                break;
            case 2:
                dLine.character = tokens[0];
                dLine.line = tokens[1];
                break;
            case 1:
                dLine.line = tokens[0];
                break;
        }

        if (dLine.line == "EMPTY_LINE")
        {
            Continue();
        }
        else
        {
            DisplayDialogLine(dLine);
        }
    }

    public void Continue()
    {
        if (story.canContinue)
        {
            //Wait why the fuck are we continuing anyways if we are trying to wait? just to parse tags?
            //Seems error prone, if we continue more than once here it breaks?
            currentDialogText = story.Continue();

            HandleTagsSOEvents();

            if (waitingForTime || waitingForEvent)
            {
                return;
            }
            else
            {
                FinishContinue();
            }
        }
        else
        {
            selectedDialogBox.SetActiveState(false);
        }
    }

    public void DisplayDialogLine(DialogLine line)
    {
        _characterDirector.HighlightCharacter(line.character);
        selectedDialogBox.DisplayLine(line);
    }

    public void HideAllDialogBoxes() => dialogBoxes.ForEach(x => x.SetActiveState(false));


    private void FinishContinue()
    {
        HandleDialog();

        if (story.currentChoices.Count > 0)
        {
            //Need to disable some dialog boxes and stuff here
            //Fire an event
            HandleChoices();
        }


    }

    public void FinishChoice(Int32 choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        selectedDialogBox.gameObject.SetActive(true);
        canContinue = true;
        Continue();
    }

    private void Start()
    {
        dialogBoxes = new List<DialogBox>(FindObjectsOfType<DialogBox>());

        story = new Story(inkJSONAsset.text);

        foreach (DialogBox dbox in dialogBoxes)
        {
            dbox.Setup(this);
            if (dbox.isDefault) selectedDialogBox = dbox;
        }

        Continue();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        waitToContinueTimer -= Time.deltaTime;

        if(!waitingForEvent && (waitingForTime && waitToContinueTimer <= 0))
        {
            waitingForTime = false;
            FinishContinue();
        }

        if (selectedDialogBox != null && selectedDialogBox.clickAnywhere && timer <= 0 && canContinue)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                timer = nextContinueTime;
                Continue();
            }
        }
    }
}
