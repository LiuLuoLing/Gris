using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    private float rotateSpeed;
    private float scaleSpeed;
    private bool isSinging;
    private Transform outside;
    private AudioSource audioSource;
    private float audioVolume;
    private bool ismusic;
    private CircleCollider2D circleCollider;

    void Start()
    {
        outside = transform.GetChild(0);
        rotateSpeed = 25;
        scaleSpeed = 3;
        audioSource = GetComponent<AudioSource>();
        audioVolume = 1f;
        circleCollider = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);
        if (isSinging)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.volume = 1;
                audioSource.Play();
                circleCollider.enabled = true;
            }
            if (transform.localScale.x <= 5f)
            {
                transform.localScale += scaleSpeed * Time.deltaTime * Vector3.one;
            }
            else
            {
                outside.gameObject.SetActive(true);
                if (outside.localScale.x <= 1)
                {
                    outside.localScale += scaleSpeed * Time.deltaTime * Vector3.one;
                }
            }
        }
        else
        {
            if (circleCollider.enabled)
            {
                circleCollider.enabled = false;
            }
            if (outside.localScale.x >= 0.83f)
            {
                outside.localScale -= scaleSpeed * 2 * Time.deltaTime * Vector3.one;
            }
            else
            {
                outside.gameObject.SetActive(false);
                if (transform.localScale.x > 0)
                {
                    transform.localScale -= scaleSpeed * 2 * Time.deltaTime * Vector3.one;
                    if (transform.localScale.x <= 0)
                    {
                        transform.localScale = Vector3.zero;
                        ismusic = true;
                    }
                }
            }
        }
        if (ismusic)
        {
            audioSource.volume -= audioVolume * Time.deltaTime;
            if (audioSource.volume <= 0)
            {
                audioSource.Stop();
                ismusic = false;
            }
        }
    }

    public void SetSing(bool state)
    {
        isSinging = state;
    }
}
