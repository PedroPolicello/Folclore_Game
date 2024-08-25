using System;
using System.Collections;
using UnityEngine;

public class WarningSpikes : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(Warning());
    }

    IEnumerator Warning()
    {
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.red;
        }
        gameObject.SetActive(false);
    }
}
