using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ModalSpreadAnimation
{
    Both,
    VerticalOnly
}

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public abstract class UIPanel_Modal_Reformed : UIBase, IPointerClickHandler
{
    [SerializeField] protected Transform[] spreadPanel;

    private readonly float appearanceSpeed = 5f;

    private CanvasGroup canvasGroup;
    private Image backFilm;

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponent<CanvasGroup>();
        backFilm = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData) { if (eventData.pointerCurrentRaycast.gameObject == backFilm.gameObject && eventData.pointerPressRaycast.gameObject == backFilm.gameObject) Close(); }

    protected override abstract void Initialize(params object[] parameters);
    protected virtual void PostOpeningAnimation() { }

    protected override IEnumerator OpeningAnimation()
    {
        float t = 0f;

        while (t < 1f / appearanceSpeed)
        {
            t += Time.deltaTime;

            float a = Mathf.Lerp(0f, .6f, t * appearanceSpeed);
            for (int i = 0; i < spreadPanel.Length; i++) { spreadPanel[i].localScale = Vector3.Lerp(InitialScale, Vector3.one, t * appearanceSpeed); }
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t * appearanceSpeed);

            yield return null;
        }

        canvasGroup.interactable = true;
        PostOpeningAnimation();
    }

    protected override IEnumerator ClosingAnimation()
    {
        canvasGroup.interactable = false;

        float t = 0f;

        while (t < 1f / appearanceSpeed)
        {
            t += Time.deltaTime;

            float a = Mathf.Lerp(.6f, 0f, t * appearanceSpeed);
            for (int i = 0; i < spreadPanel.Length; i++) { spreadPanel[i].localScale = Vector3.Lerp(Vector3.one, InitialScale, t * appearanceSpeed); }
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t * appearanceSpeed);

            yield return null;
        }
    }

    private readonly Vector3 InitialScale = new Vector3(.9f, .9f);
}
