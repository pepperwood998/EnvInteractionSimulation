using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    GameObject Context { get; }

    bool IsInteractable { get; }
}

public interface IPickable : IInteractable
{
    void GetPicked();

    void GetDropped();
}

public interface IHighlightable
{
    void ShowHighlight();

    void HideHighlight();
}
