using UnityEngine;
using UnityEngine.Events;

public class DialogEventListener : MonoBehaviour
{
    [SerializeField] public DialogEvent _event;

    [System.Serializable] public class OneArgEvent : UnityEvent<string> { }
    [System.Serializable] public class TwoArgEvent : UnityEvent<string, string> { }
    [System.Serializable] public class ThreeArgEvent : UnityEvent<string, string, string> { }
    [System.Serializable] public class FourArgEvent : UnityEvent<string, string, string, string> { }
    [System.Serializable] public class StringArgsEvent : UnityEvent<string[]> { }

    [SerializeField] public UnityEvent _onRaisedZero;

    [SerializeField] public OneArgEvent _onRaisedOne;

    [SerializeField] public TwoArgEvent _onRaisedTwo;

    [SerializeField] public ThreeArgEvent _onRaisedThree;

    [SerializeField] public FourArgEvent _onRaisedFour;

    [SerializeField] public StringArgsEvent _onRaisedVariable;

    protected void OnEnable() => SubscribeSelf();

    protected void OnDisable() => UnsubscribeSelf();

    public void OnEventRaised(string[] args)
    {
        switch (_event.argumentCount)
        {
            case ArgumentCount.ZERO:
                _onRaisedZero?.Invoke();
                break;

            case ArgumentCount.ONE:
                _onRaisedOne?.Invoke(args[0]);
                break;

            case ArgumentCount.TWO:
                _onRaisedTwo?.Invoke(args[0], args[1]);
                break;

            case ArgumentCount.THREE:
                _onRaisedThree?.Invoke(args[0], args[1], args[2]);
                break;

            case ArgumentCount.FOUR:
                _onRaisedFour?.Invoke(args[0], args[1], args[2], args[3]);
                break;

            case ArgumentCount.VARIABLE:
                _onRaisedVariable?.Invoke(args);
                break;
        }
    }

    public void UnsubscribeSelf() => _event.Unsubscribe(this);

    public void SubscribeSelf() => _event.Subscribe(this);
}
