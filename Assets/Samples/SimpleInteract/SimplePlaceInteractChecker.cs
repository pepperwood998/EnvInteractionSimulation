using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlaceInteractChecker : BaseInteractCustomChecker
{
    [SerializeField] private List<SimplePlacableName> placables;
    public override bool Check(IInteractable attachment)
    {
        if (attachment.Context.TryGetComponent<SimplePlacableHelper>(out var placable))
        {
            return placables.Contains(placable.PlacableName);
        }

        return false;
    }
}

public enum SimplePlacableName
{
    Fruit = 0,
    Food,
    Container,
}
