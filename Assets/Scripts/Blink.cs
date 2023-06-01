using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public RawImage Picture;
    private bool ColorIsWhite;

    void Start()
    {
        Picture = gameObject.GetComponent<RawImage>();
    }

    public void Update()
    {
        //if (ColorIsWhite)
        //{
        //    Picture.color = Color.black;
        //    ColorIsWhite = false;
        //}
        //else
        //{
        //    Picture.color = Color.white;
        //    ColorIsWhite = true;
        //}
        Picture.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 0.5f));
    }
}
