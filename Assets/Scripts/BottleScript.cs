using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BottleScript : MonoBehaviour
{
    public Sprite BottleImage;
    private GameObject bottle;
    public GameObject BottleScriptHolder;
    public GameObject player;
    public RuntimeAnimatorController BottleAnimationController;
    public bool animationIsDone;

    private bool buttonPressed;
    private Vector3 spawnPoint;
    private GameObject[] npcs;
    private bool coroutineStarts;
    private bool itemIsDestroyed;

    private static System.Timers.Timer aTimer;

    void Start()
    {
        coroutineStarts = false;
        itemIsDestroyed = false;
        animationIsDone = false;

        aTimer = new System.Timers.Timer();
        aTimer.Interval = 2000;
        aTimer.AutoReset = false;
        aTimer.Enabled = false;

        StartCoroutine(WaitForKeyPress(KeyCode.Mouse1));
        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }

    void Update()
    {
        if (!aTimer.Enabled && coroutineStarts)
        {
            coroutineStarts = false;
            KillBottleScript();
        }

        if (buttonPressed && animationIsDone)
        {
            coroutineStarts = true;
            aTimer.Enabled = true;
            StartCoroutine(LookAround());

            //Destroy(gameObject);
            //Destroy(bottle);
        }
    }

    private void KillBottleScript()
    {
        Destroy(bottle);
        Destroy(BottleScriptHolder);
        itemIsDestroyed = true;
        UnfreezeNPCs();
    }

    private IEnumerator LookAround()
    {
        foreach (var npc in npcs)
        {
            var distance = Mathf.Abs((npc.transform.position - spawnPoint).magnitude);
            if (distance < 3 && !itemIsDestroyed)
            {
                SlowDown(npc);
            }

            //if (itemIsDestroyed)
            //{
            //    npc.GetComponent<NavMeshAgent>().speed = 3.5f;
            //}

            yield return null;
        }
    }

    private void UnfreezeNPCs()
    {
        foreach (var npc in npcs)
        {
            var distance = Mathf.Abs((npc.transform.position - spawnPoint).magnitude);
            if (distance <= 3 && itemIsDestroyed)
                npc.GetComponent<NavMeshAgent>().speed = 3.5f;
        }
    }

    private void SlowDown(GameObject npc)
    {
        var wasShocked = false;
        var rnd = new System.Random();
        var abc = npc.GetComponent<NavMeshAgent>();
        abc.speed = 0.5f;
        //Debug.Log("npcdofk");
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
            //Debug.Log("not shock");
            StartCoroutine(WaitFor(10));
            //abc.speed = 3.5f;
        }
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(10);
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
        bottle.AddComponent<Animator>().runtimeAnimatorController = BottleAnimationController;
        bottle.GetComponent<Transform>().position = player.GetComponent<Transform>().position;
        bottle.transform.position = player.transform.position;
        bottle.AddComponent<BottleAnimationScript>();
        bottle.GetComponent<BottleAnimationScript>().spawnpoint = spawnPoint;
        bottle.GetComponent<BottleAnimationScript>().bottleScript = gameObject.GetComponent<BottleScript>();

        buttonPressed = true;
    } 
}