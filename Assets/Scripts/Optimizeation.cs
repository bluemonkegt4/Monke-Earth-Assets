
using UnityEngine;
using System.Collections.Generic;

public class LookOptimization : MonoBehaviour
{
    [Header("Player Camera")]
    public Camera playerCamera;

    [Header("Objects To Ignore")]
    public GameObject[] objectsToIgnore;

    [Header("Settings")]
    [Min(0.05f)]
    public float checkInterval = 0.2f;

    public float showDistance = 50f;

    private float timer;
    private Renderer[] allRenderers;
    private Plane[] cameraPlanes;

    private void Start()
    {
        allRenderers = FindObjectsOfType<Renderer>(true);
    }

    private void Update()
    {
        if (playerCamera == null)
            return;

        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            timer = 0f;
            CheckObjects();
        }
    }

    private void CheckObjects()
    {
        cameraPlanes = GeometryUtility.CalculateFrustumPlanes(
            playerCamera);

        foreach (Renderer rend in allRenderers)
        {
            if (rend == null)
                continue;

            if (IsIgnored(rend.transform))
            {
                rend.enabled = true;
                continue;
            }

            if (!rend.gameObject.activeInHierarchy)
                continue;

            float distance = Vector3.Distance(
                playerCamera.transform.position,
                rend.bounds.center);

            bool inView = GeometryUtility.TestPlanesAABB(
                cameraPlanes, rend.bounds);

            bool shouldShow = inView &&
                              distance <= showDistance;

            rend.enabled = shouldShow;
        }
    }

    private bool IsIgnored(Transform obj)
    {
        foreach (GameObject ignored in objectsToIgnore)
        {
            if (ignored == null)
                continue;

            if (obj == ignored.transform ||
                obj.IsChildOf(ignored.transform))
                return true;
        }

        return false;
    }
}
