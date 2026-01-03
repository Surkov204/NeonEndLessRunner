using UnityEngine;

public class ChunkRunner : MonoBehaviour
{
    public float speed = 10f;
    public Transform recyclePoint;
    public Transform backPoint;

    bool triggered = false;

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        if (!triggered && backPoint.position.z < recyclePoint.position.z)
        {
            triggered = true;

            // recycle vật lý
            transform.position += Vector3.forward * ChunkManager.Instance.TotalLength;
            ChunkManager.Instance.OnChunkRecycled();

            // 🔴 RESET ĐỂ CHUNK CÓ THỂ RECYCLE LẠI LẦN SAU
            triggered = false;
        }
    }
}
