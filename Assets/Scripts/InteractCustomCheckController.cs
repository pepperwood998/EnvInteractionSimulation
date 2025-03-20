using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCustomCheckController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<InteractionType, BaseInteractCustomChecker> checkerMap;

    public bool Check(InteractionType type, IInteractable target, IInteractable attachment)
    {
        if (checkerMap.TryGetValue(type, out var checker))
        {
            return checker.Check(target, attachment);
        }

        return false;
    }
}
