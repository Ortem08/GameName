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
    private bool prepareSoundIsDone;
    private System.Random random;
    private AudioSource audioSource;
    private GameObject player;
    private bool flag;

    private static System.Timers.Timer aTimer;

    void Start()
    {
        flag = true;
        player = GameObject.FindGameObjectWithTag("Player");
        prepareSoundIsDone = false;
        random = new System.Random();

        startPosition = player.transform.position;
        direction = player.GetComponent<NavMeshAgent>().destination - startPosition;
        spawnPoint = startPosition + direction.normalized * 3;

        currentChest = new GameObject();
        currentChest.AddComponent<SpriteRenderer>();
        currentChest.GetComponent<SpriteRenderer>().sprite = CasketImage;
        currentChest.GetComponent<SpriteRenderer>().sortingOrder = -1;
        currentChest.transform.position = spawnPoint;

        npcs = GameObject.FindGameObjectsWithTag("NPC");

        aTimer = new System.Timers.Timer();
        aTimer.Interval = 10000;
        aTimer.AutoReset = false;
        aTimer.Enabled = false;

        MakePrepareSound();
    }

    void Update()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            prepareSoundIsDone = true;
            player.GetComponent<PlayerControl>().BlockedByAnotherScript = false;
        }

        if (prepareSoundIsDone && flag)
        {
            currentChest.GetComponent<SpriteRenderer>().sortingOrder = 10;
            aTimer.Enabled = true;
            flag = false;

            var casketSong = Resources.Load<AudioClip>("CascetSong" + random.Next(1, 8));
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = casketSong;
            //audioSource.volume = 0.02f;
            audioSource.loop = false;
            audioSource.Play();
            currentChest.AddComponent<AudioVolumeDistance>().source = audioSource;
        }

        if (!audioSource.isPlaying && prepareSoundIsDone)
        {
            KillCasketScript();
        }

        else if (audioSource.isPlaying && prepareSoundIsDone)
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
    }

    private void MakePrepareSound()
    {
        var casketPrepareSound = Resources.Load<AudioClip>("CascetPrepare" + random.Next(1, 4));
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = casketPrepareSound;
        //audioSource.volume = 0.02f;
        audioSource.loop = false;
        audioSource.Play();
        player.GetComponent<PlayerControl>().BlockedByAnotherScript = true;
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
