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
            //玩个不同的地方分散
            if (stone.stopTearNum >= 5)
            {
                transform.Translate(Vector2.right * Time.deltaTime * tearMoveSpeed);
            }
        }
        else
        {
            //朝着一个固定的点移动
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
                #region 我的实现思路
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

                #region 视频的实现思路
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
