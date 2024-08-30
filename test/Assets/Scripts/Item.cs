using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;

    // �����, ������� ���������� ��� �������������� � ���������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, �������� �� ������, � ������� ��������� ������������, �������
        if (collision.CompareTag("Player"))
        {
            // ������� ������ Inventory �� ����
            GameObject inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
            if (inventoryObject != null)
            {
                Inventory playerInventory = inventoryObject.GetComponent<Inventory>();
                // ���������, ���� �� ����� � ���������
                if (playerInventory.AddItem(icon))
                {
                    // ���������� ������� ������ ���� �� ��� �������� � ���������
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogError("������: Inventory �� ������ �� ����");
            }
        }
    }
}
