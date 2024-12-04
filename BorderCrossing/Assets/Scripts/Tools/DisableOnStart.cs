using UnityEngine;

/// <summary>
/// Disables the object or specific methods on start.
/// </summary>
public class DisableOnStart : MonoBehaviour
{
    [SerializeField] private bool wholeObject = true;
    [SerializeField] private MonoBehaviour[] scriptsToDisable;
    private void Awake()
    {
        if (wholeObject)
        {
            gameObject.SetActive(false);
        }

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
