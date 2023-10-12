using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card")]
public class SignLanguageCard : ScriptableObject
{
    public Sprite image;
    public string answer;

    public bool IsTrue(string text)
    {
        text = text.Remove(text.Length - 1);
        return string.Equals(text, answer, StringComparison.OrdinalIgnoreCase);
    }
}
