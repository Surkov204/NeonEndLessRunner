using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SwipeInput : MonoBehaviour
{
    [Header("Swipe Settings")]
    [SerializeField] private float swipeThreshold = 50f;
    [SerializeField] private float directionTolerance = 0.7f;

    private Vector2 startPos;
    private bool swiping;

    // EVENTS (KHÔNG MISS INPUT)
    public static event Action OnSwipeUp;
    public static event Action OnSwipeDown;
    public static event Action OnSwipeLeft;
    public static event Action OnSwipeRight;

    void Update()
    {
#if UNITY_EDITOR
        HandleMouse();
#else
        HandleTouch();
#endif
    }

    // =========================
    // MOUSE (EDITOR)
    // =========================
    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI()) return;

            startPos = Input.mousePosition;
            swiping = true;
        }
        else if (Input.GetMouseButtonUp(0) && swiping)
        {
            Vector2 delta = (Vector2)Input.mousePosition - startPos;
            DetectSwipe(delta);
            swiping = false;
        }
    }

    // =========================
    // TOUCH (MOBILE)
    // =========================
    void HandleTouch()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);

        if (t.phase == TouchPhase.Began)
        {
            if (IsPointerOverUI(t.fingerId)) return;

            startPos = t.position;
            swiping = true;
        }
        else if ((t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) && swiping)
        {
            Vector2 delta = t.position - startPos;
            DetectSwipe(delta);
            swiping = false;
        }
    }

    // =========================
    // CORE LOGIC
    // =========================
    void DetectSwipe(Vector2 delta)
    {
        if (delta.magnitude < swipeThreshold) return;

        float absX = Mathf.Abs(delta.x);
        float absY = Mathf.Abs(delta.y);

        if (absY > absX * directionTolerance)
        {
            if (delta.y > 0)
                OnSwipeUp?.Invoke();
            else
                OnSwipeDown?.Invoke();
        }
        else if (absX > absY * directionTolerance)
        {
            if (delta.x > 0)
                OnSwipeRight?.Invoke();
            else
                OnSwipeLeft?.Invoke();
        }
    }

    // =========================
    // UI CHECK
    // =========================
    bool IsPointerOverUI(int fingerId = -1)
    {
        if (EventSystem.current == null) return false;

#if UNITY_EDITOR
        return EventSystem.current.IsPointerOverGameObject();
#else
        return fingerId != -1 && EventSystem.current.IsPointerOverGameObject(fingerId);
#endif
    }
}
