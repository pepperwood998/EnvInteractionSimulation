using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, IPickable, IHighlightable
{
    public GameObject Context => gameObject;

    public bool IsInteractable => _isInteractable;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private ColliderHelper colliders;
    [SerializeField] private Outline highlightEffect;

    [Header("Debug")]
    [SerializeField] private bool _isInteractable;

    #region Pickable

    public void GetPicked()
    {
        rb.useGravity = false;
        colliders.TurnOff();

        _isInteractable = false;
    }

    public void GetDropped()
    {
        rb.useGravity = true;
        colliders.TurnOn();

        _isInteractable = true;
    }

    #endregion

    #region Highlight

    public void ShowHighlight()
    {
        highlightEffect.enabled = true;
    }

    public void HideHighlight()
    {
        highlightEffect.enabled = false;
    }

    #endregion
}
