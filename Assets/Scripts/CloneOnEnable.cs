using UnityEngine;

public class CloneOnEnable : MonoBehaviour
{
    [Header("Clone Settings")]
    public GameObject objectToClone;
    public int cloneCount = 3;

    [Header("Spawn Settings")]
    public Vector3 spawnOffset = Vector3.zero;
    public bool randomOffset = false;
    public float randomRadius = 1f;

    private void OnEnable()
    {
        if (objectToClone == null)
        {
            Debug.LogWarning("No object assigned to clone.", this);
            return;
        }

        for (int i = 0; i < cloneCount; i++)
        {
            Vector3 offset = spawnOffset;

            if (randomOffset)
            {
                offset += Random.insideUnitSphere * randomRadius;
            }

            Instantiate(
                objectToClone,
                transform.position + offset,
                objectToClone.transform.rotation
            );
        }

        // Disable this object so it can be re-used
        gameObject.SetActive(false);
    }
}
