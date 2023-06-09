using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class DragObject : MonoBehaviour
{
    [SerializeField] private AudioClip drag, drop, merge;

    private Vector2 mousePosition;
    private float x, y;
    private float offsetX, offsetY;
    private static bool mouseButtonReleased;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown() // Захват предмета мышью (в кузнице)
    {
        mouseButtonReleased = false;
        offsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        offsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        audioSource.PlayOneShot(drag);
    }

    private void OnMouseDrag() //Перетаскивание
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x - offsetX, mousePosition.y - offsetY);
    }

    private void OnMouseUp()
    {
        mouseButtonReleased = true;
        audioSource.PlayOneShot(drop);
    }

    private void OnTriggerStay2D(Collider2D collision) // Объединение предметов
    {
        string thisGameobjectName, collisiongameobjectName;

        thisGameobjectName = gameObject.name;
        collisiongameobjectName = collision.gameObject.name;

        if (mouseButtonReleased && thisGameobjectName == collisiongameobjectName && CompareRank(gameObject, collision.gameObject))
        {
            GameObject parent = GameObject.FindGameObjectWithTag("ItemStorage");
            GameObject newItem = Instantiate(NewPrefab(collision.gameObject), GetRandomPosition(), Quaternion.identity);
            newItem.transform.SetParent(parent.transform, false);
            newItem.GetComponent<AudioSource>().PlayOneShot(merge);
            mouseButtonReleased = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private Vector3 GetRandomPosition() // Vector3 для вновь созданного предмета
    {
        System.Random randomX = new System.Random();
        x = (float)randomX.Next(-30, 28) / 100;

        System.Random randomY = new System.Random();
        y = (float)randomY.Next(-32, 20) / 100;

        return new Vector3(x, y, 0);
    }

    private bool CompareRank(GameObject thisGO, GameObject collisionGO) // Условие для слияния предметов
    {
        if (thisGO.GetComponent<MasterItem>().rank == collisionGO.GetComponent<MasterItem>().rank)
        {
            return true;
        }
        else return false;
    }

    private GameObject NewPrefab(GameObject draggingGO) // Определение типа получаемого предмета и его ранга
    {
        int rank = draggingGO.GetComponent<MasterItem>().rank;

        if (rank < 5)
        {
            switch (draggingGO.name.Substring(0, 3)) // Похоже на костыль) Стоило бы усложнить, но времени было мало, а оно работает
            {
                case "Hel":
                    return draggingGO.GetComponent<MasterItem>().Helmets[rank];
                case "Swo":
                    return gameObject.GetComponent<MasterItem>().Swords[rank];
                case "Sta":
                    return gameObject.gameObject.GetComponent<MasterItem>().Stars[rank];
                default: return draggingGO;
            }
        }
        else return draggingGO;
    }
}