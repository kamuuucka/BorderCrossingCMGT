using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptsData
{
    public List<string> prompts;

    public PromptsData()
    {
        this.prompts = new List<string>();
    }

    [Serializable]
    public class Prompts
    {
        public List<string> prompts;
    }
}
