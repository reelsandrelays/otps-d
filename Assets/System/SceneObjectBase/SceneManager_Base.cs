using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_Base : MonoBeWayviour
{
    public static SceneManager_Base Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        UIController.Initialize();
    }
}
