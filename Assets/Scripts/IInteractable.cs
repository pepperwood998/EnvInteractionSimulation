using System;
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

    void ResetPicked();

    void GetDropped();

    void GetPlaced(IObjectReceivable receivable);

    void ResetPlaced();
}

public interface IObjectReceivable : IInteractable
{
    void ReceiveObject(IPickable pickable, Action<IPickable> onResetPlacement);

    void ReleaseObject(IPickable pickable);
}
