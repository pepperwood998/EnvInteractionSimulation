using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInputHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> onMove;
    [SerializeField] private UnityEvent<Vector2> onLook;

    private void Update()
    {
        onMove?.Invoke(InputManager.Instance.MoveInput);
        onLook?.Invoke(InputManager.Instance.LookInput);
    }
}
