using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHelper : MonoBehaviour
{
    [SerializeField] private Collider[] colliders;

    public void TurnOn()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
    }

    public void TurnOff()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
