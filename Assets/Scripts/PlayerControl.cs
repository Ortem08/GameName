using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
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
        canWalk = mouse.x > 22 && mouse.x < 325 && mouse.y > 309 && mouse.y < 324;
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
