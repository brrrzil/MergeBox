using UnityEngine;

[RequireComponent(typeof(MasterItem))]

public class SwordScript : MonoBehaviour
{
    private GameObject hero;
    private float bonusDamage;

    public void AddPower() // Бонусы от меча
    {
        int rank = GetComponent<MasterItem>().rank;

        bonusDamage = (rank * 2) + (rank * rank);
        hero = GameObject.FindGameObjectWithTag("Hero");
        hero.GetComponent<Fighting>().Power += bonusDamage;
    }
}