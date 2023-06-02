using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public GameObject ImageInSlot;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void PickUpObject()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
            if (!inventory.isFull[i])
            {
                ImageInSlot.GetComponent<Spawn>().SavedScale = gameObject.transform.localScale;
                Destroy(gameObject);
                Instantiate(ImageInSlot, inventory.slots[i].transform);
                inventory.isFull[i] = true;
                break;
            }
    }
}
