using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectMethods : MonoBehaviour
{
    private ScrollRect _scrollRect;
    private void OnEnable()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    public void ScrollToTop()
    {
        _scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}
