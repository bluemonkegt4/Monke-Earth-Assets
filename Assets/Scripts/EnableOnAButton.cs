using UnityEngine;
using UnityEngine.XR;

public class EnableOnAButton : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField] private GameObject targetObject;

    private InputDevice rightController;

    private void Start()
    {
        InitializeController();
    }

    private void InitializeController()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void Update()
    {
        if (!rightController.isValid)
        {
            InitializeController();
            return;
        }

        bool aPressed;
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out aPressed))
        {
            if (targetObject != null)
                targetObject.SetActive(aPressed);
        }
    }
}