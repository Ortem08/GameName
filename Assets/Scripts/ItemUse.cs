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
    public RuntimeAnimatorController BottleAnimationController;

    private GameObject firstSlot;
    private GameObject secondSlot;
    private GameObject thirdSlot;
    private Inventory inventory;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        firstSlot = inventory.slots[0];
        secondSlot = inventory.slots[1];
        thirdSlot = inventory.slots[2];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inventory.isFull[0])
        {
            var objectImage = firstSlot.transform.GetChild(0).GetComponent<Image>();
            inventory.isFull[0] = false;
            var mainObject = new GameObject();
            AddComponents(mainObject, objectImage);
            if (objectImage.name.Contains("Bottle"))
            {
                var onMouseItem = new GameObject();
                onMouseItem.AddComponent<SpriteRenderer>().sprite = objectImage.sprite;
                onMouseItem.GetComponent<SpriteRenderer>().sortingOrder = 10;
                onMouseItem.AddComponent<FollowMouse>();
                StartCoroutine(CheckForUsage(mainObject, onMouseItem, objectImage));
            }
            else 
                Destroy(objectImage.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.W) && inventory.isFull[1])
        {
            var objectImage = secondSlot.transform.GetChild(0).GetComponent<Image>();
            inventory.isFull[1] = false;

            var mainObject = new GameObject();
            AddComponents(mainObject, objectImage);

            if (objectImage.name.Contains("Bottle"))
            {
                var onMouseItem = new GameObject();
                onMouseItem.AddComponent<SpriteRenderer>().sprite = objectImage.sprite;
                onMouseItem.GetComponent<SpriteRenderer>().sortingOrder = 10;
                onMouseItem.AddComponent<FollowMouse>();
                StartCoroutine(CheckForUsage(mainObject, onMouseItem, objectImage));
            }
            else
                Destroy(objectImage.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.E) && inventory.isFull[2])
        {
            var objectImage = thirdSlot.transform.GetChild(0).GetComponent<Image>();
            inventory.isFull[2] = false;

            var mainObject = new GameObject();
            AddComponents(mainObject, objectImage);

            if (objectImage.name.Contains("Bottle"))
            {
                var onMouseItem = new GameObject();
                onMouseItem.AddComponent<SpriteRenderer>().sprite = objectImage.sprite;
                onMouseItem.GetComponent<SpriteRenderer>().sortingOrder = 10;
                onMouseItem.AddComponent<FollowMouse>();
                StartCoroutine(CheckForUsage(mainObject, onMouseItem, objectImage));
            }
            else
                Destroy(objectImage.gameObject);
        }
    }

    private void AddComponents(GameObject gameObj, Image objectImage)
    {
        if (objectImage.name.Contains("Chest"))
        {
            var cascetScript = gameObj.AddComponent<CasketScript>();
            cascetScript.CasketImage = objectImage.sprite;
            cascetScript.CasketScriptHolder = gameObj;
        }

        if (objectImage.name.Contains("Turntable"))
        {
            var turntableScript = gameObj.AddComponent<TurntableScript>();
            turntableScript.TurntableImage = objectImage.sprite;
            turntableScript.TurntableScriptHolder = gameObj;
        }

        if (objectImage.name.Contains("Bottle"))
        {
            var bottleScript = gameObj.AddComponent<BottleScript>();
            bottleScript.BottleImage = objectImage.sprite;
            bottleScript.BottleScriptHolder = gameObj;
            bottleScript.Player = player;
            bottleScript.BottleAnimationController = BottleAnimationController;
        }

        if (objectImage.name.Contains("Dummy"))
        {
            var maskScript = gameObj.AddComponent<MaskScript>();
            maskScript.MaskImage = objectImage.sprite;
        }

        if (objectImage.name.Contains("Virus"))
        {
            gameObj.AddComponent<FearVirusItem>();
            gameObj.AddComponent<SpriteRenderer>().sprite = objectImage.sprite;
        }
    }

    private IEnumerator CheckForUsage(GameObject mainItem, GameObject onMouseItem, Image objectImage)
    {
        var bottleScriptHolder = mainItem.GetComponent<BottleScript>();
        while (!bottleScriptHolder.IsAnimationStarted)
            yield return null;
        Destroy(onMouseItem);
        Destroy(objectImage.gameObject);
    }
}
