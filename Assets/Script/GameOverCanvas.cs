using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshProUGUI scoreTextValue;

    GameManager gameManager;
    PopUp popUp;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        popUp = GetComponent<PopUp>();
    }

    public void SetScore()
    {
        scoreTextValue.text = gameManager.Score.ToString();
        Time.timeScale = 0f;
    }
}
