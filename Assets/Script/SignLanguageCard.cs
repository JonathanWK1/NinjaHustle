using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignLanguageCard : MonoBehaviour
{

    [SerializeField] Image cardImage;

    public void InitializeCard(SignLanguageCardData data)
    {
        cardImage.sprite = data.image;
    }
}
