using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // ��� ������������� IPointerClickHandler

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public bool isEmpty = true; // ����, ������������, ���� �� ����
    public Image slotImage; // ������ �� ��������� Image
    public Text slotText; // ������ �� ��������� Text, ���� ����

    private Inventory inventory; // ������ �� ���������

    private void Awake()
    {
        // �������� ������ �� ����������
        slotImage = transform.GetChild(1).GetComponent<Image>(); // Image
        slotText = transform.GetChild(0).GetComponent<Text>(); // Text
        inventory = FindObjectOfType<Inventory>(); // ����� ��������� � �����
    }

    // ��������� ����� �� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���������, ������ �� ����� ������ ����
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // ���� ���� ����, �� ��������� � ���� �������
            if (isEmpty)
            {
                if (inventory.currentSlot != null)
                {
                    // ����������� ��������
                    inventory.MoveItem(inventory.currentSlot, this);
                    inventory.currentSlot = null; // ���������� ������� ���������
                }
            }
            else
            {
                // ���� ���� �����, ��������� ��� ��� �������
                inventory.currentSlot = this;
            }
        }
        // ���������, ������ �� ������ ������ ����
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // ���� ���� �� ����, ������� ���
            if (!isEmpty)
            {
                inventory.ClearSlot(this);
            }
        }
    }

    // ����� ��� ����������� �������� � ���� ����
    public void MoveItem(Sprite newSprite)
    {
        // ��������� ������
        slotImage.sprite = newSprite;

        // ������������� ���� isEmpty � false
        isEmpty = false;
    }

    // ����� ��� ������� �����
    public void ClearSlot()
    {
        slotImage.sprite = null;

        // ���������, �� �������� �� slotText null
        if (slotText != null)
        {
            slotText.text = "";
        }

        isEmpty = true;
    }

}
