using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject hero;
    [SerializeField] private TMP_Text powerValue, shieldValue, maxHPValue, moneyValue;

    private AudioSource audioSource;
    private float power, shield, maxHP, money = 30;
    private int cost = 10;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Отображение значений в магазине
        powerValue.text = hero.GetComponent<Fighting>().Power.ToString();
        shieldValue.text = hero.GetComponent<Fighting>().Shield.ToString();
        maxHPValue.text = hero.GetComponent<Health>().MaxHP.ToString();
        moneyValue.text = money.ToString();
    }

    public void GainMoney(int income) // Вызывается из Health в методе Death
    {
        money += income;
    }

    public void BuyPower() // Кнопка покупки атаки
    {
        audioSource.Play();
        if (money >= cost)
        {
            hero.GetComponent<Fighting>().Power += 5;
            money -= cost;
        }
    }

    public void BuyDefence() // Кнопка покупки защиты
    {
        audioSource.Play();
        if (money >= cost)
        {
            hero.GetComponent<Fighting>().Shield += 5;
            money -= cost;
        }
    }

    public void BuyHP() // Кнопка покупки здоровья
    {
        audioSource.Play();
        if (money >= cost)
        {
            hero.GetComponent<Health>().BonusHP(10);
            money -= cost;
        }
    }
}