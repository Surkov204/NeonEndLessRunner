using UnityEngine;

public class ChunkDistanceActivator : MonoBehaviour
{
    public Transform cameraTransform;
    public float activeDistance = 90f;
    public Transform decorRoot;

    void Update()
    {
        float dist = Mathf.Abs(
            transform.position.z - cameraTransform.position.z
        );

        bool active = dist < activeDistance;

        if (decorRoot.gameObject.activeSelf != active)
            decorRoot.gameObject.SetActive(active);
    }
}
