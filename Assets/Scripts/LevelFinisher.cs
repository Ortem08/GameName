using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinisher : MonoBehaviour
{
    public int KillToFinish;

    private GameObject[] npcs;
    private int killed;

    private void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }
    
    private void Update()
    {
        foreach (var npc in npcs)
            if (npc.GetComponentInChildren<HpBar>().CurrentHeath < 5)
                killed++;

        if (killed >= KillToFinish)
            LevelController.Instance.IsEndGame();
        else
            killed = 0;
    }
}
