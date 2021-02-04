using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Pinching
{
    None,
    In,
    Out
}

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public static void TurnOn() { Instance.gameObject.SetActive(true); }
    public static void TurnOff()
    {
        InputOn = false;
        InputRunning = false;
        InputOff = false;
        HitUI = false;
        MouseWheelValue = 0f;
        CurrentTouchCount = 0;
        Instance.gameObject.SetActive(false);
    }

    public static Vector2 InitialPosition { get; private set; }
    public static Vector2 CurrentPosition { get; private set; }

    public static float MouseWheelValue { get; private set; }
    public static float PinchValue { get; private set; }
    public static int InitialTouchCount { get; private set; }
    public static int CurrentTouchCount { get; private set; }

    public static bool InputOn { get; private set; }
    public static bool InputOff { get; private set; }
    public static bool InputRunning { get; private set; }
    public static bool HitUI { get; private set; }

    public static Pinching PinchingState { get; private set; }
    public static bool Dragged { get { return Vector2.Distance(CurrentPosition, InitialPosition) > 16; } }
    public static bool Dragged2 { get { return Vector2.Distance(CurrentPosition, InitialPosition) > 64; } }

    public static bool DraggedLeft { get { return CurrentPosition.x - InitialPosition.x <= -64f; } }
    public static bool DraggedRight { get { return CurrentPosition.x - InitialPosition.x >= 64f; } }
    public static bool DraggedUp { get { return CurrentPosition.y - InitialPosition.y >= 64f; } }
    public static bool DraggedDown { get { return CurrentPosition.y - InitialPosition.y <= -64f; } }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        Input_PC();
#elif UNITY_ANDROID || UNITY_IOS
        Input_MobileDevice();
#endif
    }

    private void Input_PC()
    {
        InputOn = Input.GetMouseButtonDown(0);
        InputRunning = Input.GetMouseButton(0);
        InputOff = Input.GetMouseButtonUp(0);
        HitUI = EventSystem.current.IsPointerOverGameObject(-1);

        MouseWheelValue = Input.mouseScrollDelta.y;

        CurrentPosition = Input.mousePosition;

        if (InputOn) InitialPosition = Input.mousePosition;
    }

    private void Input_MobileDevice()
    {
        CurrentTouchCount = Input.touchCount;

        if (CurrentTouchCount > 0)
        {
            if (CurrentTouchCount == 1) TouchCount1(Input.GetTouch(0));
            else if (CurrentTouchCount == 2) TouchCount2(Input.GetTouch(0), Input.GetTouch(1));
        }
        else
        {
            PinchValue = 0.0f;
            PinchingState = Pinching.None;
            InitialTouchCount = 0;
        }
    }

    private void TouchCount2(Touch touch0, Touch touch1)
    {
        if (InitialTouchCount != 2) InitialTouchCount = 2;

        Vector2 previousPosition0 = touch0.position - touch0.deltaPosition;
        Vector2 previousPosition1 = touch1.position - touch1.deltaPosition;

        float previousDistance = Vector2.Distance(previousPosition0, previousPosition1);
        float currentDistance = Vector2.Distance(touch0.position, touch1.position);

        PinchValue = currentDistance - previousDistance;

        if (PinchValue > 0f) PinchingState = Pinching.In;
        else if (PinchValue < 0f) PinchingState = Pinching.Out;
        else PinchingState = Pinching.None;
    }

    private void TouchCount1(Touch touch)
    {
        PinchValue = 0.0f;
        PinchingState = Pinching.None;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                {
                    if (InitialTouchCount < 1) InitialTouchCount = 1;
                    InputOn = true;
                    InputOff = false;
                    InputRunning = true;
                    InitialPosition = touch.position;
                    CurrentPosition = touch.position;
                    HitUI = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                    break;
                }
            case TouchPhase.Moved:
                {
                    InputOn = false;
                    InputOff = false;
                    InputRunning = true;
                    CurrentPosition = touch.position;
                    break;
                }
            case TouchPhase.Stationary:
                {
                    InputOn = false;
                    InputOff = false;
                    InputRunning = true;
                    CurrentPosition = touch.position;
                    break;
                }
            case TouchPhase.Ended:
                {
                    if (InitialTouchCount > 0) InitialTouchCount = 0;
                    InputOn = false;
                    InputOff = true;
                    InputRunning = false;
                    CurrentPosition = touch.position;
                    break;
                }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
