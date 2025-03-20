using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObject : MonoBehaviour, IPickable
{
    public GameObject Context => gameObject;

    public bool IsInteractable => _isInteractable;

    // [SerializeField] private Rigidbody rb;
    // [SerializeField] private ColliderHelper colliders;
    [SerializeField] private PickableHelper pickableHelper;

    // private IObjectReceivable _activeReceiver;

    [Header("Debug")]
    [SerializeField] private bool _isInteractable;

    #region Pickable

    public void GetPicked()
    {
        // if (_activeReceiver != null)
        // {
        //     _activeReceiver.ReleaseObject(this);
        // }

        // rb.useGravity = false;
        // colliders.TurnOff();
        pickableHelper.MarkPicked(this);

        _isInteractable = false;
    }

    public void ResetPicked()
    {
        // rb.useGravity = true;
        // colliders.TurnOn();
        pickableHelper.ResetPicked();

        _isInteractable = true;
    }

    public void GetDropped() { }

    public void GetPlaced(IObjectReceivable receivable)
    {
        // rb.useGravity = false;
        // rb.isKinematic = true;

        // _activeReceiver = receivable;
        pickableHelper.MarkPlaced(receivable);
    }

    public void ResetPlaced()
    {
        // rb.useGravity = true;
        // rb.isKinematic = false;

        // _activeReceiver = null;
        pickableHelper.ResetPlaced();
    }

    #endregion
}
