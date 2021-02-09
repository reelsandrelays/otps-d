using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotchResolutionMargin : MonoBehaviour
{
    [SerializeField] private Image top;
    [SerializeField] private Image left;
    [SerializeField] private Image right;
    [SerializeField] private Image bottom;

    public void SetMargins(RectTransform rectTransform)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas == null) return;

        RectTransform canvasTransform = GetComponent<RectTransform>();
        if (canvasTransform == null) return;

        RectTransform resolutionTransform = GetComponent<RectTransform>();

        Vector3[] marginCorners = new Vector3[4];
        resolutionTransform.GetWorldCorners(marginCorners);

        Vector3[] panelCorners = new Vector3[4];
        rectTransform.GetWorldCorners(panelCorners);

        float widthRatio = canvasTransform.lossyScale.x;
        float heightRatio = canvasTransform.lossyScale.y;

        top.rectTransform.sizeDelta = new Vector2(0, (marginCorners[1].y - panelCorners[1].y) / heightRatio);
        left.rectTransform.sizeDelta = new Vector2((panelCorners[0].x - marginCorners[0].x) / widthRatio, 0);
        right.rectTransform.sizeDelta = new Vector2((marginCorners[2].x - panelCorners[2].x) / widthRatio, 0);
        bottom.rectTransform.sizeDelta = new Vector2(0, (panelCorners[0].y - marginCorners[0].y) / heightRatio);
    }
}
