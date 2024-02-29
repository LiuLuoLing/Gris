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
            //Gris����û�й������壬childCountС�ڵ���0�������ͻ����,�͸���һ��λ��
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
