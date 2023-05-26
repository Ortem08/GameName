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
    private Vector3 target;
    private NavMeshAgent agent;
    private bool isButton = true;

    private MoveState moveState = MoveState.Idle;
    private MoveDirection directionState = MoveDirection.Down;
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
        SetTargetPosition();
    }

    private void SetTargetPosition()
    {
        var mouse = Input.mousePosition;

        //var button = GameObject.FindGameObjectWithTag("TutorialMessage");
        //tutorailButton = false;
        //if (button is not null)
        //{
        //    var buttCoord = button.GetComponent<Transform>().position;
        //    var buttSize = button.GetComponent<RectTransform>().rect;
        //    tutorailButton = mouse.x < buttCoord.x + buttSize.width / 2 
        //                     && mouse.x > buttCoord.x - buttSize.width / 2
        //                     && mouse.y < buttCoord.y + buttSize.height / 2 
        //                     && mouse.y > buttCoord.y - buttSize.height / 2;
        //}

        if (Input.GetMouseButtonDown(0))
        {
            moveState = MoveState.Move;

            var camUpY = Camera.main.pixelHeight;
            var camRightX = Camera.main.pixelWidth;
            isButton = (mouse.x > 20 && mouse.x < 325
                                    && mouse.y < camUpY - 15 && mouse.y > camUpY - 135)
                || (mouse.x > camRightX - 107 && mouse.x < camRightX - 9
                                    && mouse.y < 88 && mouse.y > 58);

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!isButton)
            {
                agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));

                if (Vector3.Angle(Vector3.up, agent.destination) > 90)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
                        transform.localScale.z);
                    directionState = MoveDirection.Down;
                    animatorController.Play("PlayerWalkDown");
                }
                else
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
                        transform.localScale.z);
                    directionState = MoveDirection.Up;
                    animatorController.Play("PlayerWalkUp");
                }
            }
        }
    }

    private enum MoveDirection
    {
        Up,
        Down
    }

    private enum MoveState
    {
        Idle,
        Move
    }
}
