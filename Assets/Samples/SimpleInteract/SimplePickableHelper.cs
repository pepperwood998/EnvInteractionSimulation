using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePickableHelper : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ColliderHelper colliders;

    private IObjectReceivable _activeReceiver;

    public void MarkPicked(IPickable pickable)
    {
        if (_activeReceiver != null)
        {
            _activeReceiver.ReleaseObject(pickable);
        }

        rb.useGravity = false;
        colliders.TurnOff();
    }

    public void ResetPicked()
    {
        rb.useGravity = true;
        colliders.TurnOn();
    }

    public void MarkPlaced(IObjectReceivable receivable)
    {
        rb.useGravity = false;
        rb.isKinematic = true;

        _activeReceiver = receivable;
    }

    public void ResetPlaced()
    {
        rb.useGravity = true;
        rb.isKinematic = false;

        _activeReceiver = null;
    }
}
