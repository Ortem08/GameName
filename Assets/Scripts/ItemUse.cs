using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemUse : MonoBehaviour
{
    private GameObject firstSlot;
    private GameObject secondSlot;
    private GameObject thirdSlot;
    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        firstSlot = inventory.slots[0];
        secondSlot = inventory.slots[1];
        thirdSlot = inventory.slots[2];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inventory.isFull[0])
        {

            var item = firstSlot.transform.GetChild(0).GetComponent<Image>();
            inventory.isFull[0] = false;
            var gameObj = new GameObject();
            Debug.Log(item.name);
            AddComponents(gameObj, item);

            Destroy(item.gameObject);
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

    private void AddComponents(GameObject gameObj, Image item)
    {
        if (item.name.Contains("Chest"))
        {
            gameObj.AddComponent<CasketScript>();
            gameObj.GetComponent<CasketScript>().casketImage = item.sprite;
        }

        if (item.name.Contains("Bottle"))
        {
            gameObj.AddComponent<BottleScript>();
            gameObj.GetComponent<BottleScript>().bottleImage = item.sprite;
        }

        if (item.name.Contains("Apple"))
        {
            gameObj.AddComponent<CasketScript>();
            gameObj.GetComponent<CasketScript>().casketImage = item.sprite;
        }
    }
}
