using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory _inventory;
    public GameObject SlotButton;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall") || other.CompareTag("NPC"))
        {
            for (int j = 0; j < _inventory.slots.Length; j++)
            {
                if (!_inventory.isFull[j])
                {
                    Destroy(gameObject);
                    Instantiate(SlotButton, _inventory.slots[j].transform);
                    _inventory.isFull[j] = true;
                    break;
                }
            }
        }
    }
}
