using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public int MaxHealth;
    public float CurrentHeath;
    public float AnimationSpeed = 8f;
    public bool isInfected;
    public static List<GameObject> infectedNPCs;
    public int currentRippleCount;

    public RectTransform greenBar;
    public RectTransform whiteBar;
    private float fullWidth;
    private Coroutine adjustBarWidthCoroutine;
    private float TargetWidth => CurrentHeath * fullWidth / MaxHealth;

    private void Start()
    {
        currentRippleCount = 0;
        isInfected = false;
        infectedNPCs = new List<GameObject>();
        fullWidth = greenBar.rect.width;
    }

    public void ChangeHealth(float value)
    {
        CurrentHeath = Math.Clamp(CurrentHeath + value, 0, MaxHealth);
        if (adjustBarWidthCoroutine != null)
            StopCoroutine(adjustBarWidthCoroutine);

        adjustBarWidthCoroutine = StartCoroutine(AdjustBarWidth(value));
    }

    private IEnumerator AdjustBarWidth(float value)
    {
        var suddenChangeBar = value >= 0 ? whiteBar : greenBar;
        var slowChangeBar = value >= 0 ? greenBar : whiteBar;
        suddenChangeBar.sizeDelta = new Vector2(TargetWidth, suddenChangeBar.rect.height);

        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            slowChangeBar.sizeDelta = new Vector2(Mathf.Lerp(slowChangeBar.rect.width, TargetWidth, Time.deltaTime * AnimationSpeed), slowChangeBar.rect.height);
            yield return null;
        }
        slowChangeBar.sizeDelta = new Vector2(TargetWidth, slowChangeBar.rect.height);
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
