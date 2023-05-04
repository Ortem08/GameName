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
    private Random rnd1 = new ();
    private Random rnd2 = new ();
    //private List<Vector3> movePoints;

    // Start is called before the first frame update
    void Start()
    {
        //movePoints = new List<Vector3>(){ new Vector3(0, 0, 0), new Vector3(1, 2, 0), new Vector3(4, 5, 0), new Vector3(8, 2, 0), new Vector3(3, 0, 0)};
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance == 0)
        {
            SetTargetPosition();
            SetAgentPosition();
        }
    }

    void SetTargetPosition()
    {
        var (x, y) = (rnd1.Next(-95, 113), rnd2.Next(0, 100));
        _target = new Vector3(x, y, 0);
    }

    void SetAgentPosition()
    {
        _agent.SetDestination(_target);
    }
}
