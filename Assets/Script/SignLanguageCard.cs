using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card")]
public class SignLanguageCard : ScriptableObject
{
    public Sprite image;
    public string Answer;

    public bool IsTrue(string text)
    {
        return text == Answer;
    }
}
