using UnityEngine;

public class MasterItem : MonoBehaviour
{
    // Используется на главном префабе всех предметов

    [SerializeField] public int rank;
    [SerializeField] public GameObject[] Helmets, Swords, Stars; // Для иерархии разных предметов одного типа (от низшего ранга к высшему)
}