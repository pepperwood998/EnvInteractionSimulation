using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private Transform fpsCameraContainer;
    [SerializeField, Range(0f, 1f)] private float lookDeltaScaler;
    [SerializeField, Range(0f, 90f)] private float maxVerticalLookRange;

    private float _lookPitch = 0f;
    private float _velocityY = 0f;

    public void Move(Vector2 ratios)
    {
        _velocityY += gravity * Time.deltaTime;
        if (controller.isGrounded)
        {
            _velocityY = 0f;
        }

        var velocity = (controller.transform.forward * ratios.y + controller.transform.right * ratios.x) * moveSpeed + Vector3.down * _velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Look(Vector2 lookDelta)
    {
        // Yaw
        var horizontalRotation = Vector3.up * lookDelta.x * lookDeltaScaler;
        controller.transform.Rotate(horizontalRotation);

        // Pitch
        _lookPitch -= lookDelta.y * lookDeltaScaler;
        _lookPitch = Mathf.Clamp(_lookPitch, -maxVerticalLookRange, maxVerticalLookRange);
        fpsCameraContainer.localEulerAngles = Vector3.right * _lookPitch;
    }
}
