using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog Event Library", menuName = "Dialog/Create New Dialog Event Library", order = 1)]
public class DialogEventLibrary : ScriptableObject
{
    [SerializeField] private List<DialogEvent> _events = null;

    public void InvokeEvent(string id, params string[] args)
    {
        DialogEvent eventToInvoke = _events.Find(x => x.CheckAliases(id));
        if(eventToInvoke == null)
        {
            throw new System.ArgumentException($"DialogEvent named {id} not found in dispatcher {name}");
        }
        eventToInvoke?.Invoke(args);
	}
}