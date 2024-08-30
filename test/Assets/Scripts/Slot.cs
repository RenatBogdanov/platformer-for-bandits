using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Для реализации IPointerClickHandler

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public bool isEmpty = true; // Флаг, указывающий, пустой ли слот
    public Image slotImage; // Ссылка на компонент Image слота
    public Text slotText; // Ссылка на компонент Text слота, если он существует

    private Inventory inventory; // Ссылка на инвентарь

    private void Awake()
    {
        // Инициализация ссылок на UI компоненты слота
        slotImage = transform.GetChild(1).GetComponent<Image>(); // Image
        slotText = transform.GetChild(0).GetComponent<Text>(); // Text
        inventory = FindObjectOfType<Inventory>(); // Получаем инвентарь в сцене
    }

    // Реализация события клика на слоте
    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем, был ли клик левой кнопкой мыши
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Если слот пуст, пытаемся добавить в него предмет
            if (isEmpty)
            {
                if (inventory.currentSlot != null)
                {
                    // Перемещаем предмет из текущего слота в этот слот
                    inventory.MoveItem(inventory.currentSlot, this);
                    inventory.currentSlot = null; // Очищаем ссылку на текущий слот
                }
            }
            else
            {
                // Если слот занят, устанавливаем его как текущий слот
                inventory.currentSlot = this;
            }
        }
        // Проверяем, был ли клик правой кнопкой мыши
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Если слот не пустой, очищаем его
            if (!isEmpty)
            {
                inventory.ClearSlot(this);
            }
        }
    }

    // Метод для перемещения предмета в этот слот
    public void MoveItem(Sprite newSprite)
    {
        // Присваиваем новый спрайт изображению слота
        slotImage.sprite = newSprite;

        // Устанавливаем флаг isEmpty в false
        isEmpty = false;
    }

    // Метод для очистки слота
    public void ClearSlot()
    {
        slotImage.sprite = null; // Убираем спрайт из слота

        // Если slotText существует, очищаем его текст
        if (slotText != null)
        {
            slotText.text = "";
        }

        isEmpty = true; // Устанавливаем слот как пустой
    }
}

