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
    public bool canWalk = true;
    private Transform[] buttons;

    void Start()
    {
        buttons = FindObjectsOfType<Button>().Select(x => x.transform).ToArray();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        SetAgentPosition();
    }

    void SetTargetPosition()
    {
        var mouse = Input.mousePosition;
        foreach (var button in buttons)
        {
            canWalk = mouse.x < button.position.x + 50 && mouse.x > button.position.x - 50 && mouse.y < button.position.y + 10 && mouse.y > button.position.y - 10;
            if (canWalk == false)
                break;
        }
        
        if (Input.GetMouseButtonDown(0) && !canWalk)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(Input.mousePosition);
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }
}
