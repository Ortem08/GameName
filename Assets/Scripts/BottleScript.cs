using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class BottleScript : MonoBehaviour
{
    public Sprite BottleImage;
    private GameObject bottle;
    private bool buttonPressed;
    private Vector3 spawnPoint;
    private GameObject[] npcs;
    
    void Start()
    {
        
        StartCoroutine(WaitForKeyPress(KeyCode.Mouse1));
        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }

    void Update()
    {
        if (buttonPressed)
        {
            //StartCoroutine(WaitFor(7));
            StartCoroutine(LookAround());

            //Destroy(gameObject);
            //Destroy(bottle);
        }
    }

    private IEnumerator LookAround()
    {
        foreach (var bro in npcs)
        {
            var distance = Mathf.Abs((bro.transform.position - spawnPoint).magnitude);
            if (distance < 3)
            {
                SlowDown(bro);
            }
            yield return null;
        }
    }

    private void SlowDown(GameObject npc)
    {
        var wasShocked = false;
        var rnd = new System.Random();
        var abc = npc.GetComponent<NavMeshAgent>();
        abc.speed = 0.5f;
        Debug.Log("npcdofk");
        //if (rnd.NextDouble() % 157 < 1e-4)
        //{
        //    Debug.Log("shock");
        //    wasShocked = true;
        //    abc.speed = 0;
        //    StartCoroutine(WaitFor(10));
        //    abc.speed = 3.5f;
        //}

        if(!wasShocked)
        {
            Debug.Log("not shock");
            StartCoroutine(WaitFor(10));
            //abc.speed = 3.5f;
        }
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(10);
        Debug.Log("done");
    }


    private IEnumerator WaitForKeyPress(KeyCode key)
    {
        while (!Input.GetKeyDown(key))
            yield return null;
        
        spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPoint.z = 0;

        bottle = new GameObject();
        bottle.AddComponent<SpriteRenderer>();
        bottle.GetComponent<SpriteRenderer>().sprite = BottleImage;
        bottle.GetComponent<SpriteRenderer>().sortingOrder = 10;
        bottle.transform.position = spawnPoint;
        buttonPressed = true;
    }
}