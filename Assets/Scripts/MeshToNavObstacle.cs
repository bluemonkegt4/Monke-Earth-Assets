using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(NavMeshObstacle))]
public class MeshToNavObstacle : MonoBehaviour
{
    void Awake()
    {
        MeshCollider meshCol = GetComponent<MeshCollider>();
        NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();

        if (meshCol == null) return;

        // 🔥 FIXED LINE (use collider bounds, not mesh bounds)
        Bounds bounds = meshCol.bounds;

        obstacle.shape = NavMeshObstacleShape.Box;
        obstacle.size = bounds.size;
        obstacle.center = transform.InverseTransformPoint(bounds.center);
        obstacle.carving = true;
    }
}