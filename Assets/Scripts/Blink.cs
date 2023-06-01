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
        Picture.color = Color.Lerp(Color.black, Color.white, Mathf.Abs(Mathf.Sin(Time.time)) * 2);
    }
}
