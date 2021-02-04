using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel_RisingPopup : UIBase
{
    private RectTransform rectTransform;
    private float hiddenY_LocalY;

    protected override void Awake()
    {
        base.Awake();

        rectTransform = GetComponent<RectTransform>();

        hiddenY_LocalY = 0f - rectTransform.rect.height;

        rectTransform.localPosition = new Vector3(0, hiddenY_LocalY);
    }

    protected override IEnumerator ClosingAnimation()
    {
        Vector3 startPosition = rectTransform.localPosition;
        Vector3 destination = new Vector3(rectTransform.localPosition.x, hiddenY_LocalY);
        float totalDistance = Vector3.Distance(startPosition, destination);

        float elapsedTime = 0.0f;

        while (rectTransform.localPosition.y > hiddenY_LocalY)
        {
            elapsedTime += Time.deltaTime;

            Lerp(startPosition, destination, totalDistance, elapsedTime);

            yield return null;
        }

        //gameObject.SetActive(false);
    }

    protected override IEnumerator OpeningAnimation()
    {
        Vector3 startPosition = rectTransform.localPosition;
        Vector3 destination = new Vector3(rectTransform.localPosition.x, 0);
        float totalDistance = Vector3.Distance(startPosition, destination);

        float elapsedTime = 0.0f;

        while (rectTransform.localPosition.y < 0)
        {
            elapsedTime += Time.deltaTime;

            Lerp(startPosition, destination, totalDistance, elapsedTime);

            yield return null;
        }
    }

    private void Lerp(Vector3 startPosition, Vector3 destination, float totalDistance, float elapsedTime)
    {
        float movedDistance = elapsedTime * 4096f;
        float movingProportion = movedDistance / totalDistance;
        rectTransform.localPosition = Vector3.Lerp(startPosition, destination, movingProportion);
    }
}
