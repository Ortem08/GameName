using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedButtonActivate : MonoBehaviour
{
    private Inventory inventory;
    public int id;
    private PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void DropItem()
    {
        inventory.isFull[id] = false;
        if (transform.childCount > 0)
        {
            transform.GetChild(0).GetComponent<Spawn>().SpawnDroppedItem();
        }
    }
}
