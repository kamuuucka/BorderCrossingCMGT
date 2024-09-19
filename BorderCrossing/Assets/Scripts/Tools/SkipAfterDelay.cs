using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkipAfterDelay
{
    public static IEnumerator SwitchTextAfterDelay(string message, float delay, TMP_Text text)
    {
        yield return new WaitForSeconds(delay);
        text.text = message;
    }
}
