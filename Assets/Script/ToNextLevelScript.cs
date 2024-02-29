using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextLevelScript : MonoBehaviour
{
    private float speed;
    private Vector2 targetPos;
    private bool startMove;
    private Rigidbody2D rigid;
    private Animator setam;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        speed = 0.02f;
        setam = GameObject.Find("Gris").GetComponent<Animator>();
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().flipX = true;
    }

    void Update()
    {
        if (startMove)
        {
            if (Vector2.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector2.Lerp(transform.position, targetPos, speed);
            }
            else
            {
                startMove = false;
            }
        }
    }

    //设置动画目标位置
    public void StartMove(Vector2 pos)
    {
        startMove = true;
        targetPos = pos;
    }

    public void SetRigibodyType(RigidbodyType2D type2D)
    {
        rigid.bodyType = type2D;
    }

    public void SetAnimator(string animatorname)
    {
        setam.Play(animatorname);
    }
}
