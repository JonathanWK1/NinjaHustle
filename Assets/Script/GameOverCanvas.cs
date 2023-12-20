using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshProUGUI scoreTextValue;
    GameManager gameManager;
    Canvas canvas;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        canvas = GetComponent<Canvas>();
        gameManager.GameEnds.AddListener(ShowCanvas);
    }

    void ShowCanvas(bool Win)
    {
        scoreTextValue.text = gameManager.Score.ToString();
        canvas.enabled = true;
    }
}
