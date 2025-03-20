using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCrate : MonoBehaviour, IPickable, IObjectReceivable
{
    public GameObject Context => gameObject;

    public bool IsInteractable => _isInteractable;

    [SerializeField] private PickableHelper pickableHelper;
    [SerializeField] private Transform surfacePoint;

    private Action<IPickable> _onResetPlacement;

    [Header("Debug")]
    [SerializeField] private bool _isInteractable;

    #region Pickable

    public void GetPicked()
    {
        pickableHelper.MarkPicked(this);

        _isInteractable = false;
    }

    public void ResetPicked()
    {
        pickableHelper.ResetPicked();

        _isInteractable = true;
    }

    public void GetDropped() { }

    public void GetPlaced(IObjectReceivable receivable)
    {
        pickableHelper.MarkPlaced(receivable);
    }

    public void ResetPlaced()
    {
        pickableHelper.ResetPlaced();
    }

    #endregion

    #region Receivable

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

    #endregion
}
