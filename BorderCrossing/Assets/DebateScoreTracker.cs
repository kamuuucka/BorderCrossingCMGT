using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebateScoreTracker : MonoBehaviour
{
    [SerializeField] int yesCount;
    [SerializeField] int noCount;
    [SerializeField] private UnityEvent<string> onCountUpdate;

    public void Reset()
    {
        yesCount = 0;
        noCount = 0;
        onCountUpdate?.Invoke(0 + " VS " + 0);
    }

    private void Awake()
    {
        Reset();
    }

    public void AddPlayerToSide(bool side)
    {
        if (side) { yesCount++; }
        else { noCount++; }

        string countText = yesCount + " VS " + noCount;
        onCountUpdate?.Invoke(countText);
    }
}
