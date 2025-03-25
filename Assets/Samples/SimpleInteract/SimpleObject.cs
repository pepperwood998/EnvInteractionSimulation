using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObject : MonoBehaviour, IPickable
{
    public GameObject Context => gameObject;

    public bool IsInteractable => _isInteractable;

    [SerializeField] private SimplePickableHelper pickableHelper;

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
}
