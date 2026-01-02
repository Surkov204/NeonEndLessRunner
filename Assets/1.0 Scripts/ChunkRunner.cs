using UnityEngine;

public class ChunkRunner : MonoBehaviour
{
    public float speed = 10f;
    public Transform recyclePoint;
    public Transform backPoint;

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        if (backPoint.position.z < recyclePoint.position.z)
        {
            transform.position += Vector3.forward * ChunkManager.Instance.TotalLength;
        }
    }
}
