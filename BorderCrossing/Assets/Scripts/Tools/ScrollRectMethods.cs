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
        StartCoroutine(ScrollAfterFrame());
    }

    private IEnumerator ScrollAfterFrame()
    {
        yield return new WaitForEndOfFrame();
        
        _scrollRect.verticalNormalizedPosition = 1f;
    }
}
