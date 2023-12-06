using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEditor.Progress;
using UnityEditor;

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

    [SerializeField] List<Sprite> cardImage;

    List<SignLanguageCardData> currCards = new List<SignLanguageCardData>();
    List<GameObject> currCardObjects = new List<GameObject>();

    List<SignLanguageCardData> allCards;



    const int maxCurrCard = 5;
    float timeOut;
    void Start()
    {
        //MakeScriptable();
        TextInput.ActivateInputField();
        Time.timeScale = 1;
        timeOut = MaxTimeOut;
        TimerText.text = Convert.ToInt32(timeOut).ToString();
        allCards = Resources.LoadAll<SignLanguageCardData>("Sign Language Card/").ToList();
        SpawnCards();
        DrawCards();
    }

    //void MakeScriptable()
    //{
    //    foreach (Sprite sprite in cardImage)
    //    {
    //        string path = "Assets/Resources/Sign Language Card/";
    //        // MyClass is inheritant from ScriptableObject base class
    //        SignLanguageCard example = ScriptableObject.CreateInstance<SignLanguageCard>();
    //        // path has to start at "Assets"
    //        example.answer = sprite.name;
    //        example.image = sprite;
    //        path += "Card " + sprite.name + ".asset";

    //        AssetDatabase.CreateAsset(example, path);
    //        AssetDatabase.SaveAssets();
    //        AssetDatabase.Refresh();
    //    }
    //}

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
        TextInput.ActivateInputField();
    }
    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void onInputTextChange(string name)
    {
        TextInput.ActivateInputField();
        if (name.Trim().Length == 0) return;
        if (name[name.Length - 1] == ' ')
        {
            if (currCards[0].IsTrue(name))
            {
                PopCard();
                ResetTimer(true);
            }
            else
            {
                ShakeCard();
                ResetTimer(false);
            }
        }
    }
     void ShakeCard()
    {
        currCardObjects[0].transform.DOShakeRotation(0.2f, 40,80);
    }
    void PopCard()
    {
        currCardObjects[0].transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).WaitForCompletion();

        for (int i = currCardObjects.Count - 1; i > 0; i--)
        {
            currCardObjects[i].transform.DOMoveX(currCardObjects[i-1].transform.position.x, 0.2f).SetEase(Ease.InBounce);
        }

        currCards.RemoveAt(0);
        currCardObjects.RemoveAt(0);

        SpawnCards();
        DrawCard(currCards[maxCurrCard-1]);
    }
    void SpawnCards()
    {
        while (currCards.Count < maxCurrCard)
        {
            int randInt = UnityEngine.Random.Range(0, allCards.Count);
            currCards.Add(allCards[randInt]);
        }
    }

    void DrawCards()
    {
        Vector3 spawnPosition = SignLanguageContainer.localPosition;
        foreach (SignLanguageCardData card in currCards)
        {
            DrawCard(card);
        }
    }

    void DrawCard(SignLanguageCardData cardData)
    {
        Vector3 spawnPosition = SignLanguageContainer.localPosition;
        GameObject cardObject = Instantiate(SignLanguageCardPrefab, SignLanguageContainer);

        SignLanguageCard signLanguageCard = cardObject.GetComponent<SignLanguageCard>();
        signLanguageCard.InitializeCard(cardData);

        RectTransform cardTransform = cardObject.GetComponent<RectTransform>();
        cardObject.transform.localPosition = new Vector3(spawnPosition.x + (currCardObjects.Count) * (cardTransform.rect.width + SpaceBetweenCards), spawnPosition.y, spawnPosition.z);
        currCardObjects.Add(cardObject);
    }
}
