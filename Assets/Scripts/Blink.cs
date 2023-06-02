using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public RawImage Picture;

    void Start()
    {
        Picture = gameObject.GetComponent<RawImage>();
    }

    public void Update()
    {
        Picture.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 0.5f));
    }
}
