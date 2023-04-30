using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory _inventory;
    public GameObject SlotButton;
    private PlayerControl _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        var distance = (_player.transform.position - gameObject.transform.position).magnitude;
        
        if (Input.GetKeyDown(KeyCode.E) && _player.hit.collider?.tag == "Item" && Mathf.Abs(distance) <= _player.PickUpDistance)
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

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") || other.CompareTag("Wall") || other.CompareTag("NPC"))
    //    {
    //        for (int j = 0; j < _inventory.slots.Length; j++)
    //        {
    //            if (!_inventory.isFull[j])
    //            {
    //                Destroy(gameObject);
    //                Instantiate(SlotButton, _inventory.slots[j].transform);
    //                _inventory.isFull[j] = true;
    //                break;
    //            }
    //        }
    //    }
    //}
}
