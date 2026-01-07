using UnityEngine;

public class NPCVehicle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stopDistance = 0.1f;
    [SerializeField] private float retreatDistance = 50f;

    private Vector3 targetPosition;
    private Vector3 forwardDir;

    private bool isMoving;
    private bool isRetreating;
    private bool retreatTargetSet;

    private Vector3 spawnPosition;

    public void InitSpawnPoint(Vector3 spawnPos)
    {
        spawnPosition = spawnPos;
    }

    public void MoveTo(Vector3 target)
    {
        targetPosition = target;
        forwardDir = (target - transform.position).normalized;
        isMoving = true;
        isRetreating = false;
        retreatTargetSet = false;
    }

    public void Retreat()
    {
        targetPosition = spawnPosition;
        isMoving = true;
        isRetreating = true;
    }

    private void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) <= stopDistance)
        {
            transform.position = targetPosition;
            isMoving = false;

            if (isRetreating)
            {
                Destroy(gameObject); // ✅ GIỜ CHẮC CHẮN CHẠY
            }
            else
            {
                OnArrived();
            }
        }
    }

    protected virtual void OnArrived()
    {
        Debug.Log("[NPCVehicle] Arrived at stop point");
    }
}
