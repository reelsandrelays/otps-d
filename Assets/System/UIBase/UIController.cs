using System.Collections;
using System.Collections.Generic;

public class UIController
{
    public static UIBase CurrentUI { get; private set; }

    private int LastIndex { get { return openedUI.Count - 1; } }

    private static UIController instance = null;

    private List<UIBase> openedUI;

    public static void CloseAll()
    {
        if (instance.openedUI == null) return;

        for (int i = instance.openedUI.Count - 1; i >= 0; i--) { instance.openedUI[i].Close(); }

        instance.openedUI = new List<UIBase>();
        CurrentUI = null;
    }

    public static void Initialize()
    {
        instance = new UIController();

        instance.openedUI = new List<UIBase>();

        CurrentUI = null;
    }

    public static void ClosingUI(UIBase uI)
    {
        if (instance == null)
        {
            Initialize();
            return;
        }

        if (instance.openedUI.Contains(uI)) instance.openedUI.Remove(uI);

        CurrentUI = (instance.openedUI.Count > 0) ? instance.openedUI[instance.LastIndex] : null;
    }

    public static void OpeningUI(UIBase uI)
    {
        if (instance == null) Initialize();

        if (instance.openedUI == null) instance.openedUI = new List<UIBase>();

        if (!instance.openedUI.Contains(uI)) instance.openedUI.Add(uI);

        instance.SetCurrentUI(uI);
    }

    private void SetCurrentUI(UIBase uI)
    {
        if (instance.openedUI == null) instance.openedUI = new List<UIBase>();

        if (!instance.openedUI.Contains(uI)) instance.openedUI.Add(uI);

        instance.openedUI.Remove(uI);
        instance.openedUI.Add(uI);

        CurrentUI = instance.openedUI[LastIndex];
    }
}
