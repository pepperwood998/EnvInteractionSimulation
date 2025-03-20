using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractCustomChecker : MonoBehaviour
{
    public abstract bool Check(IInteractable target, IInteractable attachment);
}
