using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameData
{
    public List<DataElement> Elements;

    public GameData()
    {
        Elements = new List<DataElement>();
    }

    public void AddNewPrompts(string name, List<string> newValue)
    {
        var newElement = new DataElement
        {
            name = name,
            content = newValue,
            active = false,
            basePrompt = false
        };
        Elements.Add(newElement);
    }

    [Serializable]
    public class DataElement
    {
        public string name;
        public List<string> content;
        public bool active;
        public bool basePrompt;
        public Image image;

        public void SetAsActive()
        {
            active = true;
        }
    }
}
