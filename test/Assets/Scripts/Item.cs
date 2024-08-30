using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;

    // Метод, который вызывается при взаимодействии с предметом
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, является ли объект, с которым произошло столкновение, игроком
        if (collision.CompareTag("Player"))
        {
            // Находим объект Inventory по тегу
            GameObject inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
            if (inventoryObject != null)
            {
                Inventory playerInventory = inventoryObject.GetComponent<Inventory>();
                // Проверяем, есть ли место в инвентаре
                if (playerInventory.AddItem(icon))
                {
                    // Уничтожаем предмет только если он был добавлен в инвентарь
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogError("Ошибка: Inventory не найден по тегу");
            }
        }
    }
}
