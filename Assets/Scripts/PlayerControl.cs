using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public bool TutorialFinished;
    public float PickUpDistance = 3f;
    public RaycastHit2D hit;

    private Vector3 target;
    private NavMeshAgent agent;
    private bool isButton = true;
    
    private Vector3 startPosition;
    private Vector3 direction;

    private bool tutorailButton;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
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
            var camUpY = Camera.main.pixelHeight;
            isButton = (mouse.x > 20 && mouse.x < 325
                                    && mouse.y < camUpY - 15 && mouse.y > camUpY - 135)
                || tutorailButton;

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!isButton)
                agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));

            startPosition = transform.position;
            direction = agent.destination - startPosition;
        }

        DrawRay();
    }

    private void DrawRay()
    {
        Physics2D.queriesStartInColliders = false;
        hit = Physics2D.Raycast(transform.position, direction, PickUpDistance);
    }
}
