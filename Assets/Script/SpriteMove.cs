using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMove : MonoBehaviour
{
    private Transform sp1Trans;
    private float moveSpeed;
    private Animator animator;

    void Start()
    {
        moveSpeed = 1f;
        sp1Trans = GameObject.Find("Script1").transform;
        animator = GetComponent<Animator>();
        animator.Play("ScriptWalk");
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, sp1Trans.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, sp1Trans.position) < 0.01f)
        {
            animator.Play("Idle");
        }
        if (Vector2.Distance(transform.position, sp1Trans.position) < 0.001f)
        {
            this.enabled = false;
        }
    }
}
