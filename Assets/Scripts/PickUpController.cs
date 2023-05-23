using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public float PickUpRadius = 1f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, PickUpRadius);
            foreach (var col in colliders)
            {
                if (col.gameObject.tag is not "Item")
                    continue;

                col.gameObject.GetComponent<PickUp>().PickUpObject();
            }
        }
    }
}
