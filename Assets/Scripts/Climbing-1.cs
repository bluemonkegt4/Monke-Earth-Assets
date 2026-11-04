using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using System;
using GorillaLocomotion;
using System.Collections.Generic;

public class Climbing : MonoBehaviour
{
    [Header("Climb Settings")]
    public XRNode Hand;
    public float HandDist;

    [Tooltip("Layers on the objects/surfaces you want to climb")]
    public LayerMask ClimbLayer;

    public Transform Player;

    [Tooltip("Set this bool to true if it is a rope/vine make sure to create a new script on the hand if you want climbing as well as ropes/vines set different layers for both")]
    public bool IsRope;

    [Header("Debug stuff")]
    public bool DebugGrab;

    private InputDevice handDevice;
    private bool isGripping;
    private Transform grippedObject = null;
    private Vector3 lastHandPosition;
    private Transform LastGripPoint;
    private Rigidbody PlayerRb;
    private Vector3 HandVelocity;
    private Quaternion HandRotation;
    private Climbing OtherHand;

    private void Start()
    {
        PlayerRb = Player.GetComponent<Rigidbody>();

        Climbing[] CS = GameObject.FindObjectsOfType<Climbing>();
        foreach(Climbing climbing in CS)
        {
            if (climbing.IsRope == IsRope && climbing != this)
            {
                OtherHand = climbing;
            }
        }
    }

    private void Update()
    {
        handDevice = InputDevices.GetDeviceAtXRNode(Hand);
        if (handDevice.isValid || DebugGrab)
        {
            bool gripHeld;
            if (!DebugGrab)
            {
                handDevice.TryGetFeatureValue(CommonUsages.gripButton, out gripHeld);
            }
            else
            {
                gripHeld = DebugGrab;
            }

            if (gripHeld && !isGripping)
            {
                TryGripObject();
            }
            else if (!gripHeld && isGripping)
            {
                ReleaseGrip();
            }

            if (isGripping)
            {
                PlayerMovement();
            }
        }
    }

    private void TryGripObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, HandDist, ClimbLayer);
        if (hitColliders.Length > 0)
        {
            grippedObject = hitColliders[0].transform;
            isGripping = true;
            lastHandPosition = transform.position;

            GameObject GrabPoint = new GameObject("GrabbedPoint");
            GrabPoint.transform.transform.position = transform.position;
            GrabPoint.transform.parent = grippedObject;
            LastGripPoint = GrabPoint.transform;

            if (IsRope)
            {
                grippedObject.GetComponent<Rigidbody>().velocity += Vector3.ClampMagnitude(PlayerRb.velocity, 6) +
                               (Quaternion.Euler(0, Player.GetComponent<Player>().headCollider.transform.eulerAngles.y, 0) *
                                Vector3.forward * 4);
            }

            HandRotation = transform.rotation;

            if (OtherHand.isGripping)
            {
                OtherHand.ReleaseGrip();
                OtherHand.isGripping = false;
            }
        }
    }

    public void ReleaseGrip()
    {
        grippedObject = null;
        isGripping = false;

        PlayerRb.velocity = HandVelocity * 0.7f;

        Destroy(LastGripPoint.gameObject);
    }

    private void PlayerMovement()
    {
        if (grippedObject != null)
        {
            Vector3 handMovement = LastGripPoint.position - transform.position;

            Player.position += handMovement;

            HandVelocity = handMovement / Time.deltaTime;

            lastHandPosition = transform.position;
            transform.rotation = HandRotation;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, HandDist);
    }
}