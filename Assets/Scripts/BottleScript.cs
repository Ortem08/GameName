using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BottleScript : MonoBehaviour
{

    public static System.Timers.Timer aTimer;

    [SerializeField] public Sprite BottleImage;
    [SerializeField] public GameObject BottleScriptHolder;
    [SerializeField] public GameObject Player;
    [SerializeField] public RuntimeAnimatorController BottleAnimationController;

    public bool IsAnimationFinished { get; set; }
    public bool IsAnimationStarted { get; private set; }

    private GameObject bottle;
    private bool buttonPressed;
    private Vector3 spawnPoint;
    private GameObject[] npcs;
    private bool coroutineStarts;
    private bool itemIsDestroyed;
    private Animator animator;
    private float slowDistance = 3;
    private bool IsGrowthStarted { get; set; }
    private bool IsGrowthEnded { get; set; }
    private bool IsBurstStarted { get; set; }

    void Start()
    {
        coroutineStarts = false;
        itemIsDestroyed = false;
        IsAnimationFinished = false;

        aTimer = new System.Timers.Timer();
        aTimer.Interval = 10000;
        aTimer.AutoReset = false;
        aTimer.Enabled = false;

        StartCoroutine(WaitForKeyPress(KeyCode.Mouse1));
        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }

    void Update()
    {
        if (!aTimer.Enabled && coroutineStarts)
        {
            coroutineStarts = false;
            KillBottleScript();
        }

        if (!IsBurstStarted && IsGrowthEnded)
            animator.Play("PuddleBurst");

        if (buttonPressed && IsAnimationFinished)
        {
            if (!IsGrowthStarted)
                StartBreakAnimation();
            coroutineStarts = true;
            aTimer.Enabled = true;
            StartCoroutine(LookAround());
            if (Mathf.Abs(animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 1f) < 0.001)
                IsGrowthEnded = true;
            //Destroy(gameObject);
            //Destroy(bottle);
        }
    }

    public void KillBottleScript()
    {
        Destroy(bottle);
        Destroy(BottleScriptHolder);
        itemIsDestroyed = true;
        UnfreezeNPCs();
    }

    private IEnumerator LookAround()
    {
        foreach (var npc in npcs)
        {
            if (npc != null)
            {
                var distance = Mathf.Abs((npc.transform.position - spawnPoint).magnitude);
                if (distance < slowDistance && !itemIsDestroyed)
                    SlowDown(npc);
            }

            //if (itemIsDestroyed)
            //{
            //    npc.GetComponent<NavMeshAgent>().speed = 3.5f;
            //}

            yield return null;
        }
    }

    private void StartBreakAnimation()
    {
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 10;
        gameObject.transform.localScale = new Vector2(slowDistance * 5.3f, slowDistance * 5.3f);
        transform.position = spawnPoint;
        IsGrowthStarted = true;
        animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/PF Village Props - Wine Bottle");
        animator.Play("PuddleGrowth");
    }

    private void UnfreezeNPCs()
    {
        foreach (var npc in npcs)
        {
            var distance = Mathf.Abs((npc.transform.position - spawnPoint).magnitude);
            if (distance <= slowDistance && itemIsDestroyed)
                npc.GetComponent<NavMeshAgent>().speed = 3.5f;
        }
    }

    private void SlowDown(GameObject npc)
    {
        //var wasShocked = false;
        //var rnd = new System.Random();
        var abc = npc.GetComponent<NavMeshAgent>();
        abc.speed = 0.5f;
        //Debug.Log("npcdofk");
        //if (rnd.NextDouble() % 157 < 1e-4)
        //{
        //    Debug.Log("shock");
        //    wasShocked = true;
        //    abc.speed = 0;
        //    StartCoroutine(WaitFor(10));
        //    abc.speed = 3.5f;
        //}

        //if(!wasShocked)
        //{
        //    Debug.Log("not shock");
        //    StartCoroutine(WaitFor(10));
        //    abc.speed = 3.5f;
        //}
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(10);
    }

    private IEnumerator WaitForKeyPress(KeyCode key)
    {
        while (!Input.GetKeyDown(key))
            yield return null;
        
        spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPoint.z = 0;

        bottle = new GameObject();
        var spriteRenderer = bottle.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = BottleImage;
        spriteRenderer.sortingOrder = 10;
        bottle.AddComponent<Animator>().runtimeAnimatorController = BottleAnimationController;
        bottle.transform.position = Player.transform.position;
        var bottleAnimationScript = bottle.AddComponent<BottleAnimationScript>();
        bottleAnimationScript.Spawnpoint = spawnPoint;
        bottleAnimationScript.BottleScript = gameObject.GetComponent<BottleScript>();
        IsAnimationStarted = true;

        buttonPressed = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnPoint, slowDistance);
    }
}