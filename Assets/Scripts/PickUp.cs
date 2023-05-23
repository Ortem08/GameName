using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject SlotButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void PickUpObject()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
            if (!inventory.isFull[i])
            {
                Destroy(gameObject);
                Instantiate(SlotButton, inventory.slots[i].transform);
                inventory.isFull[i] = true;
                break;
            }
    }
}
