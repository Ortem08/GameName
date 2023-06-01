using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeath : MonoBehaviour
{
    private GameObject gravestone;

    void Start()
    {
        gravestone = Resources.Load<GameObject>("Prefabs/Gravestone");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInChildren<HpBar>().CurrentHeath < 0.01)
        {
            Instantiate(gravestone, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
