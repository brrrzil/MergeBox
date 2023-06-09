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

    private float bonusHP, bonusPower, bonusShield; // ����� ����� ������ �� ��������� (���� �����)

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    private void Death()
    {
        if (!CompareTag("Hero")) // �� ������� ����� ������� �� ����������)
        {
            shop.GetComponent<ShopScript>().GainMoney(GetComponent<EnemyBehaviour>().Income);
            forge.GetComponent<ForgeScript>().AddThreeItems();
        }
        animator.SetBool("isDead", true);
        currentHP = 0;
        StartCoroutine(DeathCoroutine()); // ������� �������� � ������ ����� �������� � ���� ������� ������ ���������
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1);
        if (!CompareTag("Hero")) // ����� �� ���������, �. �. � ���� ��������� ������ �������. ����� ����� ������.
        {
            Destroy(gameObject);
        }
    }

    public void GetDamage(float damage) // ��������� ����� �� ������� �����
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

    public void BonusHP(float bonus) // ������� �������� � ��������
    {
        currentHP += bonus;
    }

    void Update()
    {
        hpBar.localScale = new Vector3((float)currentHP / 100, 0.1f, 1); // ����� ��������
    }
}