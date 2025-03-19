using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInputHandler : MonoBehaviour
{
    [SerializeField] private Transform lookPoint;
    [SerializeField] private float interactableMaxDistance;
    [SerializeField] private LayerMask interactableLayerMask;

    [Header("Events")]
    [SerializeField] private UnityEvent<Vector2> onMove;
    [SerializeField] private UnityEvent<Vector2> onLook;
    [SerializeField] private UnityEvent<IInteractable> onFocus;
    [SerializeField] private UnityEvent onClearFocus;
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private UnityEvent onDrop;

    private void Update()
    {
        TryFocus();

        if (InputManager.Instance.HasInteractInput)
        {
            onInteract?.Invoke();
        }
        if (InputManager.Instance.HasDropInput)
        {
            onDrop?.Invoke();
        }
        onMove?.Invoke(InputManager.Instance.MoveInput);
        onLook?.Invoke(InputManager.Instance.LookInput);
    }

    private void TryFocus()
    {
        if (Physics.Raycast(
            lookPoint.position, lookPoint.forward, out var hit,
            interactableMaxDistance, interactableLayerMask))
        {
            var target = hit.collider.transform;
            if (target.parent && target.parent.TryGetComponent<IInteractable>(out var interactable))
            {
                onFocus?.Invoke(interactable);
                return;
            }
        }

        onClearFocus?.Invoke();
    }
}
