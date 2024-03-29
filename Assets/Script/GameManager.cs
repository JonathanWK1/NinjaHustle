using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using Unity.Mathematics;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public float MaxTimeOut;
    public int SpaceBetweenCards;
    public UnityEvent playerAttack;
    public UnityEvent enemyAttack;

    [SerializeField] TMP_InputField TextInput;
    [SerializeField] Slider PlayerHPBar;
    [SerializeField] Slider EnemyHPBar;
    [SerializeField] RectTransform SignLanguageContainer;
    [SerializeField] Transform EnemyPrefabContainer;
    [SerializeField] GameObject SignLanguageCardPrefab;
 
    [SerializeField] List<Sprite> cardImage;

    [SerializeField] SoundManager soundManager;

    [SerializeField] Character Player;
    [SerializeField] Slider TimerSlider;

    List<SignLanguageCardData> currCards = new List<SignLanguageCardData>();
    List<GameObject> currCardObjects = new List<GameObject>();

    List<SignLanguageCardData> allCards;


    [SerializeField]
    GameObject EnemyPrefab;

    [SerializeField]
    GameObject BossPrefab;

    Character enemy;

    [SerializeField]
    PopUp GameOverPopUp;

    public int Score;
    int enemyCount = 0;
    const int maxCurrCard = 3;
    float timeOut;
    float currMaxTimeOut;
    bool gameOver;
    void Start()
    {
        //MakeScriptable();
        enemyCount = 0;
        gameOver = false;
        Score = 0;
        TextInput.ActivateInputField();
        Time.timeScale = 1;
        currMaxTimeOut = MaxTimeOut;
        timeOut = currMaxTimeOut;
        TimerSlider.maxValue = currMaxTimeOut;
        TimerSlider.value = timeOut;
        allCards = Resources.LoadAll<SignLanguageCardData>("Sign Language Card/").ToList();
        Player.CharacterDead.AddListener(OnCharacterDead);
        Player.HPChanged.AddListener(SetHpUI);
        SpawnCards();
        DrawCards();
        SpawnEnemy();

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void FocusText()
    {
        TextInput.Select();
        TextInput.ActivateInputField();
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
        if (gameOver)
        {
            TextInput.DeactivateInputField();
            return;
        }
        if (timeOut >= 0)
        {
            timeOut -= Time.deltaTime;
            TimerSlider.value = timeOut;
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
            Score += 1;
            soundManager.PlaySFX("Attack");
            playerAttack.Invoke();
            enemy.TakeDamage(Player.Damage);
        }
        else
        {
            soundManager.PlaySFX("Hurt");
            enemyAttack.Invoke();
            Player.TakeDamage(enemy.Damage);
        }
        TextInput.text = "";

        timeOut = currMaxTimeOut;
        TimerSlider.value = currMaxTimeOut;
    }

    public void SetHpUI(int hp, bool IsPlayer)
    {
        if (IsPlayer)
        {
            PlayerHPBar.value = hp;
        }
        else
        {
            EnemyHPBar.value = hp;
        }
    }

    public void OnCharacterDead(bool IsPlayer)
    {
        if (IsPlayer)
        {
            soundManager.PlaySFX("Lose");
            gameOver = true;
            GameOverPopUp.Show();
        }
        else
        {
            Score += enemy.Score;
            Destroy(enemy.gameObject);
            soundManager.PlaySFX("Win");
            enemyCount++;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (enemyCount%3 == 1)
        {
            InstantiateEnemy(BossPrefab);
        }
        else
        {
            InstantiateEnemy(EnemyPrefab);
        }
    }

    public void InstantiateEnemy(GameObject enemyPrefab)
    {
        GameObject enemyObject = Instantiate(enemyPrefab, EnemyPrefabContainer);

        Character enemyCharacter = enemyObject.GetComponent<Character>();
        enemyObject.transform.localPosition = Vector3.zero;


        enemyCharacter.Damage += enemyCount / 4;
        enemyCharacter.Damage = math.clamp(enemyCharacter.Damage, 1, 5);

        enemyCharacter.MaxHP *=  1 + (enemyCharacter.MaxHPMultiplier * (enemyCount / 4));

        EnemyHPBar.maxValue = enemyCharacter.MaxHP;
        EnemyHPBar.value = enemyCharacter.MaxHP;

        enemyCharacter.CharacterDead.AddListener(OnCharacterDead);
        enemyCharacter.HPChanged.AddListener(SetHpUI);

        currMaxTimeOut = MaxTimeOut - enemyCount / 2;
        currMaxTimeOut = math.clamp(currMaxTimeOut, 1,5);

        TimerSlider.maxValue = currMaxTimeOut;
        enemy = enemyCharacter;
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
        TextInput.ActivateInputField();
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
            currCardObjects[i].transform.DOMoveY(currCardObjects[i-1].transform.position.y, 0.2f).SetEase(Ease.InBounce);
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
        //cardObject.transform.localPosition = new Vector3((currCardObjects.Count) * (cardTransform.rect.width + SpaceBetweenCards), 0, 0);
        cardObject.transform.localPosition = new Vector3(0, currCardObjects.Count * (cardTransform.rect.height + SpaceBetweenCards), 0);
        currCardObjects.Add(cardObject);
    }
}
