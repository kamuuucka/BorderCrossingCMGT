using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot_ui_test : MonoBehaviour
{
    public Image image;
    public Button button;
    
    public void ChangeColor()
    {
        ColorBlock buttonColor = button.colors;
        buttonColor.disabledColor = Color.white;
        button.colors = buttonColor;
        image.color = Color.white;
        
    }

    private void Update()
    {
        if (image.color == Color.white)
        {
            button.interactable = false;
        }
    }
}
