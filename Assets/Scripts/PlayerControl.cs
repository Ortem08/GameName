using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public bool BlockedByAnotherScript { get; set; }

    private Vector3 target;
    private NavMeshAgent agent;
    private bool isButton;
    
    private Animator animatorController;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animatorController = GetComponent<Animator>();
    }
    
    private void Update()
    {
        animatorController.speed = agent.speed / 3;
        if (Input.GetMouseButtonDown(0))
            SetTargetPosition();
        else if (agent.remainingDistance <= 0.1)
            animatorController.Play("PlayerIdle");
        else
            SetAnimation();
    }

    private void SetTargetPosition()
    {
        var mouse = Input.mousePosition;

        var camUpY = Camera.main.pixelHeight;
        var camRightX = Camera.main.pixelWidth;
        isButton = (mouse.x > 20 && mouse.x < 325
                                && mouse.y < camUpY - 15 && mouse.y > camUpY - 135)
            || (mouse.x > camRightX - 107 && mouse.x < camRightX - 9
                                && mouse.y < 88 && mouse.y > 58);

        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!isButton && !BlockedByAnotherScript)
        {
            agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));

            SetAnimation();
        }
    }

    private void SetAnimation()
    {
        var angleVertical = Vector3.Angle(Vector3.up, agent.desiredVelocity.normalized);
        var angleHorizontal = Vector3.Angle(Vector3.right, agent.desiredVelocity.normalized);
        
        if (angleVertical <= 60)
            animatorController.Play("PlayerWalkUp");
        else if (angleVertical >= 120)
            animatorController.Play("PlayerWalkDown");
        else if (angleHorizontal <= 30)
            animatorController.Play("PlayerWalkRight");
        else if (angleHorizontal > 150)
            animatorController.Play("PlayerWalkLeft");
    }
}
