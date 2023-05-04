using UnityEngine;
using UnityEngine.AI;

public class CasketScript : MonoBehaviour
{
    public Sprite casketImage;
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

        var newObj = new GameObject();
        newObj.AddComponent<SpriteRenderer>();
        newObj.GetComponent<SpriteRenderer>().sprite = casketImage;
        newObj.GetComponent<SpriteRenderer>().sortingOrder = 10;
        newObj.transform.position = spawnPoint;

        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }

    // Update is called once per frame
    void Update()
    {
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(3, 0, 0));
    }
}
