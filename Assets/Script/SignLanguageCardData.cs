using System;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New Card")]
public class SignLanguageCardData : ScriptableObject
{
    public Sprite image;
    public string answer;

    public bool IsTrue(string text)
    {
        text = text.Trim();
        return string.Equals(text, answer, StringComparison.OrdinalIgnoreCase);
    }
}
