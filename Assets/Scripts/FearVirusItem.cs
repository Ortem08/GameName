using System.Collections;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class FearVirusItem : MonoBehaviour
{
    public float radius = 5f;
    public float rippleInterval = 0.5f;
    public int maxRippleCount = 6;
    public float infectionDuration = 3f;

    private int currentRippleCount = 0;
    private bool isVirusActive = false;
    private GameObject infectedNPC;
    private GameObject[] npcs;

    void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }

    void Update()
    {
        if (isVirusActive || Input.GetMouseButtonDown(1))
        {
            var spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPoint.z = 0;
            var obj = new GameObject();
            obj.AddComponent<CircleCollider2D>();
            obj.transform.position = spawnPoint;
            obj.AddComponent<Rigidbody2D>();
            var rigidbody = obj.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;

            foreach (var npc in npcs)
            {
                var a = npc.transform.position;
                var b = obj.transform.position;
                Debug.Log(b.ToString() + "......" + a.ToString());

                if (rigidbody.IsTouching(npc.GetComponent<CircleCollider2D>()))
                {
                    Debug.Log("Touch");
                    if (npc != null)
                    {
                        SprayVirus(npc.GetComponent<NPCMove>(), npc);
                    }
                }
            }
        }
    }

    public void ActivateVirus()
    {
        isVirusActive = true;
        StartCoroutine(VirusDurationCoroutine());
    }

    private IEnumerator VirusDurationCoroutine()
    {
        yield return new WaitForSeconds(infectionDuration);
        isVirusActive = false;
        currentRippleCount = 0;
        infectedNPC = null;
    }

    private void SprayVirus(NPCMove npcmove, GameObject npc)
    {
        if (npc == infectedNPC)
        {
            npcmove.UpdateInfectionEffect();
        }
        else
        {
            infectedNPC = npc;
            currentRippleCount = 0;

            StartCoroutine(RippleEffectCoroutine(npcmove.GetComponent<NPCMove>(), npc));
        }
    }

    private IEnumerator RippleEffectCoroutine(NPCMove npcmove, GameObject npc)
    {
        while (currentRippleCount < maxRippleCount)
        {
            yield return new WaitForSeconds(rippleInterval);

            if (!npcmove.IsInfected)
            {
                MakeDamage(npc, radius);
                currentRippleCount++;

                if (currentRippleCount == maxRippleCount)
                {
                    npcmove.Infect();
                }
            }
        }
    }

    private void MakeDamage(GameObject npc, float distance)
    {
        var abc = npc.GetComponentInChildren<HpBar>();
        abc.ChangeHealth(30);
        StartCoroutine(WaitFor(5));
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}