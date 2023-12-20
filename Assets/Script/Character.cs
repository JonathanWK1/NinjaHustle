using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    bool IsPlayer;

    public int MaxHP;
    public int Damage;
    public int Score;

    public int MaxHPMultiplier;

    public UnityEvent<bool> CharacterDead;
    public UnityEvent<int, bool> HPChanged;
    private int HP;

    public bool IsAnimationFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (IsPlayer)
        {
            gameManager.playerAttack.AddListener(SetToAttackAnimation);
            gameManager.enemyAttack.AddListener(SetToHurtAnimation);
        }
        else
        {
            gameManager.playerAttack.AddListener(SetToHurtAnimation);
            gameManager.enemyAttack.AddListener(SetToAttackAnimation);
        }
        BackToIdleAnimation();
    }

    private void Update()
    {
        if (IsAnimationFinished)
        {
            BackToIdleAnimation();
        }
    }

    public void TakeDamage(int damage)
    {
        HP = math.clamp(HP - damage, 0, MaxHP);
        HPChanged.Invoke(HP,IsPlayer);
        if (HP <= 0)
        {
            CharacterDead.Invoke(IsPlayer);
        }
    }


    void BackToIdleAnimation()
    {
        animator.SetInteger("AttackType", 0);
        animator.SetBool("IsHurt", false);
        IsAnimationFinished = false;
    }

    public void SetToAttackAnimation()
    {
        int attackType = UnityEngine.Random.Range(1, 2);
        animator.SetInteger("AttackType", attackType);
    }

    public void SetToHurtAnimation()
    {
        print("start");
        animator.SetBool("IsHurt", true);
    }

}
