using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using static UnityEngine.GraphicsBuffer;
using NavMeshSurface = Unity.AI.Navigation.NavMeshSurface;
using Random = System.Random;

public class NPCMove : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Vector3 _target;
    private Random rnd1 = new ();
    private Random rnd2 = new ();

    public bool IsInfected { get; private set; }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
    
    void Update()
    {
        if (_agent.remainingDistance == 0)
        {
            SetTargetPosition();
            SetAgentPosition();
        }
    }

    void SetTargetPosition()
    {
        var (x, y) = (rnd1.Next(-95, 113), rnd2.Next(0, 100));
        _target = new Vector3(x, y, 0);
    }

    void SetAgentPosition()
    {
        _agent.SetDestination(_target);
    }

    // Function to apply damage to the NPC within a given radius
    public void ApplyDamage(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            NPCMove npc = collider.GetComponent<NPCMove>();

            if (npc != null && !npc.IsInfected)
            {
                // Apply damage to the NPC
                Debug.Log("NPC takes damage!");
            }
        }
    }

    // Function to infect the NPC
    public void Infect()
    {
        IsInfected = true;
        // Apply infection effect to the NPC
        Debug.Log("NPC gets infected!");
    }

    // Function to update the infection effect on the NPC
    public void UpdateInfectionEffect()
    {
        // Update infection effect on the NPC
        Debug.Log("NPC infection effect updated!");
    }
}
