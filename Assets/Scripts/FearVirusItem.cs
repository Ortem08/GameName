using System.Collections;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public class FearVirusItem : MonoBehaviour
{
    public float radius = 5f;
    public float rippleInterval = 0.5f;
    public int maxRippleCount = 12;
    public float infectionDuration = 6f;

    private GameObject infectedNPC;
    private GameObject[] npcs;
    private Collider2D[] colliders;

    void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        colliders = new Collider2D[npcs.Length];
        for (var i = 0; i < npcs.Length; i++)
            colliders[i] = npcs[i].GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectVirusArea(Input.mousePosition, true, null);
        }
    }

    private void SelectVirusArea(Vector3 inputSpawnpoint, bool chooseByMouse, GameObject npc)
    {
        var radius = 3;

        var spawnPoint = new Vector3();
        if (chooseByMouse)
            spawnPoint = Camera.main.ScreenToWorldPoint(inputSpawnpoint);
        else
        {
            spawnPoint = inputSpawnpoint;
            radius = 5;
        }
        spawnPoint.z = 0;


        var colliderInCircle = Physics2D.OverlapCircleAll(spawnPoint, radius);
        Debug.Log(colliderInCircle.Length);
        foreach (var collider in colliderInCircle)
        {
            if (collider.gameObject.tag == "NPC" && collider.gameObject != npc)
            {
                SprayVirus(collider);
            }
        }
    }

    private void SprayVirus(Collider2D other)
    {
        Debug.Log("Spray virus on the " + other.ToString());
        Debug.Log(other.GetComponentInChildren<HpBar>());
        other.GetComponentInChildren<HpBar>().isInfected = true;
        HpBar.infectedNPCs.Add(other.gameObject);
        StartCoroutine(VirusDurationCoroutine(other.gameObject));
        StartCoroutine(RippleEffectCoroutine(other.gameObject));
    }

    private IEnumerator VirusDurationCoroutine(GameObject npc)
    {
        yield return new WaitForSeconds(infectionDuration);
        npc.GetComponentInChildren<HpBar>().isInfected = false;
        npc.GetComponentInChildren<HpBar>().currentRippleCount = 0;
        HpBar.infectedNPCs.Remove(npc);
    }

    private IEnumerator RippleEffectCoroutine(GameObject npc)
    {
        while (npc.GetComponentInChildren<HpBar>().currentRippleCount < maxRippleCount && npc.GetComponentInChildren<HpBar>().isInfected)
        {
            yield return new WaitForSeconds(rippleInterval);

            if (npc.GetComponentInChildren<HpBar>().isInfected)
            {
                SelectVirusArea(npc.transform.position, false, npc);
                //npc.GetComponentInChildren<HpBar>().ChangeHealth(-2.5f);
                npc.GetComponentInChildren<HpBar>().currentRippleCount++;

                //if (currentRippleCount == maxRippleCount)
                //{
                //    npc.GetComponentInChildren<HpBar>().isInfected = false;
                //}
            }
        }
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}