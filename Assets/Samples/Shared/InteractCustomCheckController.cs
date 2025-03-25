using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvInteraction.Samples.Shared
{
    public class InteractCustomCheckController : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<InteractionType, BaseInteractCustomChecker> checkerMap;

        public bool Check(InteractionType type, IInteractable attachment)
        {
            if (checkerMap.TryGetValue(type, out var checker))
            {
                return checker.Check(attachment);
            }

            return true;
        }
    }
}
