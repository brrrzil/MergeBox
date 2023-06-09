using UnityEngine;
using System;

[RequireComponent(typeof(MasterItem))]

public class StarScript : MonoBehaviour
{
    private GameObject hero;
    private float bonusStar;

    public void AddPower() // Бонусы от звёздочек
    {
        int rank = GetComponent<MasterItem>().rank;

        bonusStar = (float)Math.Pow(3, rank);
        hero = GameObject.FindGameObjectWithTag("Hero");

        hero.GetComponent<Fighting>().Power += bonusStar;
        hero.GetComponent<Fighting>().Shield += bonusStar;
        hero.GetComponent<Health>().BonusHP(bonusStar);
    }
}