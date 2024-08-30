using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // Для использования Select

public class Inventory : MonoBehaviour
{
    public Transform itemsParent; // Родительский объект для слотов инвентаря
    public GameObject slotPrefab; // Префаб слота инвентаря
    public GameObject inventoryCanvas; // Canvas инвентаря

    private List<GameObject> slots = new List<GameObject>(); // Список слотов инвентаря
    public Slot currentSlot; // Переменная для хранения текущего выбранного слота

    void Awake()
    {
        // Инициализируем список слотов
        slots = new List<GameObject>();

        // Подсчитываем ячейки в инвентаре
        int slotCount = itemsParent.childCount;
        Debug.Log("Количество ячеек: " + slotCount);
        // Скрываем инвентарь при старте
        inventoryCanvas.SetActive(false);

        // Находим все существующие слоты (если они есть)
        slots.AddRange(itemsParent.GetComponentsInChildren<Slot>().Select(slot => slot.gameObject));
    }

    void Update()
    {
        // Проверяем, нажата ли клавиша "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Переключаем видимость инвентаря
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
    }

    // Метод для добавления предмета в инвентарь
    public bool AddItem(Sprite iconSprite)
    {
        if (slots.Count > 0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].GetComponent<Slot>().isEmpty)
                {
                    slots[i].GetComponent<Slot>().MoveItem(iconSprite);
                    return true;
                }
            }
        }
        Debug.LogError("Ошибка: В инвентаре нет свободных слотов.");
        return false;
    }

    // Метод для перемещения предмета между слотами
    public void MoveItem(Slot fromSlot, Slot toSlot)
    {
        // Проверяем, что слоты не равны и что исходный слот не пуст
        if (fromSlot != toSlot && !fromSlot.isEmpty)
        {
            // Сохраняем текущее состояние предмета в исходном слоте
            Sprite currentSprite = fromSlot.slotImage.sprite;

            // Если целевой слот пуст, перемещаем предмет
            if (toSlot.isEmpty)
            {
                toSlot.MoveItem(currentSprite); // Перемещаем предмет в целевой слот
                fromSlot.ClearSlot(); // Очищаем исходный слот
            }
            else // Если целевой слот занят, меняем местами
            {
                Sprite targetSprite = toSlot.slotImage.sprite; // Сохраняем спрайт целевого слота
                toSlot.MoveItem(currentSprite); // Перемещаем предмет из исходного слота в целевой
                fromSlot.MoveItem(targetSprite); // Перемещаем предмет из целевого слота в исходный
            }
        }
    }

    // Метод для очистки слота
    public void ClearSlot(Slot slot)
    {
        slot.ClearSlot();
    }
}