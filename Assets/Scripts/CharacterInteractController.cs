using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    [SerializeField] private Transform hand;

    private IInteractable _focusing;
    private IPickable _holding;

    public void Focus(IInteractable interactable)
    {
        if (_focusing != null)
        {
            if (_focusing != interactable && _focusing is IHighlightable highlightable)
            {
                highlightable.HideHighlight();
            }
        }

        if (interactable != _focusing)
        {
            if (interactable is IHighlightable highlightable)
            {
                highlightable.ShowHighlight();
            }
        }
        _focusing = interactable;

        Debug.Log($"Focusing on <color=yellow>{interactable.Context.name}</color>");
    }

    public void ClearFocus()
    {
        if (_focusing != null)
        {
            if (_focusing is IHighlightable highlightable)
            {
                highlightable.HideHighlight();
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
                Debug.Log($"Interact with <color=green>{_focusing.Context.name}</color>");

                // TODO: Need to make the system more flexible
                if (_focusing is IPickable pickable)
                {
                    PickUp(pickable);
                }
            }
        }
    }

    private void PickUp(IPickable pickable)
    {
        pickable.GetPicked();

        var go = pickable.Context;
        go.transform.SetParent(hand);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;

        _holding = pickable;
    }

    public void TryDrop()
    {
        if (_holding != null)
        {
            if (_holding is IPickable pickable)
            {
                pickable.GetDropped();
            }

            var go = _holding.Context;
            go.transform.SetParent(null);

            _holding = null;
        }
    }
}
