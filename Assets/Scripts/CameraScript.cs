using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float smoothing = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;

    private Transform player;
    private int lastX;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(offset.x, offset.y);
        FindPlayer(isLeft);
    }

    // Update is called once per frame
    void Update()
    {
        int currentX = Mathf.RoundToInt(player.position.x);
         
        if (currentX > lastX)
            isLeft = false;
        else if (currentX < lastX)
            isLeft = true;
        lastX = currentX;
        Vector3 target = isLeft
            ? new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z)
            : new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        var currentPosition = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);
        transform.position = currentPosition;
    }

    public void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindWithTag("Player").transform;
        lastX = Mathf.RoundToInt(player.position.x);

        transform.position = playerIsLeft 
            ? new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z)
            : new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
