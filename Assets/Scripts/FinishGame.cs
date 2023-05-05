using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject obj;
    private Rigidbody2D rigidbody2D;
    private Collider2D collider2D;
    private GameObject player;
    private Collider2D playerCollider;

    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Finish");
        rigidbody2D = obj.GetComponent<Rigidbody2D>();
        collider2D = obj.GetComponent <Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider2D>();
        Debug.Log(obj.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody2D.IsTouching(playerCollider) && CheckNPCsHealth())
        {
            Debug.Log("Close Game");
            Application.Quit();
        }
    }

    private bool CheckNPCsHealth()
    {
        var npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (var npc in npcs)
        {
            if (npc.GetComponentInChildren<HpBar>().CurrentHeath < 5)
            {
                return true;
            }
        }
        return false;
    }
}
