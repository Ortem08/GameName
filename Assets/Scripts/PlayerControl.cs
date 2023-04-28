using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 target;
    private NavMeshAgent agent;
    public bool isButton = true;
    private Transform[] buttons;

    void Start()
    {
        //buttons = FindObjectsOfType<Button>().Select(x => x.transform).ToArray();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
    }

    void SetTargetPosition()
    {
        var mouse = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            var camCenter = Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f, 0));
            isButton = mouse.x < camCenter.x - 90 && mouse.x > camCenter.x - 395 && mouse.y < camCenter.y + 106 && mouse.y > camCenter.y + 93;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!isButton) { SetAgentPosition(); }
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }
}
