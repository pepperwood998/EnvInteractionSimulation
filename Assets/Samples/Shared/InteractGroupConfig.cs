using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EnvInteraction.Samples.Shared
{
    [CreateAssetMenu(fileName = "Interact Group Config", menuName = "EnvInteraction/Samples/Interact Group Config")]
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

        public bool TryIterateInteractionTypes(IInteractable target, IInteractable attachment, Func<InteractionType, IInteractable, IInteractable, bool> customCheckDelegate, out InteractionType interactionType)
        {
            var targetType = GetInteractableTypes(target);
            var attachmentType = GetInteractableTypes(attachment);
            for (int i = 0; i < interactionPriority.Count; i++)
            {
                var config = interactionPriority[i];
                if ((targetType & config.target) != 0)
                {
                    if ((attachmentType == InteractableType.None && config.attachment == InteractableType.None)
                        || (attachmentType & config.attachment) != 0)
                    {
                        if (customCheckDelegate(config.interaction, target, attachment))
                        {
                            interactionType = config.interaction;
                            return true;
                        }
                    }
                }
            }

            interactionType = InteractionType.None;
            return false;
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
}