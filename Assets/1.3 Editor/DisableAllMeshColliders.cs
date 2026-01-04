using UnityEngine;
using UnityEditor;

public class DisableAllMeshCollidersEditor
{
    [MenuItem("Tools/Optimization/Disable ALL MeshColliders")]
    static void DisableAllMeshColliders()
    {
        MeshCollider[] colliders = Object.FindObjectsOfType<MeshCollider>(true);

        int count = 0;
        foreach (var col in colliders)
        {
            if (col.enabled)
            {
                Undo.RecordObject(col, "Disable MeshCollider");
                col.enabled = false;
                EditorUtility.SetDirty(col);
                count++;
            }
        }

        Debug.Log($"[Editor] Disabled {count} MeshColliders in scene.");
    }
}
