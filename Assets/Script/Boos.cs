using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boos : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigid2D;
    private Transform grisTrans;
    private float speed;
    private Song song;
    private float timerVal;
    private SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        grisTrans = GameObject.Find("Gris").transform;
        speed = 1;
        song = transform.GetChild(0).GetComponent<Song>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timerVal += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        animator.SetFloat("MoveX", rigid2D.velocity.x);
        //×·ÖðGris
        if (rigid2D.velocity.x >= 0)
        {
            sr.flipX = true;
        }
        else if (rigid2D.velocity.x <= 0)
        {
            sr.flipX = false;
        }
        float dis = grisTrans.position.x - transform.position.x;
        int dir;
        if (Mathf.Abs(dis) > (float)10 / 5)
        {

            if (dis > 0)
            {
                dir = 1;
                sr.flipX = true;
            }
            else
            {
                dir = -1;
                sr.flipX = false;
            }
            rigid2D.velocity = Vector2.right * dir * speed;
            animator.SetBool("Sing", false);
            song.SetSing(false);
            timerVal = 0;
        }
        else
        {
            if (timerVal > 0.8f)
            {
                //ÊÍ·Å¹¥»÷
                animator.SetBool("Sing", true);
                song.SetSing(true);
            }
        }
    }
}
