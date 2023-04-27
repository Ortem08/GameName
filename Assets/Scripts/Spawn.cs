using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Item;
    private Transform _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public void SpawnDroppedItem()
    {
        var playerPos = new Vector2(_player.position.x + 1, _player.position.y);
        Instantiate(Item, playerPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
