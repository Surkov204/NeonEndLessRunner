using UnityEngine;

public class ChunkRunner : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform recyclePoint;
    [SerializeField] private Transform BackPoint;
    public Transform backPoint => BackPoint;

    bool triggered = false;

    private void LateUpdate()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        if (!triggered && BackPoint.position.z < recyclePoint.position.z)
        {
            triggered = true;
            transform.position += Vector3.forward * ChunkManager.Instance.TotalLength;
            ChunkManager.Instance.OnChunkRecycled();
            triggered = false;
        }
    }
}
