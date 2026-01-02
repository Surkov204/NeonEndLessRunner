using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance;

    public Chunk[] chunks;
    public int startIndex = 1;

    public float chunkLength;
    public float TotalLength;

    int currentIndex;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        System.Array.Sort(chunks, (a, b) =>
            a.transform.position.z.CompareTo(b.transform.position.z));

        var r0 = chunks[0].GetComponent<ChunkRunner>();
        var r1 = chunks[1].GetComponent<ChunkRunner>();

        if (r0 == null || r1 == null)
        {
            Debug.LogError("ChunkRunner missing");
            return;
        }

        chunkLength = Mathf.Abs(
            r1.backPoint.position.z -
            r0.backPoint.position.z
        );

        TotalLength = chunkLength * chunks.Length;

        currentIndex = startIndex;
        UpdateEnvironment();
    }

    // 🔴 CHỈ TĂNG THEO VÒNG
    public void OnChunkRecycled()
    {
        currentIndex = (currentIndex + 1) % chunks.Length;
        UpdateEnvironment();
    }

    void UpdateEnvironment()
    {
        foreach (var c in chunks)
            c.envRoot.SetActive(false);

        int next = (currentIndex + 1) % chunks.Length;

        chunks[currentIndex].envRoot.SetActive(true);
        chunks[next].envRoot.SetActive(true);
    }
}
