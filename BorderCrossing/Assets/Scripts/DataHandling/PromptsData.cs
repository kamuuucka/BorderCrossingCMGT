using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptsData
{
    public List<Prompts> promptList;

    public PromptsData()
    {
        promptList = new List<Prompts>();
    }

    public void AddNewPrompts(string name, List<string> newValue)
    {
        var newPrompts = new Prompts
        {
            name = name,
            prompts = newValue,
            active = false,
            basePrompt = false
        };
        promptList.Add(newPrompts);
    }

    [Serializable]
    public class Prompts
    {
        public string name;
        public List<string> prompts;
        public bool active;
        public bool basePrompt;

        public void SetAsActive()
        {
            active = true;
        }
    }
}
