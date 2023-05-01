using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHeath;
    public float AnimationSpeed = 8f;

    public RectTransform greenBar;
    public RectTransform whiteBar;
    private float fullWidth;
    private Coroutine adjustBarWidthCoroutine;
    private float TargetWidth => CurrentHeath * fullWidth / MaxHealth;

    private void Start()
    {
        fullWidth = greenBar.rect.width;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeHealth(20);
        }

        if (Input.GetMouseButtonDown(1))
        {
            ChangeHealth(-20);
        }

    }
    public void ChangeHealth(int value)
    {
        CurrentHeath = Math.Clamp(CurrentHeath + value, 0, MaxHealth);
        if (adjustBarWidthCoroutine != null)
        {
            StopCoroutine(adjustBarWidthCoroutine);
        }

        adjustBarWidthCoroutine = StartCoroutine(AdjustBarWidth(value));
    }

    private IEnumerator AdjustBarWidth(int value)
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
}
