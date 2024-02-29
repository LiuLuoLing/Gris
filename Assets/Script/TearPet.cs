using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearPet : MonoBehaviour
{
    private Transform targetTrans;
    private float speed;

    void Start()
    {
        Transform grisTrans = GameObject.Find("Gris").transform;
        speed = 3;

        for (int i = 0; i < grisTrans.childCount; i++)
        {
            //Gris下面没有挂子物体，childCount小于等于0的条件就会成立,就给他一个位置
            if (grisTrans.GetChild(i).childCount <= 0)
            {
                targetTrans = grisTrans.GetChild(i);
                GameObject go = new GameObject();
                go.transform.SetParent(targetTrans);
                break;
            }
        }
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, targetTrans.position) > 0.5f)
        {
            PetMove();
        }
    }

    private void PetMove()
    {
        transform.position = Vector2.Lerp(transform.position, targetTrans.position, Time.fixedDeltaTime * speed);
    }
}
