using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

public static class Extension_Test
{
    [Button]
    public static void SaveMenuStyles(this OdinMenuItem menuItem)
    {
        Debug.Log("Something");
    }
}
