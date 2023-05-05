using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CasketScript : MonoBehaviour
{
    public Sprite CasketImage;
    private GameObject currentChest;
    private Vector3 startPosition;
    private Vector3 direction;
    private Vector3 spawnPoint;

    private GameObject[] npcs;

    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        startPosition = player.transform.position;
        direction = player.GetComponent<NavMeshAgent>().destination - startPosition;
        spawnPoint = startPosition + direction.normalized * 3;

        currentChest = new GameObject();
        currentChest.AddComponent<SpriteRenderer>();
        currentChest.GetComponent<SpriteRenderer>().sprite = CasketImage;
        currentChest.GetComponent<SpriteRenderer>().sortingOrder = 10;
        currentChest.transform.position = spawnPoint;

        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var bro in npcs)
        {
            var distance = Mathf.Abs((bro.transform.position - spawnPoint).magnitude);
            if (distance < 3)
            {
                MakeDamage(bro, distance);
                //Destroy(gameObject);
                //Destroy(currentChest);
            }
        }
    }

    private void MakeDamage(GameObject npc, float distance)
    {
        var abc = npc.GetComponentInChildren<HpBar>();
        abc.ChangeHealth(-(1 / (distance)));
        StartCoroutine(WaitFor(5));
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
