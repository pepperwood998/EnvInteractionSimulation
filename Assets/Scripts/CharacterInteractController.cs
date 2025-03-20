using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInteractController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<InteractionType, UnityEvent<IInteractable>> interactionDelegateMap;
    [Space]
    [SerializeField] private InteractGroupConfig interactGroupConfig;
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
                var interactionType = interactGroupConfig.GetInteractionType(_holding, interactable);
                if (interactionType != InteractionType.None && CheckInteractionCustom(interactionType, interactable, _holding))
                {
                    current.ShowHighlight();
                }
                else
                {
                    current.HideHighlight();
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
                var interactionType = interactGroupConfig.GetInteractionType(_holding, _focusing);
                if (interactionDelegateMap.TryGetValue(interactionType, out var action))
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

    public void TryPlace(IInteractable interactable)
    {
        if (_holding != null)
        {
            var receivable = interactable as IObjectReceivable;
            _holding.ResetPicked();
            _holding.GetPlaced(receivable);
            receivable.ReceiveObject(_holding, ResetPlacement);

            _holding = null;
            _forceCheckHighlight = true;
        }
    }

    private void ResetPlacement(IPickable pickable)
    {
        pickable.ResetPlaced();
    }

    private bool CheckInteractionCustom(InteractionType type, IInteractable target, IInteractable attachment)
    {
        var customCheckController = target.Context.GetComponent<InteractCustomCheckController>();
        if (customCheckController)
        {
            return customCheckController.Check(type, target, attachment);
        }

        return true;
    }
}
