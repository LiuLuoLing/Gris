using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    [HideInInspector]
    public Transform[] roadsTrans;
    private int index;
    [HideInInspector]
    public int finalIndex;
    public Stone stone;
    private bool notTargetMove;
    private float tearMoveSpeed;

    void Start()
    {
        tearMoveSpeed = 7.5f;
        Destroy(gameObject, 25);
    }

    void Update()
    {
        if (notTargetMove)
        {
            //�����ͬ�ĵط���ɢ
            if (stone.stopTearNum >= 5)
            {
                transform.Translate(Vector2.right * Time.deltaTime * tearMoveSpeed);
            }
        }
        else
        {
            //����һ���̶��ĵ��ƶ�
            if (index == finalIndex)
            {
                if (transform.position == roadsTrans[finalIndex].position)
                {
                    stone.stopTearNum++;
                    notTargetMove = true;
                    transform.Rotate(new Vector3(0, 0, Random.Range(0, 180)));
                    return;
                }
            }
            else
            {
                #region �ҵ�ʵ��˼·
                if (transform.position != roadsTrans[index + 1].position)
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                        roadsTrans[index + 1].position, 1.6f * Time.deltaTime);
                }
                else
                {
                    index++;
                }
                #endregion

                #region ��Ƶ��ʵ��˼·
                //transform.position = Vector2.MoveTowards(transform.position,
                //        roadsTrans[index + 1].position, 0.02f);
                //if (Vector2.Distance(transform.position, roadsTrans[index + 1].position) < 0.01f)
                //{
                //    index++;
                //}
                #endregion

            }
        }
    }
}
