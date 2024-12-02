using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataRecord : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Image image;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button loadButton;

    public void ChangeText(string value)
    {
        textField.text = value;
    }

    public Image GetColor()
    {
        return image;
    }

    public Button GetDeleteButton()
    {
        return deleteButton;
    }

    public Button GetLoadButton()
    {
        return loadButton;
    }
}
