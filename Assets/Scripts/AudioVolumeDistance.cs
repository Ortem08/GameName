using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeDistance : MonoBehaviour
{
    private Transform player;
    public AudioSource source;
    private float minVolume = 0.0f;
    private float maxVolume = 1.0f;
    private float maxDistance = 15f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance > maxDistance)
            source.volume = minVolume;
        else if (distance < 0.1f)
            source.volume = maxVolume;
        else
            source.volume = Mathf.Lerp(minVolume, maxVolume, (maxDistance - distance) / maxDistance);
    }
}
