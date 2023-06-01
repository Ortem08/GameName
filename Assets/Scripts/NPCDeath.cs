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
    
    void Update()
    {
        if (gameObject.GetComponentInChildren<HpBar>().CurrentHeath < 0.01)
        {
            Instantiate(gravestone, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
