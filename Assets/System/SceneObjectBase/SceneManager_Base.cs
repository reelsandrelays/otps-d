using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneManager_Base : MonoBehaviour
{
    public static SceneManager_Base Instance { get; private set; }

    protected abstract IEnumerator CustomUpdate();

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        UIController.Initialize();
    }
}
