using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float MaxTimeOut;
    public int PlayerDamage;
    public int EnemyDamage;

    [SerializeField] TMP_InputField TextInput;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] Slider PlayerHPBar;
    [SerializeField] Slider EnemyHPBar;
    [SerializeField] RectTransform SignLanguageContainer;
    [SerializeField] GameObject SignLanguageCardPrefab;

    List<SignLanguageCard> SignLanguageCards;

    int maxCard;
    float TimeOut;
    void Start()
    {
        maxCard = 5;
        TimeOut = MaxTimeOut;
        TimerText.text = Convert.ToInt32(TimeOut).ToString();
        SpawnCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeOut >= 0)
        {
            TimeOut -= Time.deltaTime;
            TimerText.text = Convert.ToInt32(TimeOut).ToString();
        }
        else
        {
            ResetTimer(false);
        }
    }

    void ResetTimer(bool IsSuccess)
    {
        if (IsSuccess)
        {
            EnemyHPBar.value -= PlayerDamage;
        }
        else
        {
            PlayerHPBar.value -= EnemyDamage;
        }
        
        if (EnemyHPBar.value <= 0 || PlayerHPBar.value <= 0)
        {
            ResetGame();
        }
        TextInput.text = "";


        TimerText.text = Convert.ToInt32(TimeOut).ToString();
        TimeOut = MaxTimeOut;
    }
    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void onInputTextChange(string name)
    {
        if (name == "") return;
        if (name == name && name[name.Length - 1] == ' ')
        {
            ResetTimer(true);
        }
    }

    public void SpawnCards()
    {
        Vector3 spawnPosition = SignLanguageContainer.localPosition;
        
        if (SignLanguageCards.Count < maxCard)
        {
            int randInt = UnityEngine.Random.Range(0, 6);

        }
    }

}
