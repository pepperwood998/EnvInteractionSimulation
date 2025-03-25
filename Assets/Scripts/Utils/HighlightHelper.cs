using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightHelper : MonoBehaviour
{
    [SerializeField] private Outline highlightEffect;

    public void ShowHighlight()
    {
        highlightEffect.enabled = true;
    }

    public void HideHighlight()
    {
        highlightEffect.enabled = false;
    }
}
