using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Для использования IPointerClickHandler

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public bool isEmpty = true; // Флаг, показывающий, пуст ли слот
    public Image slotImage; // Ссылка на компонент Image
    public Text slotText; // Ссылка на компонент Text, если есть

    private Inventory inventory; // Ссылка на инвентарь

    private void Awake()
    {
        // Получаем ссылки на компоненты
        slotImage = transform.GetChild(1).GetComponent<Image>(); // Image
        slotText = transform.GetChild(0).GetComponent<Text>(); // Text
        inventory = FindObjectOfType<Inventory>(); // Найти инвентарь в сцене
    }

    // Обработка клика на слот
    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем, нажата ли левая кнопка мыши
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Если слот пуст, то добавляем в него предмет
            if (isEmpty)
            {
                if (inventory.currentSlot != null)
                {
                    // Перемещение предмета
                    inventory.MoveItem(inventory.currentSlot, this);
                    inventory.currentSlot = null; // Сбрасываем текущее состояние
                }
            }
            else
            {
                // Если слот занят, сохраняем его как текущий
                inventory.currentSlot = this;
            }
        }
        // Проверяем, нажата ли правая кнопка мыши
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Если слот не пуст, очищаем его
            if (!isEmpty)
            {
                inventory.ClearSlot(this);
            }
        }
    }

    // Метод для перемещения предмета в этот слот
    public void MoveItem(Sprite newSprite)
    {
        // Обновляем спрайт
        slotImage.sprite = newSprite;

        // Устанавливаем флаг isEmpty в false
        isEmpty = false;
    }

    // Метод для очистки слота
    public void ClearSlot()
    {
        slotImage.sprite = null;

        // Проверяем, не является ли slotText null
        if (slotText != null)
        {
            slotText.text = "";
        }

        isEmpty = true;
    }

}
