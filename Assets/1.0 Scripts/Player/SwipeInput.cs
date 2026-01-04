using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeInput : MonoBehaviour
{
    public float swipeThreshold = 80f;

    private Vector2 startPos;
    private bool swiping = false;

    public static bool SwipeUpDetected;
    public static bool SwipeDownDetected;

    void Update()
    {
        SwipeUpDetected = false;
        SwipeDownDetected = false;

#if UNITY_EDITOR
        HandleMouse();
#else
        HandleTouch();
#endif
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            startPos = Input.mousePosition;
            swiping = true;
        }
        else if (Input.GetMouseButtonUp(0) && swiping)
        {
            Vector2 delta = (Vector2)Input.mousePosition - startPos;

            if (Mathf.Abs(delta.y) > swipeThreshold && Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
            {
                if (delta.y > 0)
                    SwipeUpDetected = true;
                else
                    SwipeDownDetected = true;
            }

            swiping = false;
        }
    }

    void HandleTouch()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);

        if (t.phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(t.fingerId)) return;
            startPos = t.position;
            swiping = true;
        }
        else if (t.phase == TouchPhase.Ended && swiping)
        {
            Vector2 delta = t.position - startPos;

            if (Mathf.Abs(delta.y) > swipeThreshold && Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
            {
                if (delta.y > 0)
                    SwipeUpDetected = true;
                else
                    SwipeDownDetected = true;
            }

            swiping = false;
        }
    }
}
