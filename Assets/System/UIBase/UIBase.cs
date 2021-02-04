using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBeWayviour
{
    [SerializeField] protected bool disableOnStart = true;

    protected abstract void Initialize(params object[] parameters);
    protected abstract IEnumerator OpeningAnimation();
    protected abstract IEnumerator ClosingAnimation();

    protected virtual void Awake() { }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (disableOnStart) gameObject.SetActive(false);
    }

    public virtual void Open() { OpeningProcess(null); }
    public virtual void Open(Object parameter) { OpeningProcess(parameter); }
    public virtual void Open(params object[] parameters) { OpeningProcess(parameters); }
    public virtual void Open(int parameter) { OpeningProcess(parameter); }

    public virtual void Close()
    {
        UIController.ClosingUI(this);

        StartAnimationCoroutine(Closing());
    }

    protected void TurnOff() { gameObject.SetActive(false); }

    private IEnumerator Closing()
    {
        IEnumerator closingAnimation = ClosingAnimation();
        //closingAnimation.Reset();

        while (closingAnimation.MoveNext()) { yield return closingAnimation.Current; }

        gameObject.SetActive(false);
    }

    private void OpeningProcess(params object[] parameters)
    {
        UIController.OpeningUI(this);

        gameObject.SetActive(true);

        StartAnimationCoroutine(OpeningAnimation());

        Initialize(parameters);
    }

    private void StartAnimationCoroutine(IEnumerator animationCoroutine)
    {
        StopAllCoroutines();

        StartCoroutine(animationCoroutine);
    }
}
