using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearItem : MonoBehaviour
{
    private Transform insideCircleTrans;
    private Transform outsideCircleTrans;
    private SpriteRenderer sp;
    private SpriteRenderer tearSr;
    private Sprite sprite;
    public GameObject tearGo;
    private float rotateSpeed;
    private float scaleSpeed;
    private float ColorSpeed;
    private bool ishoxi;
    private bool oneGetTear;
    private AudioClip tearClip;

    void Start()
    {
        rotateSpeed = 90f;
        scaleSpeed = 0.3f;
        ColorSpeed = 1.5f;
        insideCircleTrans = transform.Find("InsideCircle").transform;
        outsideCircleTrans = transform.Find("OutSideCircle").transform;
        sp = outsideCircleTrans.GetComponent<SpriteRenderer>();
        tearSr = GetComponent<SpriteRenderer>();
        sprite = Resources.Load<Sprite>("Gris/Sprites/Item/" + gameObject.name);
        tearGo = Resources.Load<GameObject>("Prefabs/TearPet");
        tearClip = Resources.Load<AudioClip>("Gris/Audioclips/Tear");
    }

    void Update()
    {
        Childs();
        if (oneGetTear)
        {
            tearSr.color -= new Color(0, 0, 0, scaleSpeed) * Time.deltaTime;
            if (tearSr.color.a <= 0)
            {
                Instantiate(tearGo, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Gris" && oneGetTear == false)
        {
            oneGetTear = true;
            tearSr.sprite = sprite;
            tearSr.color = new Color(0, 0, 0);
            AudioSource.PlayClipAtPoint(tearClip, transform.position);
        }
    }

    void Childs()
    {
        if (outsideCircleTrans != null && insideCircleTrans != null)
        {
            insideCircleTrans.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);

            outsideCircleTrans.Rotate(Vector3.forward * Time.deltaTime * (rotateSpeed / 3));
            if (!ishoxi)
            {
                outsideCircleTrans.localScale += scaleSpeed * Time.deltaTime * Vector3.one;
                sp.color += new Color(0, 0, 0, ColorSpeed) * Time.deltaTime;
                if (outsideCircleTrans.localScale.x > 0.7f)
                {
                    ishoxi = true;
                }
            }
            if (ishoxi)
            {
                outsideCircleTrans.localScale -= scaleSpeed * Time.deltaTime * Vector3.one;
                sp.color -= new Color(0, 0, 0, ColorSpeed) * Time.deltaTime;
                if (outsideCircleTrans.localScale.x < 0.3f)
                {
                    ishoxi = false;
                }
            }
            if (oneGetTear)
            {
                Destroy(outsideCircleTrans.gameObject);
                Destroy(insideCircleTrans.gameObject);
            }
        }
    }
}
