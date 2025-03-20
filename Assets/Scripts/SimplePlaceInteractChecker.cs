using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlaceInteractChecker : BaseInteractCustomChecker
{
    public override bool Check(IInteractable target, IInteractable attachment)
    {
        return true;
    }
}

public enum SimplePlacableName
{
}
