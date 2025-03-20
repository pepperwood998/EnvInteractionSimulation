using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Interact Group Config", menuName = "Configs/Interact Group Config")]
public class InteractGroupConfig : SerializedScriptableObject
{
    [SerializeField] private List<InteractConfig> interactionPriority;

    public InteractionType GetInteractionType(IInteractable attachment, IInteractable target)
    {
        var attachmentType = GetInteractableTypes(attachment);
        var targetType = GetInteractableTypes(target);
        foreach (var config in interactionPriority)
        {
            if (attachmentType == InteractableType.None && config.attachment == InteractableType.None)
            {
                if ((targetType & config.target) != 0)
                {
                    return config.interaction;
                }
            }
            else
            {
                if ((attachmentType & config.attachment) != 0
                    && (targetType & config.target) != 0)
                {
                    return config.interaction;
                }
            }

        }

        return InteractionType.None;
    }

    private InteractableType GetInteractableTypes(IInteractable interactable)
    {
        InteractableType type = InteractableType.None;
        if (interactable is IPickable)
        {
            type |= InteractableType.Pickable;
        }
        if (interactable is IObjectReceivable)
        {
            type |= InteractableType.ObjectReceivable;
        }

        return type;
    }
}

[Serializable]
public class InteractConfig
{
    public InteractableType attachment;
    public InteractableType target;
    public InteractionType interaction;
}

[Flags]
public enum InteractableType
{
    None = 0,
    Pickable = 1 << 0,
    ObjectReceivable = 1 << 1,
}

public enum InteractionType
{
    None = 0,
    PickUp,
    Place,
}
