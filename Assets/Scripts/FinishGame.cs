using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject obj;
    private Rigidbody2D finiRigidbody;
    private GameObject player;
    private Collider2D playerCollider;
    //private Timer gameTime;

    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Finish");
        finiRigidbody = obj.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider2D>();
        //gameTime = GetComponent<Timer>().lifeTime;
    }

    void Update()
    {
        if (finiRigidbody.IsTouching(playerCollider) && CheckNPCsHealth())
        {
            Debug.Log("Close Game");
            //LevelController.Instance.IsEndGame();
            //Application.Quit();
        }
    }

    private bool CheckNPCsHealth()
    {
        var npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (var npc in npcs)
            if (npc.GetComponentInChildren<HpBar>().CurrentHeath < 5)
                return true;

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LevelController.Instance.IsEndGame();
    }
}
