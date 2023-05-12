using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class CasketScript : MonoBehaviour
{
    public Sprite CasketImage;
    public GameObject CasketScriptHolder;
    private GameObject currentChest;

    private Vector3 startPosition;
    private Vector3 direction;
    private Vector3 spawnPoint;
    private GameObject[] npcs;

    private static System.Timers.Timer aTimer;

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

        aTimer = new System.Timers.Timer();
        aTimer.Interval = 2000;
        aTimer.AutoReset = false;
        aTimer.Enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (aTimer.Enabled == false)
        {
            KillCasketScript();
        }

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

    private void KillCasketScript()
    {
        Destroy(currentChest);
        Destroy(CasketScriptHolder);
    }

    private void MakeDamage(GameObject npc, float distance)
    {
        var abc = npc.GetComponentInChildren<HpBar>();
        abc.ChangeHealth(-(1 / (distance / 100)));
        StartCoroutine(WaitFor(5));
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
