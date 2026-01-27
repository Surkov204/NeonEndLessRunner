using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLaneMove : MonoBehaviour
{
    [Header("Player Reference (Drag & Drop)")]
    [SerializeField] private Transform playerTransform;

    [Header("Local X Movement")]
    [SerializeField] private float minX = 0f;   
    [SerializeField] private float maxX = 6f;   
    [SerializeField] private float moveSpeed = 4f;

    public float CurrentSpeed => moveSpeed;
    private const float MIN_SPEED = 4f;
    private int moveDir = 0; 

    private void Update()
    {
        HandleMovement();
        HandleKeyboardInput();
    }

    private void HandleMovement()
    {
        if (moveDir == 0 || playerTransform == null) return;

        Vector3 localPos = playerTransform.localPosition;
        localPos.x += moveDir * moveSpeed * Time.deltaTime;
        localPos.x = Mathf.Clamp(localPos.x, minX, maxX);
        playerTransform.localPosition = localPos;
    }

    private void HandleKeyboardInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Keyboard.current == null) return;

        // Giữ phím → move
        if (Keyboard.current.wKey.isPressed)
        {
            MoveUpStart();
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            MoveDownStart();
        }

        // Nhả phím → stop (GIỐNG thả nút UI)
        if (Keyboard.current.wKey.wasReleasedThisFrame ||
            Keyboard.current.sKey.wasReleasedThisFrame)
        {
            StopMove();
        }
#endif

    }

    // ===== UI EVENTS =====
    public void MoveUpStart()    // VỀ 0
    {
        moveDir = -1;
    }

    public void MoveDownStart()  // VỀ 6
    {
        moveDir = 1;
    }

    public void StopMove()
    {
        moveDir = 0;
    }

    public void ApplyStats(CharacterStats stats)
    {
        moveSpeed = Mathf.Max(MIN_SPEED, stats.moveSpeed);
    }
}
