using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasicCharacterInteractController : MonoBehaviour
{
    [SerializeField] private Transform hand;

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
                if (CheckInteractCustomConditions(interactable, _holding, out _))
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
                if (CheckInteractCustomConditions(_focusing, _holding, out var action))
                {
                    action?.Invoke(_focusing);
                }

                Debug.Log($"Interact with <color=green>{_focusing.Context.name}</color>");
            }
        }
    }

    protected abstract bool CheckInteractCustomConditions
        (IInteractable interactable, IPickable attachment, out UnityEvent<IInteractable> actionDelegate);
}
