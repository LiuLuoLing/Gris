using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private SpriteRenderer sr;
    public bool startLerp;
    public float targetValue;
    private GameObject grisGo;

    void Start()
    {
        sr = GameObject.Find("BlackSKY").GetComponent<SpriteRenderer>();
        grisGo = GameObject.Find("Boss");
        grisGo.SetActive(false);
    }

    void Update()
    {
        if (startLerp)
        {
            if (targetValue > 0)
            {
                sr.color += new Color(0, 0, 0, 0.2f) * Time.deltaTime;
            }
            else
            {
                sr.color -= new Color(0, 0, 0, 0.2f) * Time.deltaTime;
            }

            if (Mathf.Abs(sr.color.a - targetValue) <= 0.05f)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, targetValue);
                if (targetValue >= 1)
                {
                    if (!grisGo.activeInHierarchy)
                    {
                        grisGo.SetActive(true);
                        startLerp = false;
                    }
                }
            }
        }
    }
}
