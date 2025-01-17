using System;
using UnityEngine;

/// <summary>
/// Disables specific methods on start.
/// </summary>
public class DisableOnStart : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] scriptsToDisable;
    private void Awake()
    {
        foreach (var script in scriptsToDisable)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }

        enabled = false;
    }
}
