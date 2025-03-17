using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform fpsCameraContainer;
    [SerializeField, Range(0f, 1f)] private float lookDeltaScaler;
    [SerializeField, Range(0f, 90f)] private float maxVerticalLookRange;

    public void Move(Vector2 ratios)
    {
        float frameSpeed = moveSpeed * Time.deltaTime;
        var amount = new Vector3(ratios.x * frameSpeed, 0f, ratios.y * frameSpeed);
        controller.Move(amount);
    }

    public void Look(Vector2 lookDelta)
    {
        var horizontalRotation = Vector3.up * lookDelta.x * lookDeltaScaler;
        controller.transform.Rotate(horizontalRotation);

        var verticalRotation = Vector3.right * -lookDelta.y * lookDeltaScaler;
        fpsCameraContainer.Rotate(verticalRotation);

        // Limit vertical rotation
        var checkAngles = fpsCameraContainer.localEulerAngles;
        float xAngle = checkAngles.x > 180f ? checkAngles.x - 360f : checkAngles.x;
        xAngle = Mathf.Clamp(xAngle, -maxVerticalLookRange, maxVerticalLookRange);
        fpsCameraContainer.localRotation = Quaternion.Euler(xAngle, checkAngles.y, checkAngles.z);
    }
}
