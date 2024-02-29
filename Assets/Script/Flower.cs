using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private bool bloom;
    private Animator animator;
    private GameObject tearItemGo;
    private AudioClip audioClip;

    void Start()
    {
        animator = GetComponent<Animator>();
        tearItemGo = Resources.Load<GameObject>("Prefabs/TearItem1");
        audioClip = Resources.Load<AudioClip>("Gris/Audioclips/Bloom");
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Song")
        {
            if (!bloom)
            {
                animator.Play("Bloom");
                bloom = true;
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
                Invoke("CreateTearItem", 1.5f);
            }
        }
    }

    private void CreateTearItem()
    {
        GameObject tearGO = Instantiate(tearItemGo, transform.position + transform.up * 0.6f, Quaternion.identity);
        tearGO.name = gameObject.name;
        this.enabled = false;
    }

}
