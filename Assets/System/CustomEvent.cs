using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event_", menuName = "Events/Custom Event: Default")]
public class CustomEvent : ScriptableObject
{
    public event EventResponse Response;

    public void Invoke() { Response?.Invoke(); }

    public delegate void EventResponse();

#if UNITY_EDITOR
    [Space]
    [Space]
    [TextArea] public string tooltip;
#endif
}

public class CustomEvent<T> : ScriptableObject
{
    public event EventResponse Response;

    public void Invoke(T parameter) { Response?.Invoke(parameter); }

    public delegate void EventResponse(T parameter);

#if UNITY_EDITOR
    [Space]
    [Space]
    [TextArea] public string tooltip;
#endif
}

public class CustomEvent<T1, T2> : ScriptableObject
{
    public event EventResponse Response;

    public void Invoke(T1 parameter1, T2 parameter2) { Response?.Invoke(parameter1, parameter2); }

    public delegate void EventResponse(T1 parameter1, T2 parameter2);

#if UNITY_EDITOR
    [Space]
    [Space]
    [TextArea] public string tooltip;
#endif
}

public class CustomEvent<T1, T2, T3> : ScriptableObject
{
    public event EventResponse Response;

    public void Invoke(T1 parameter1, T2 parameter2, T3 parameter3) { Response?.Invoke(parameter1, parameter2, parameter3); }

    public delegate void EventResponse(T1 parameter1, T2 parameter2, T3 parameter3);

#if UNITY_EDITOR
    [Space]
    [Space]
    [TextArea] public string tooltip;
#endif
}

public class CustomEvent<T1, T2, T3, T4> : ScriptableObject
{
    public event EventResponse Response;

    public void Invoke(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) { Response?.Invoke(parameter1, parameter2, parameter3, parameter4); }

    public delegate void EventResponse(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);

#if UNITY_EDITOR
    [Space]
    [Space]
    [TextArea] public string tooltip;
#endif
}
