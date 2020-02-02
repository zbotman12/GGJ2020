using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public struct ChoiceData
{
    public string[] args;
    public int index;
    public string namedIndex;
}


[CreateAssetMenu(fileName = "New Dialog Choice Type", menuName = "Dialog/New Dialog Choice Type", order = 1)]
public class DialogChoiceType : ScriptableObject
{
    public ArgumentCount argumentCount = ArgumentCount.ZERO;

    public List<string> possibleOutcomes;

    public List<string> aliases;

    List<DialogChoiceStartedListener> OnStartedListeners;

    List<DialogChoiceFinishedListener> OnFinishedListeners;

    public bool CheckAliases(string id)
    {
        return id.ToLower().Trim() == name.ToLower().Trim() || aliases.Select(x => x.ToLower().Trim()).ToList().Contains(id.ToLower().Trim());
    }

    public void StartChoice(params ChoiceData[] args)
    {
        for (int i = OnStartedListeners.Count - 1; i >= 0; i--)
        {
            OnStartedListeners[i].OnEventRaised(args);
        }
    }

    public void FinishChoice(int choiceIndex)
    {
        for (int i = OnFinishedListeners.Count - 1; i >= 0; i--)
        {
            OnFinishedListeners[i].OnEventRaised(choiceIndex);
        }
    }

    public void Subscribe(DialogChoiceStartedListener listener) => OnStartedListeners.Add(listener);

    public void Unsubscribe(DialogChoiceStartedListener listener) => OnStartedListeners.Remove(listener);

    public void Subscribe(DialogChoiceFinishedListener listener) => OnFinishedListeners.Add(listener);

    public void Unsubscribe(DialogChoiceFinishedListener listener) => OnFinishedListeners.Remove(listener);
}
