using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ForgeScript : MonoBehaviour
{
    [SerializeField] GameObject[] ItemList;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject forgePanel, shopPanel, itemStorage;

    private GameObject randomItem;
    private float x, y;
    private float bonus;

    private void Start()
    {
        AddThreeItems();
    }

    public void InstantiateRandomItem()
    {
        GetComponent<AudioSource>().Play();

        System.Random random = new System.Random();
        randomItem = ItemList[random.Next(ItemList.Length)] as GameObject;

        System.Random randomX = new System.Random();
        x = (float) randomX.Next(-30, 28) / 100;

        System.Random randomY = new System.Random();
        y = (float)randomY.Next(-32, 20) / 100;

        Vector3 itemPosition = new Vector3(x, y, 0);

        GameObject newItem = Instantiate(randomItem, itemPosition, Quaternion.identity);
        newItem.transform.SetParent(parent.transform, false);

        AddBonus(bonus);
    }

    public void AddThreeItems() // Закидывает в инвентарь три предмета
    {
        forgePanel.SetActive(true);                 // Вызывается так же при убийстве монстра из другого скрипта 
        itemStorage.SetActive(true);                // поэтому приходится делать видимой панель,
        shopPanel.SetActive(false);                 // иначе вылетает ошибка
        StartCoroutine(AddThreeItemsCoroutine());
    }

    private IEnumerator AddThreeItemsCoroutine() // Предметы создаются с задержкой, чтобы не было громкого звука и так эффектнее
    {
        yield return new WaitForSeconds(0.2f);
        InstantiateRandomItem();
        yield return new WaitForSeconds(0.3f);
        InstantiateRandomItem();
        yield return new WaitForSeconds(0.5f);
        InstantiateRandomItem();
    }

    private void AddBonus(float value)  // Здесь будут бонусы от предметов (если успею) 
    {                                   // upd: Не успел

    }
}