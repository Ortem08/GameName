using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    public float Radius = 15f;
    [Range(1, 360)] public float angle = 360f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    private GameObject player;

    public bool CanSeePlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FOV();
        }
    }

    private void FOV()
    {
        var rangeCheck = Physics2D.OverlapCircleAll(transform.position, Radius, targetLayer);

        if (rangeCheck.Length > 0)
        {
            var target = rangeCheck[0].transform;
            var directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                var distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                    CanSeePlayer = true;
                else
                    CanSeePlayer = false;
            }
            else if (CanSeePlayer)
                CanSeePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white; 
        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, Radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * Radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * Radius);

        if (CanSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegress)
    {
        angleInDegress += eulerY;

        return new Vector2(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }

}