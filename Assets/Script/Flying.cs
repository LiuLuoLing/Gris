using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    private bool moveDir;
    private float moveSpeed;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private SpriteRenderer sr;

    void Start()
    {
        moveSpeed = 4f;
        sr = GetComponent<SpriteRenderer>();
        startPoint = GameObject.Find("StartFlyingPoint").transform.localPosition;
        endPoint = GameObject.Find("EndFlyingPoint").transform.localPosition;
    }

    void Update()
    {
        if (moveDir)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, startPoint, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.localPosition, startPoint) < 0.1f)
            {
                moveDir = false;
                sr.flipX = false;
            }
        }
        else
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, endPoint, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.localPosition, endPoint) < 0.1f)
            {
                moveDir = true;
                sr.flipX = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Gris")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "Gris")
        {
            collision.transform.SetParent(null);
        }
    }
}
