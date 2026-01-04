using UnityEngine;

public class PlayerLaneMove : MonoBehaviour
{
    [Header("Player Reference (Drag & Drop)")]
    [SerializeField] private Transform playerTransform;

    [Header("Local X Movement")]
    [SerializeField] private float minX = 0f;   // BIÊN TRÁI
    [SerializeField] private float maxX = 6f;   // BIÊN PHẢI
    [SerializeField] private float moveSpeed = 4f;

    private int moveDir = 0; // -1: về minX | 1: về maxX

    private void Update()
    {
        if (moveDir == 0 || playerTransform == null) return;

        Vector3 localPos = playerTransform.localPosition;
        localPos.x += moveDir * moveSpeed * Time.deltaTime;
        localPos.x = Mathf.Clamp(localPos.x, minX, maxX);
        playerTransform.localPosition = localPos;
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
}
