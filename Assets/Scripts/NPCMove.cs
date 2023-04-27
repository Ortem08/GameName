using System;
using System.Collections;
using System.Collections.Generic;
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
    private Random _rnd = new Random();

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        SetAgentPosition();
    }

    void SetTargetPosition()
    {
        _target = new Vector3(_rnd.Next(-150, 150), _rnd.Next(-150, 150), 0);
    }

    void SetAgentPosition()
    {
        _agent.SetDestination(new Vector3(_target.x, _target.y, transform.position.z));
    }
}
