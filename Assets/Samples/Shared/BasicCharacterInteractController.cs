using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasicCharacterInteractController : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected Transform hand;

    private IInteractable _focusing;
    private IPickable _holding;
    private bool _forceCheckHighlight;

    public void Focus(IInteractable interactable)
    {
        if (interactable != _focusing)
        {
            if (_focusing != null)
            {
                if (_focusing.Context.TryGetComponent<HighlightHelper>(out var highlight))
                {
                    highlight.HideHighlight();
                }
            }
        }

        if (interactable != _focusing || _forceCheckHighlight)
        {
            _forceCheckHighlight = false;

            if (interactable.Context.TryGetComponent<HighlightHelper>(out var current))
            {
                if (CheckInteractable(interactable, _holding, out _))
                {
                    current.ShowHighlight();
                }
            }
        }

        _focusing = interactable;

        Debug.Log($"Focusing on <color=yellow>{interactable.Context.name}</color>");
    }

    public void ClearFocus()
    {
        if (_focusing != null)
        {
            if (_focusing.Context.TryGetComponent<HighlightHelper>(out var highlight))
            {
                highlight.HideHighlight();
            }

            _focusing = null;
        }
    }

    public void TryInteract()
    {
        if (_focusing != null)
        {
            if (_focusing.IsInteractable)
            {
                // TODO: Test and check if this is really flexible
                if (CheckInteractable(_focusing, _holding, out var action))
                {
                    action?.Invoke(_focusing);
                }

                Debug.Log($"Interact with <color=green>{_focusing.Context.name}</color>");
            }
        }
    }

    public void PickUp(IInteractable interactable)
    {
        var pickable = interactable as IPickable;
        pickable.GetPicked();

        var go = pickable.Context;
        go.transform.SetParent(hand);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;

        _holding = pickable;
        _forceCheckHighlight = true;
    }

    public void TryDrop()
    {
        if (_holding != null)
        {
            if (_holding is IPickable pickable)
            {
                pickable.ResetPicked();
                pickable.GetDropped();
            }

            var go = _holding.Context;
            go.transform.SetParent(null);

            _holding = null;
            _forceCheckHighlight = true;
        }
    }

    public void TryPlace(IInteractable target)
    {
        if (_holding != null)
        {
            var receivable = target as IObjectReceivable;
            _holding.ResetPicked();
            _holding.GetPlaced(receivable);
            receivable.ReceiveObject(_holding, ResetPlacement);

            _holding = null;
            _forceCheckHighlight = true;
        }
    }

    protected void ResetPlacement(IPickable pickable)
    {
        pickable.ResetPlaced();
    }

    protected abstract bool CheckInteractable
        (IInteractable interactable, IPickable attachment, out UnityEvent<IInteractable> actionDelegate);
}
