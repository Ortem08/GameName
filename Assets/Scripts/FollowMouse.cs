using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - Input.mousePosition;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
