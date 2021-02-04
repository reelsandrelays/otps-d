using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevelopmentConsole : MonoBehaviour
{
    private Text log;
    private static DevelopmentConsole instance = null;

    public static void ConsoleLog(string log) { if (instance != null) instance.log.text += "\n" + log; }

    private void Awake()
    {
        instance = this;
        log = GetComponent<Text>();
        gameObject.SetActive(false);
    }
}
