using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PlayerControl : MonoBehaviour
{
    public float PickUpDistance = 1.5f;
    public RaycastHit2D hit;

    private Vector3 target;
    private NavMeshAgent agent;
    private bool isButton = true;
    
    private Vector3 startPosition;
    private Vector3 direction;
    
    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    private void Update()
    {
        SetTargetPosition();
    }

    private void SetTargetPosition()
    {
        var mouse = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            var camCenter = Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f, 0));
            isButton = mouse.x < camCenter.x - 90 && mouse.x > camCenter.x - 395 
                                                  && mouse.y < camCenter.y + 106 && mouse.y > camCenter.y + 93;
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
