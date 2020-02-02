using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog Choice Type Library", menuName = "Dialog/Create New Dialog Choice Type Library", order = 1)]
public class DialogChoiceTypeLibrary : ScriptableObject
{
    [SerializeField] private List<DialogChoiceType> _events = null;

    public void StartDialogChoice(string id, params ChoiceData[] args)
    {
        DialogChoiceType eventToInvoke = _events.Find(x => x.CheckAliases(id));
        if (eventToInvoke == null)
        {
            throw new System.ArgumentException($"DialogChoiceType named {id} not found in dispatcher {name}");
        }
        else
        {
            Debug.Log(eventToInvoke.name);
        }
        eventToInvoke?.StartChoice(args);
    }
}