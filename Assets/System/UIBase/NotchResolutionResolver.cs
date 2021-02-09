using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotchResolutionResolver : MonoBehaviour
{
    [SerializeField] private NotchResolutionMargin margin;

    void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        Vector2 anchorMin = Screen.safeArea.position;
        Vector2 anchorMax = Screen.safeArea.position + Screen.safeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

        if (margin != null) margin.SetMargins(rectTransform);
    }
}
