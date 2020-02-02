using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogChoiceStartedListener :MonoBehaviour
{
    [SerializeField] private DialogChoiceType _event;

    public DialogChoiceType Event => _event;

    [System.Serializable] public class ChoiceDataEvent : UnityEvent<ChoiceData[]> { }

    [SerializeField] public ChoiceDataEvent _onRaised;

    protected void OnDisable() => UnsubscribeSelf();

    protected void OnEnable() => SubscribeSelf();

    public void OnEventRaised(ChoiceData[] args)
    {
        _onRaised?.Invoke(args);
    }

    public void UnsubscribeSelf() => _event.Unsubscribe(this);

    public void SubscribeSelf() => _event.Subscribe(this);
}
