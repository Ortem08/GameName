using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BottleScript : MonoBehaviour
{
    public Sprite bottleImage;

    private bool buttonPressed;
    private Vector3 spawnPoint;
    private GameObject[] npcs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForKeyPress(KeyCode.Mouse1));
        
        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
            foreach (var bro in npcs)
            {
                var distance = Mathf.Abs((bro.transform.position - spawnPoint).magnitude);
                if (distance < 3)
                    MakeDamage(bro, distance);
            }
    }

    private void MakeDamage(GameObject npc, float distance)
    {
        var abc = npc.GetComponentInChildren<HpBar>();
        abc.ChangeHealth(-(1 / (distance * 10)));
    }

    private IEnumerator WaitForKeyPress(KeyCode key)
    {
        while (!Input.GetKeyDown(key))
            yield return null;
        
        spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPoint.z = 0;

        var newObj = new GameObject();
        newObj.AddComponent<SpriteRenderer>();
        newObj.GetComponent<SpriteRenderer>().sprite = bottleImage;
        newObj.GetComponent<SpriteRenderer>().sortingOrder = 10;
        newObj.transform.position = spawnPoint;
        buttonPressed = true;
    }
}