using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class TurntableScript : MonoBehaviour
{
    public Sprite TurntableImage;
    public GameObject TurntableScriptHolder;

    [SerializeField] public static float DamageDistance { get; private set; }

    private GameObject currentTable;
    private Vector3 startPosition;
    private Vector3 direction;
    private Vector3 spawnPoint;
    private GameObject[] npcs;
    private bool prepareSoundIsDone;
    private AudioSource audioSource;
    private GameObject player;
    private bool flag = true;
    private bool SecondPrepareStarted { get; set; }

    private AudioMixer Mixer { get; set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        startPosition = player.transform.position;
        direction = player.GetComponent<NavMeshAgent>().destination - startPosition;
        spawnPoint = startPosition + direction.normalized * 3;

        currentTable = new GameObject();
        var turntableSprite = currentTable.AddComponent<SpriteRenderer>();
        turntableSprite.sprite = TurntableImage;
        currentTable.transform.localScale = new Vector3(3, 3, 1); 
        turntableSprite.sortingOrder = -1;
        currentTable.transform.position = spawnPoint;

        npcs = GameObject.FindGameObjectsWithTag("NPC");

        DamageDistance = 5f;
        Mixer = Resources.Load<AudioMixer>("AudioMixer");
        MakeFirstPrepareSound();
    }

    private void Update()
    {
        if (audioSource != null && !audioSource.isPlaying && SecondPrepareStarted)
        {
            prepareSoundIsDone = true;
            player.GetComponent<PlayerControl>().BlockedByAnotherScript = false;
        }

        if (prepareSoundIsDone && flag)
        {
            currentTable.GetComponent<SpriteRenderer>().sortingOrder = 10;
            flag = false;

            var vinilSong = Resources.Load<AudioClip>("Sounds/VinilSong" + Random.Range(1, 8));
            
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
            audioSource.PlayOneShot(vinilSong);
            currentTable.AddComponent<AudioVolumeDistance>().Source = audioSource;
            currentTable.AddComponent<Animator>().runtimeAnimatorController = 
                Resources.Load<RuntimeAnimatorController>("Animation/PF Turntable");
        }   

        if (!audioSource.isPlaying && prepareSoundIsDone)
        {
            KillCasketScript();
        }

        else if (audioSource.isPlaying && prepareSoundIsDone)
        {
            foreach (var npc in npcs)
            {
                if (npc != null)
                {
                    var distance = Mathf.Abs((npc.transform.position - spawnPoint).magnitude);
                    if (distance < DamageDistance)
                        MakeDamage(npc, distance);
                }
            }
        }
    }

    private void MakeFirstPrepareSound()
    {
        var turntablePrepareSound = Resources.Load<AudioClip>("Sounds/VinilPrepareFirst" + Random.Range(1, 2));
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(turntablePrepareSound);
        Invoke(nameof(MakeSecondPrepareSound), turntablePrepareSound.length);
        player.GetComponent<PlayerControl>().BlockedByAnotherScript = true;
    }

    private void MakeSecondPrepareSound()
    {
        var turntablePrepareSound = Resources.Load<AudioClip>("Sounds/VinilPrepareSecond" + Random.Range(1, 3));
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(turntablePrepareSound);
        player.GetComponent<PlayerControl>().BlockedByAnotherScript = true;
        SecondPrepareStarted = true;
    }

    private void KillCasketScript()
    {
        Destroy(currentTable);
        Destroy(TurntableScriptHolder);
    }

    private void MakeDamage(GameObject npc, float distance)
    {
        if (npc.active)
        {
            var abc = npc.GetComponentInChildren<HpBar>();
            abc.ChangeHealth(-10 / distance);
            StartCoroutine(WaitFor(5));
        }
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
