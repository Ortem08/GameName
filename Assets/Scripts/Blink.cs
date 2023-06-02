using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public RawImage Picture;
    private AudioSource source;
    private AudioClip clip;
    private bool flag;

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("Sounds/Shtirlic");
        source.PlayOneShot(clip);
        StartCoroutine(Screamer());
    }

    private void Update()
    {
        if (flag)
            Picture.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 0.5f));
    }

    public IEnumerator Screamer()
    {
        yield return new WaitForSeconds(clip.length);
        Picture.enabled = true;
        flag = true;
    }
}
