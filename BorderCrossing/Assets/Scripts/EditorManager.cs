using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EditorManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToDisable;

    private void DisableScenes()
    {
        if (objectsToDisable.Length > 0)
        {
            foreach (var objectToDisable in objectsToDisable)
            {
                if (objectToDisable.activeSelf)
                {
                    objectToDisable.SetActive(false);
                }
            }
        }
    }

    private void OnValidate()
    {
        DisableScenes();
    }
}
