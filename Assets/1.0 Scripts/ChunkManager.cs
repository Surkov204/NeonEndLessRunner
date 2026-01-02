using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance;

    public Transform[] chunks;
    public float chunkSpacing; 
    public float TotalLength => chunkSpacing * chunks.Length;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (chunks == null || chunks.Length < 2)
        {
            Debug.LogError("Need at least 2 chunks");
            return;
        }

        System.Array.Sort(chunks, (a, b) => a.position.z.CompareTo(b.position.z));
        Transform backA = chunks[0].Find("BackPoint");
        Transform backB = chunks[1].Find("BackPoint");

        if (backA == null || backB == null)
        {
            Debug.LogError("BackPoint missing in chunks");
            return;
        }

        chunkSpacing = Mathf.Abs(backB.position.z - backA.position.z);

        Debug.Log($"[ChunkManager] Count={chunks.Length}");
        Debug.Log($"[ChunkManager] BackPoint spacing={chunkSpacing}");
        Debug.Log($"[ChunkManager] TotalLength={TotalLength}");
    }
}
