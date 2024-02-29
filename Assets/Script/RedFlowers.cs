using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlowers : MonoBehaviour
{
    private SpriteRenderer[] srs;
    private float scaleSpeed;
    private SpriteRenderer sr;

    void Start()
    {
        srs = GetComponentsInChildren<SpriteRenderer>();
        sr = GameObject.Find("GradientBG").GetComponent<SpriteRenderer>();
        scaleSpeed = 0.5f;
    }

    void Update()
    {
        transform.localScale += scaleSpeed * Time.deltaTime * Vector3.one;
        for (int i = 0; i < srs.Length; i++)
        {
            srs[i].color -= new Color(0, 0, 0, scaleSpeed * 1.3f) * Time.deltaTime;
        }
        sr.color -= new Color(0, 0, 0, scaleSpeed * 2) * Time.deltaTime;
        if (srs[0].color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
