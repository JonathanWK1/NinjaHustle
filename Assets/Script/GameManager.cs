using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public float MaxTimeOut;
    public int PlayerDamage;
    public int EnemyDamage;
    public int SpaceBetweenCards;

    [SerializeField] TMP_InputField TextInput;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] Slider PlayerHPBar;
    [SerializeField] Slider EnemyHPBar;
    [SerializeField] RectTransform SignLanguageContainer;
    [SerializeField] GameObject SignLanguageCardPrefab;
    [SerializeField] Canvas GameOverCanvas;

    List<SignLanguageCard> currCards = new List<SignLanguageCard>();

    List<SignLanguageCard> allCards;

    int maxCurrCard;
    float timeOut;
    void Start()
    {
        Time.timeScale = 1;
        maxCurrCard = 5;
        timeOut = MaxTimeOut;
        TimerText.text = Convert.ToInt32(timeOut).ToString();
        allCards = Resources.LoadAll<SignLanguageCard>("Sign Language Card/").ToList();
        SpawnCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOut >= 0)
        {
            timeOut -= Time.deltaTime;
            TimerText.text = Convert.ToInt32(timeOut).ToString();
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
            Time.timeScale = 0f;
            GameOverCanvas.enabled = true;
        }
        TextInput.text = "";


        TimerText.text = Convert.ToInt32(timeOut).ToString();
        timeOut = MaxTimeOut;
    }
    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void onInputTextChange(string name)
    {
        if (name.Length == 0) return;
        if (name[name.Length - 1] == ' ' && currCards[0].IsTrue(name))
        {
            currCards.RemoveAt(0);
            SpawnCards();
            ResetTimer(true);
            
        }
    }

    public void SpawnCards()
    {
        while (currCards.Count < maxCurrCard)
        {
            int randInt = UnityEngine.Random.Range(0, allCards.Count);
            currCards.Add(allCards[randInt]);
        }

        DrawCards();
    }

    public void DrawCards()
    {
        int i = 0;
        Vector3 spawnPosition = SignLanguageContainer.localPosition;

        foreach (SignLanguageCard card in currCards)
        {
            GameObject cardObject = Instantiate(SignLanguageCardPrefab, SignLanguageContainer);
            Image image = cardObject.GetComponent<Image>();
            RectTransform cardTransform = cardObject.GetComponent<RectTransform>();
            image.sprite = card.image;
            cardObject.transform.localPosition = new Vector3(spawnPosition.x + i * (cardTransform.rect.width + SpaceBetweenCards), spawnPosition.y,spawnPosition.z);
            i++;
        }
    }
}
