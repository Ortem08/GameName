using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public uint NPCID;

    private NavMeshAgent agent;
    private Vector3 target;
    private Animator AnimatorController { get; set; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        AnimatorController = GetComponent<Animator>();
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
        var (x, y) = (Random.Range(-95, 113), Random.Range(0, 100));
        target = new Vector3(x, y, 0);
        agent.SetDestination(target);
    }

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
