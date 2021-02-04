using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player_Base : MonoBehaviour
{
    public static Player_Base Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
