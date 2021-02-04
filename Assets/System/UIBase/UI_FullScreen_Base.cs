using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_FullScreen_Base : UIBase
{
    [SerializeField] private Image background;

    protected override void Awake()
    {
        base.Awake();
        background.rectTransform.sizeDelta = new Vector2(Screen.safeArea.height, 0);
    }
}
