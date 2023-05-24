using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
            AddComponents(gameObj, item);
            if (item.name.Contains("Bottle"))
            {
                var onMouseItem = new GameObject();
                onMouseItem.AddComponent<SpriteRenderer>().sprite = item.sprite;
                onMouseItem.GetComponent<SpriteRenderer>().sortingOrder = 10;
                onMouseItem.AddComponent<FollowMouse>();
                StartCoroutine(CheckForUsage(gameObj, onMouseItem, item));
            }
            else 
                Destroy(item.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.W) && inventory.isFull[1])
        {
            var item = secondSlot.transform.GetChild(0).GetComponent<Image>();
            inventory.isFull[1] = false;

            var gameObj = new GameObject();
            AddComponents(gameObj, item);

            if (item.name.Contains("Bottle"))
            {
                var onMouseItem = new GameObject();
                onMouseItem.AddComponent<SpriteRenderer>().sprite = item.sprite;
                onMouseItem.GetComponent<SpriteRenderer>().sortingOrder = 10;
                onMouseItem.AddComponent<FollowMouse>();
                StartCoroutine(CheckForUsage(gameObj, onMouseItem, item));
            }
            else
                Destroy(item.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.E) && inventory.isFull[2])
        {
            var item = thirdSlot.transform.GetChild(0).GetComponent<Image>();
            inventory.isFull[2] = false;

            var gameObj = new GameObject();
            AddComponents(gameObj, item);

            if (item.name.Contains("Bottle"))
            {
                var onMouseItem = new GameObject();
                onMouseItem.AddComponent<SpriteRenderer>().sprite = item.sprite;
                onMouseItem.GetComponent<SpriteRenderer>().sortingOrder = 10;
                onMouseItem.AddComponent<FollowMouse>();
                StartCoroutine(CheckForUsage(gameObj, onMouseItem, item));
            }
            else
                Destroy(item.gameObject);
        }
    }

    private void AddComponents(GameObject gameObj, Image item)
    {
        if (item.name.Contains("Chest"))
        {
            gameObj.AddComponent<CasketScript>();
            gameObj.GetComponent<CasketScript>().CasketImage = item.sprite;
            gameObj.GetComponent<CasketScript>().CasketScriptHolder = gameObj;
        }

        if (item.name.Contains("Bottle"))
        {
            gameObj.AddComponent<BottleScript>();
            gameObj.GetComponent<BottleScript>().BottleImage = item.sprite;
            gameObj.GetComponent<BottleScript>().BottleScriptHolder = gameObj;
        }

        if (item.name.Contains("Dummy"))
        {
            gameObj.AddComponent<MaskScript>();
            gameObj.GetComponent<MaskScript>().MaskImage = item.sprite;
        }

        if (item.name.Contains("Virus"))
        {
            gameObj.AddComponent<FearVirusItem>();
        }
    }

    private IEnumerator CheckForUsage(GameObject item, GameObject onMouseItem, Image parenrtItem)
    {
        var bottleScriptHolder = item.GetComponent<BottleScript>();
        while (!bottleScriptHolder.isTimerEverEnabled)
            yield return null;
        Destroy(onMouseItem);

        while (!bottleScriptHolder.isTimerEnded)
            yield return null;

        Destroy(item.gameObject);
        Destroy(parenrtItem);
        bottleScriptHolder.KillBottleScript();
    }
}
