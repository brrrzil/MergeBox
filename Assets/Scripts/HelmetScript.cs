using UnityEngine;

[RequireComponent(typeof(MasterItem))]

public class HelmetScript : MonoBehaviour
{
    private GameObject hero;
    private float bonusShield;

    public void AddShield() // ����� ������ �� �����
    {
        int rank = GetComponent<MasterItem>().rank;

        bonusShield = (rank * 2) + (rank * rank);
        hero = GameObject.FindGameObjectWithTag("Hero");
        hero.GetComponent<Fighting>().Shield += bonusShield;
    }
}