using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneEventsManager_Base : MonoBehaviour
{
    public static SceneEventsManager_Base Instance { get; private set; }

    protected abstract void SetEvents();

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SetEvents();
    }
}
