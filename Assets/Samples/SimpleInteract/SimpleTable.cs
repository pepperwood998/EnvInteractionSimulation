using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTable : MonoBehaviour, IObjectReceivable
{
    public GameObject Context => gameObject;

    public bool IsInteractable => _isInteractable;

    [SerializeField] private Transform surfacePoint;

    [Header("Debug")]
    [SerializeField] private bool _isInteractable;

    private Action<IPickable> _onResetPlacement;

    public void ReceiveObject(IPickable pickable, Action<IPickable> onResetPlacement)
    {
        var target = pickable.Context;

        target.transform.SetParent(surfacePoint);
        target.transform.localPosition = Vector3.zero;
        target.transform.localRotation = Quaternion.identity;

        _onResetPlacement = onResetPlacement;
    }

    public void ReleaseObject(IPickable pickable)
    {
        _onResetPlacement?.Invoke(pickable);
    }
}
