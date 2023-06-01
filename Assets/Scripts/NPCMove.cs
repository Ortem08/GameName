using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.AI;
using static UnityEngine.GraphicsBuffer;
using NavMeshSurface = Unity.AI.Navigation.NavMeshSurface;
//using Random = System.Random;

public class NPCMove : MonoBehaviour
{
    public uint NPCID;

    private NavMeshAgent agent;
    private Vector3 target;

    public bool IsInfected { get; private set; }
    private Animator AnimatorController { get; set; }

    private int[] lightZones;
    private bool flashlight1, flashlight2, flashlight3, flashlight4, flashlight5;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        AnimatorController = GetComponent<Animator>();
        lightZones = new int[5];
        flashlight4 = true;
    }

    void Update()
    {
        AnimatorController.speed = agent.speed / 3;
        if (agent.remainingDistance < 0.1)
            SetTargetPosition();
        SetAnimation();
    }

    void SetTargetPosition()
    {
        var rnd = Random.Range(1, 100);
        var (x, y) = (0, 0);
        var lightZone = 0;
        if (flashlight1)
            lightZones[0] = 0;
        if (flashlight2)
            lightZones[1] = 20;
        if (flashlight3)
            lightZones[2] = 35;
        if (flashlight4)
            lightZones[3] = 60;
        if (flashlight5)
            lightZones[4] = 90;
        if (flashlight1 || flashlight2 || flashlight3 || flashlight4 || flashlight5 || rnd > 30)
        {
            lightZone = lightZones[Random.Range(0, lightZones.Length)];
            (x, y) = (Random.Range(lightZone, lightZone + 10), Random.Range(lightZone, lightZone + 10));
        }
        if (rnd < 30)
            (x, y) = (Random.Range(-95, 113), Random.Range(0, 100));
        lightZones = new int[5];
        target = new Vector3(x, y, 0);
        agent.SetDestination(target);
    }

    //// Function to apply damage to the NPC within a given Radius
    //public void ApplyDamage(float Radius)
    //{
    //    Debug.Log("NPC takes damage!");
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);

    //    foreach (Collider collider in colliders)
    //    {
    //        var npc = collider.GetComponent<NPCMove>();

    //        if (npc != null && npc.IsInfected)
    //        {
    //            // Apply damage to the NPC
    //            Debug.Log("NPC takes damage!");
    //        }
    //    }
    //}

    //// Function to infect the NPC
    //public void Infect()
    //{
    //    IsInfected = true;
    //    // Apply infection effect to the NPC
    //    Debug.Log("NPC gets infected!");
    //}

    //// Function to update the infection effect on the NPC
    //public void UpdateInfectionEffect()
    //{
    //    // Update infection effect on the NPC
    //    Debug.Log("NPC infection effect updated!");
    //}

    private void SetAnimation()
    {
        var angleVertical = Vector3.Angle(Vector3.up, agent.desiredVelocity.normalized);
        var angleHorizontal = Vector3.Angle(Vector3.right, agent.desiredVelocity.normalized);

        if (angleVertical <= 60)
            AnimatorController.Play(NPCID + "NPCWalkUp");
        else if (angleVertical >= 120)
            AnimatorController.Play(NPCID + "NPCWalkDown");
        else if (angleHorizontal <= 30)
            AnimatorController.Play(NPCID + "NPCWalkRight");
        else if (angleHorizontal > 150)
            AnimatorController.Play(NPCID + "NPCWalkLeft");
    }
}
