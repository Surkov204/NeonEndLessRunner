using UnityEngine;

public class WorldRunner : MonoBehaviour
{
    public float speed = 10f;
    public Transform recyclePoint;

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        //ChunkManager.Instance.RecycleIfNeeded(recyclePoint);
    }
}
