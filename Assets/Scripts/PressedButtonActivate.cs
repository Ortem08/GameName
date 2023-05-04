using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedButtonActivate : MonoBehaviour
{
    private Inventory _inventory;
    public int Id;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    
    public void DropItem()
    {
        _inventory.isFull[Id] = false;
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<Spawn>().SpawnDroppedItem();
    }
}
