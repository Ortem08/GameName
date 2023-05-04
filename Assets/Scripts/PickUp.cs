using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject SlotButton;
    private PlayerControl player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        var distance = (player.transform.position - gameObject.transform.position).magnitude;
        if (Input.GetKeyDown(KeyCode.F) && player.hit.collider?.tag is "Item" && Mathf.Abs(distance) <= player.PickUpDistance)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (!inventory.isFull[i])
                {
                    Destroy(gameObject);
                    Instantiate(SlotButton, inventory.slots[i].transform);
                    inventory.isFull[i] = true;
                    break;
                }
            }
        }
    }
}
