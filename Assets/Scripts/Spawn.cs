using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Item;
    private Transform _player;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public void SpawnDroppedItem()
    {
        Physics2D.queriesStartInColliders = false;
        var hit = Physics2D.Raycast(_player.position, Vector2.right, 1);

        if (hit.collider?.tag is not "Wall" and not "NPC")
        {
            var playerPos = new Vector2(_player.position.x + 1, _player.position.y);
            var itemObj = Instantiate(Item, playerPos, Quaternion.identity);
            itemObj.tag = "Item";
            Destroy(gameObject);
        }
    }
}
