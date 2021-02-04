using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel_HighPopup_Base : UIBase
{
    [Header("Panel Transform")]
    [SerializeField] private RectTransform top;
    [Tooltip("Flexible")] [SerializeField] private RectTransform middle;
    [SerializeField] private RectTransform bottom;
    [SerializeField] private RectTransform canvas;

    private RectTransform rectTransform;
    private float hiddenY_LocalY;

    protected override void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();
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

    protected override void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, canvas.rect.height * .91f);

        float height = GetComponent<RectTransform>().rect.height;
        middle.sizeDelta = new Vector2(0, height - top.rect.height - bottom.rect.height);

        hiddenY_LocalY = 0f - rectTransform.rect.height;
        rectTransform.localPosition = new Vector3(0, hiddenY_LocalY);

        base.Start();
    }

    private void Lerp(Vector3 startPosition, Vector3 destination, float totalDistance, float elapsedTime)
    {
        float movedDistance = elapsedTime * 6400f;
        float movingProportion = movedDistance / totalDistance;
        rectTransform.localPosition = Vector3.Lerp(startPosition, destination, movingProportion);
    }
}
