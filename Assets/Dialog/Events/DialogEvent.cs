using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ArgumentCount { ZERO, ONE, TWO, THREE, FOUR, VARIABLE }

[CreateAssetMenu(fileName = "New Dialog Event", menuName = "Dialog/Create New Dialog Event", order = 1)]
public class DialogEvent : ScriptableObject
{
    public ArgumentCount argumentCount = ArgumentCount.ZERO;
    public List<string> aliases;

    List<DialogEventListener> Listeners = new List<DialogEventListener>();

    public bool CheckAliases(string id)
    {
        return id.ToLower().Trim() == name.ToLower().Trim() || aliases.Select(x => x.ToLower().Trim()).ToList().Contains(id.ToLower().Trim());
    }

    public void Invoke(params string[] args)
    {
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i].OnEventRaised(args);
        }
    }

    public void Subscribe(DialogEventListener listener) =>  Listeners.Add(listener);

    public void Unsubscribe(DialogEventListener listener) => Listeners.Remove(listener);
}
