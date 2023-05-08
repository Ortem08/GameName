using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class MaskScript : MonoBehaviour
{
    private GameObject[] npcs;
    private Vector3 spawnPoint;
    public Sprite MaskImage;
    void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        spawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;

        foreach (var npc in npcs)
        {
            var distance = Mathf.Abs((npc.transform.position - spawnPoint).magnitude);
            if (distance < 10)
            {
                RunAway(npc);
            }
        }
    }

    void Update()
    {
        //foreach (var npc in npcs)
        //{
        //    var distance = Mathf.Abs((npc.transform.position - spawnPoint).magnitude);
        //    if (distance < 5)
        //    {
        //        RunAway(npc);
        //    }
        //}
    }

    private void RunAway(GameObject npc)
    {
        var navAgent = npc.GetComponent<NavMeshAgent>();
        navAgent.destination = ((navAgent.transform.position - spawnPoint) * 3) + spawnPoint;
    }
}