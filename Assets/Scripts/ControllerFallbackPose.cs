using UnityEngine;
using UnityEngine.XR;

public class ControllerFallbackPose : MonoBehaviour
{
    public enum ControllerHand
    {
        Left,
        Right
    }

    [Header("Controller")]
    public ControllerHand hand;

    [Header("Fallback Pose")]
    public Transform fallbackTransform; // empty object with desired pose

    [Header("Settings")]
    public float followSpeed = 10f;

    private InputDevice device;
    private bool isTracked;

    private void Start()
    {
        InitializeDevice();
    }

    private void InitializeDevice()
    {
        device = InputDevices.GetDeviceAtXRNode(
            hand == ControllerHand.Left ? XRNode.LeftHand : XRNode.RightHand
        );
    }

    private void Update()
    {
        if (!device.isValid)
        {
            InitializeDevice();
            return;
        }

        device.TryGetFeatureValue(CommonUsages.isTracked, out isTracked);

        if (!isTracked)
        {
            ApplyFallbackPose();
        }
    }

    private void ApplyFallbackPose()
    {
        if (fallbackTransform == null)
            return;

        transform.position = Vector3.Lerp(
            transform.position,
            fallbackTransform.position,
            Time.deltaTime * followSpeed
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            fallbackTransform.rotation,
            Time.deltaTime * followSpeed
        );
    }
}