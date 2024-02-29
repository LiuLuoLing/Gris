using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtroller : MonoBehaviour
{
    private Vector3 targetPos;
    public bool startPosLerp;
    private float targetSize;
    private bool startSizeLerp;
    private Color targetColor;
    private bool startColorLerp;
    private float lerpSpeed;

    private Transform targetTrans;

    void Start()
    {
        lerpSpeed = 1f;
        targetTrans = GameObject.Find("CameraTargetPos").transform;
    }

    void FixedUpdate()
    {
        //λ��
        if (startPosLerp)
        {
            if (Vector3.Distance(transform.position, targetTrans.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, targetTrans.position, lerpSpeed * 20 * Time.fixedDeltaTime);
            }
        }
        //��С
        if (startSizeLerp)
        {
            if (Mathf.Abs(Camera.main.orthographicSize - targetSize) > 0.01f)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, lerpSpeed * Time.fixedDeltaTime);
            }
            else
            {
                startSizeLerp = false;
            }
        }
        //��ɫ
        if (startColorLerp)
        {
            if (!Color.Equals(Camera.main.backgroundColor, targetColor))
            {
                Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, targetColor, lerpSpeed * Time.fixedDeltaTime);
            }
            else
            {
                startColorLerp = false;
            }
        }
    }

    //��С
    public void SetSize(float size)
    {
        startSizeLerp = true;
        targetSize = size;
    }

    //�ı���ɫ
    public void SetColor(Color color)
    {
        startColorLerp = true;
        targetColor = color;
    }
}
