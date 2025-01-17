using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObjectsBasedOnValue : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;

    public void ChangeTheObject(Slider slider)
    {
        foreach (var objectToSwitchOff in objects)
        {
            objectToSwitchOff.SetActive(false);
        }
        objects[(int)slider.value].SetActive(true);
    }
}
