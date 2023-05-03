using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    private GameObject firstSlot;
    private GameObject secondSlot;
    private GameObject thirdSlot;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        firstSlot = inventory.slots[0];
        secondSlot = inventory.slots[1];
        thirdSlot = inventory.slots[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inventory.isFull[0])
        {
            Debug.Log(firstSlot.GetComponent<Transform>().GetChild(0));
        }

        if (Input.GetKeyDown(KeyCode.W) && inventory.isFull[1])
        {
            Debug.Log(secondSlot.GetComponent<Transform>().GetChild(0));
        }

        if (Input.GetKeyDown(KeyCode.E) && inventory.isFull[2])
        {
            Debug.Log(thirdSlot.GetComponent<Transform>().GetChild(0));
        }
    }
}
