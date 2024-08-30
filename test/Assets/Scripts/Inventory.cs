using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // ��� ������������� Select

public class Inventory : MonoBehaviour
{
    public Transform itemsParent; // ������������ ������ ��� ������ ���������
    public GameObject slotPrefab; // ������ ����� ���������
    public GameObject inventoryCanvas; // Canvas ���������

    private List<GameObject> slots = new List<GameObject>(); // ������ ������ ���������
    public Slot currentSlot; // ���������� ��� �������� �������� ���������� �����

    void Awake()
    {
        // �������������� ������ ������
        slots = new List<GameObject>();

        // ������������ ������ � ���������
        int slotCount = itemsParent.childCount;
        Debug.Log("���������� �����: " + slotCount);
        // �������� ��������� ��� ������
        inventoryCanvas.SetActive(false);

        // ������� ��� ������������ ����� (���� ��� ����)
        slots.AddRange(itemsParent.GetComponentsInChildren<Slot>().Select(slot => slot.gameObject));
    }

    void Update()
    {
        // ���������, ������ �� ������� "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            // ����������� ��������� ���������
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
    }

    // ����� ��� ���������� �������� � ���������
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
        Debug.LogError("������: � ��������� ��� ��������� ������.");
        return false;
    }

    // ����� ��� ����������� �������� ����� �������
    public void MoveItem(Slot fromSlot, Slot toSlot)
    {
        // ���������, ��� ����� �� ����� � ��� �������� ���� �� ����
        if (fromSlot != toSlot && !fromSlot.isEmpty)
        {
            // ��������� ������� ��������� �������� � �������� �����
            Sprite currentSprite = fromSlot.slotImage.sprite;

            // ���� ������� ���� ����, ���������� �������
            if (toSlot.isEmpty)
            {
                toSlot.MoveItem(currentSprite); // ���������� ������� � ������� ����
                fromSlot.ClearSlot(); // ������� �������� ����
            }
            else // ���� ������� ���� �����, ������ �������
            {
                Sprite targetSprite = toSlot.slotImage.sprite; // ��������� ������ �������� �����
                toSlot.MoveItem(currentSprite); // ���������� ������� �� ��������� ����� � �������
                fromSlot.MoveItem(targetSprite); // ���������� ������� �� �������� ����� � ��������
            }
        }
    }

    // ����� ��� ������� �����
    public void ClearSlot(Slot slot)
    {
        slot.ClearSlot();
    }
}