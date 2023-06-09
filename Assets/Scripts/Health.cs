using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Fighting))]

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private Transform hpBar;
    [SerializeField] private GameObject shop, forge;

    public float MaxHP { get { return maxHP; } set { maxHP = value; } }

    private float currentHP;
    private Animator animator;

    private float bonusHP, bonusPower, bonusShield; // Здесь будут бонусы от предметов (если успею)

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    private void Death()
    {
        if (!CompareTag("Hero")) // За мёртвого героя награда не полагается)
        {
            shop.GetComponent<ShopScript>().GainMoney(GetComponent<EnemyBehaviour>().Income);
            forge.GetComponent<ForgeScript>().AddThreeItems();
        }
        animator.SetBool("isDead", true);
        currentHP = 0;
        StartCoroutine(DeathCoroutine()); // Сначала анимация и только после задержки в одну секунду объект удаляется
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1);
        if (!CompareTag("Hero")) // Герой не удаляется, т. к. к нему привязаны разные события. Пусть лежит мёртвый.
        {
            Destroy(gameObject);
        }
    }

    public void GetDamage(float damage) // Получение урона за вычетом блока
    {
        if (damage > GetComponent<Fighting>().Shield)
            currentHP = currentHP - (damage - GetComponent<Fighting>().Shield);
        if (!IsAlive()) Death();
    }

    public bool IsAlive()
    {
        if (currentHP > 0) return true;
        else return false;
    }

    public void BonusHP(float bonus) // Покупка здоровья в магазине
    {
        currentHP += bonus;
    }

    void Update()
    {
        hpBar.localScale = new Vector3((float)currentHP / 100, 0.1f, 1); // Шкала здоровья
    }
}