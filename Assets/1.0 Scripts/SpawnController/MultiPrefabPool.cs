using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MultiPrefabPool : MonoBehaviour
{
    public static MultiPrefabPool Instance;

    [System.Serializable]
    public class PoolEntry
    {
        public GameObject prefab;
        public int prewarm = 10;
        public Transform parent;
    }

    [SerializeField] private PoolEntry[] entries;

    [Inject] private DiContainer container;

    private readonly Dictionary<GameObject, Queue<GameObject>> pools = new();
    private readonly Dictionary<GameObject, Transform> parents = new();

    private void Awake()
    {
        Instance = this;

        if (entries == null) return;

        foreach (var e in entries)
        {
            if (e.prefab == null) continue;

            parents[e.prefab] = e.parent; // nhớ parent theo prefab

            var q = GetOrCreate(e.prefab);
            for (int i = 0; i < e.prewarm; i++)
            {
                var go = CreateNew(e.prefab);
                go.SetActive(false);
                q.Enqueue(go);
            }
        }
    }

    private Queue<GameObject> GetOrCreate(GameObject prefab)
    {
        if (!pools.TryGetValue(prefab, out var q))
        {
            q = new Queue<GameObject>();
            pools[prefab] = q;
        }
        return q;
    }

    private GameObject CreateNew(GameObject prefab)
    {
        parents.TryGetValue(prefab, out var p);
        var go = container.InstantiatePrefab(prefab, p); 

        var tag = go.GetComponent<PooledTag>();
        if (tag == null) tag = go.AddComponent<PooledTag>();
        tag.prefab = prefab;

        return go;
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        var q = GetOrCreate(prefab);
        var go = q.Count > 0 ? q.Dequeue() : CreateNew(prefab);

        go.transform.SetPositionAndRotation(pos, rot);
        go.SetActive(true);
        return go;
    }

    public void Despawn(GameObject go)
    {
        if (go == null) return;

        var tag = go.GetComponent<PooledTag>();
        if (tag == null || tag.prefab == null)
        {
            Destroy(go);
            return;
        }

        go.SetActive(false);
        GetOrCreate(tag.prefab).Enqueue(go);
    }
}

public class PooledTag : MonoBehaviour
{
    public GameObject prefab;
}
