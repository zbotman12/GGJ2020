using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogChoiceFinishedListener : MonoBehaviour
{
    [SerializeField] public DialogChoiceType _event;

    [System.Serializable] public class StringArgsEvent : UnityEvent<int> { }

    [SerializeField] public StringArgsEvent _onRaisedVariable;

    protected void OnEnable() => SubscribeSelf();

    protected void OnDisable() => UnsubscribeSelf();

    public void OnEventRaised(int choiceIndex)
    {
        _onRaisedVariable?.Invoke(choiceIndex);
    }

    public void UnsubscribeSelf() => _event.Unsubscribe(this);

    public void SubscribeSelf() => _event.Subscribe(this);
}
