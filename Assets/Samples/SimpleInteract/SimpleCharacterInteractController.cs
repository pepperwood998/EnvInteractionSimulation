using System;
using System.Collections;
using System.Collections.Generic;
using EnvInteraction.Samples.Shared;
using UnityEngine;
using UnityEngine.Events;

public class SimpleCharacterInteractController : BasicCharacterInteractController
{
    [Header("Others")]
    [SerializeField] private SerializedDictionary<InteractionType, UnityEvent<IInteractable>> interactionDelegateMap;
    [Space]
    [SerializeField] private InteractGroupConfig interactGroupConfig;

    protected override bool CheckInteractable(
        IInteractable interactable, IPickable attachment, out UnityEvent<IInteractable> actionDelegate)
    {
        var interactionType = interactGroupConfig.GetInteractionType(attachment, interactable);
        if (interactionDelegateMap.TryGetValue(interactionType, out var action))
        {
            actionDelegate = action;
            return CheckInteractCustom(interactionType, interactable, attachment);
        }

        actionDelegate = null;
        return false;
    }

    private bool CheckInteractCustom(InteractionType type, IInteractable target, IInteractable attachment)
    {
        var customCheckController = target.Context.GetComponent<InteractCustomCheckController>();
        if (customCheckController)
        {
            return customCheckController.Check(type, attachment);
        }

        return true;
    }
}
