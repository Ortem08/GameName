using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Item;
    [SerializeField] public Vector3 SavedScale;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public void SpawnDroppedItem()
    {
        Physics2D.queriesStartInColliders = false;
        var hit = Physics2D.Raycast(player.position, Vector2.right, 1);

        if (hit.collider?.tag is not "Wall" and not "NPC")
        {
            Item.transform.localScale = SavedScale;
            var playerPos = new Vector2(player.position.x + 1, player.position.y);
            var itemObj = Instantiate(Item, playerPos, Quaternion.identity);
            itemObj.tag = "Item";
            Destroy(gameObject);
        }
    }
}
