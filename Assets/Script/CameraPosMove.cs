using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosMove : MonoBehaviour
{
    private Vector3 targetPos;
    private bool startPosLerp;
    private float lerpSpeed;

    void Start()
    {
        lerpSpeed = 0.1f;
    }

    void FixedUpdate()
    {
        //Œª÷√
        if (startPosLerp)
        {
            if (Vector3.Distance(transform.localPosition, targetPos) > 0.1f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, lerpSpeed * 10 * Time.fixedDeltaTime);
            }
            else
            {
                startPosLerp = false;
            }
        }
    }

    //≤Â÷µŒª÷√
    public void SetPos(Vector3 pos)
    {
        startPosLerp = true;
        targetPos = pos;
    }
}
