using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance;

    public Chunk[] chunks;
    public int startIndex = 1;

    public float chunkLength;
    public float TotalLength;

    int currentIndex;
    int prevA = -1, prevB = -1;

    void Awake() => Instance = this;

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

        chunkLength = Mathf.Abs(r1.backPoint.position.z - r0.backPoint.position.z);
        TotalLength = chunkLength * chunks.Length;

        // init: tat het 1 lan thoi
        for (int i = 0; i < chunks.Length; i++)
            chunks[i].envRoot.SetActive(false);

        currentIndex = startIndex;
        ApplyActivePair(currentIndex);
    }

    public void OnChunkRecycled()
    {
        currentIndex = (currentIndex + 1) % chunks.Length;
        ApplyActivePair(currentIndex);
    }

    void ApplyActivePair(int idx)
    {
        int a = idx;
        int b = (idx + 1) % chunks.Length;

        // tat 2 cai cu
        if (prevA != -1) chunks[prevA].envRoot.SetActive(false);
        if (prevB != -1 && prevB != prevA) chunks[prevB].envRoot.SetActive(false);

        // bat 2 cai moi
        chunks[a].envRoot.SetActive(true);
        chunks[b].envRoot.SetActive(true);

        prevA = a;
        prevB = b;
    }
}
