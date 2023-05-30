using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeDistance : MonoBehaviour
{
    public AudioSource Source { get; set; }

    private Transform player;
    private float minVolume = 0.0f;
    private float maxVolume = 1.0f;
    private float maxDistance = CasketScript.DamageDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        var distance = Vector2.Distance(player.position, transform.position);

        if (distance > maxDistance)
            Source.volume = minVolume;
        else if (distance < 0.1f)
            Source.volume = maxVolume;
        else
            Source.volume = Mathf.Lerp(minVolume, maxVolume, (maxDistance - distance) / maxDistance);
    }
}
