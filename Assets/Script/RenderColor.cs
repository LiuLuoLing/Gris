using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderColor : MonoBehaviour
{
    public RenderData[] renderDatas;
    private bool startChangeBGAlphaCutoff;
    private bool startChangeColorAlpha;
    private bool startChangeAlphaAndScaleValues;

    void Start()
    {
        renderDatas = new RenderData[3];
        for (int i = 0; i < renderDatas.Length; i++)
        {
            renderDatas[i] = new RenderData();
        }

        //��ȡ������ɫ��Ϣ
        GetDatas(transform.GetChild(0), 0);
        //��ȡ�ο���ɫ��Ϣ
        GetDatas(transform.GetChild(1), 1);
        //��ȡ������ɫ��Ϣ
        GetDatas(transform.GetChild(2), 2);
    }

    void Update()
    {
        ChangeBGAlphaCutoff();
        ChangeColorAlpha();
        ChangeAlphaAndScaleValues();
    }

    /// <summary>
    /// �����޸����²���ɫ͸����������ֵ�Ŀ��أ��ⲿ���ã�
    /// </summary>
    public void StartChangeAlphaAndScaleValues()
    {
        startChangeAlphaAndScaleValues = true;
    }

    /// <summary>
    /// �޸��·���ɫ͸�����Լ����Ź���
    /// </summary>
    private void ChangeAlphaAndScaleValues()
    {
        if (startChangeAlphaAndScaleValues)
        {
            for (int i = 0; i < renderDatas[2].srs.Length; i++)
            {
                ChangeAlphaValue(renderDatas[2].srs[i], renderDatas[2].aValues[i]);
                ChangeScaleValue(renderDatas[2].trans[i], renderDatas[2].scales[i]);
            }
        }
    }

    /// <summary>
    /// ����ı�ĳһ�����о�����Ⱦ������Ϸ���������ֵ
    /// </summary>
    /// <param name="t"></param>
    /// <param name="targetValue"></param>
    private void ChangeScaleValue(Transform t, Vector3 targetValue)
    {
        if (t.localScale.x <= targetValue.x)
        {
            t.localScale += Vector3.one * 0.2f * Time.deltaTime;
        }
    }

    /// <summary>
    /// �����޸����ϲ���ɫ͸������͸���Ȳü��Ŀ��أ��ⲿ���ã�
    /// </summary>
    public void StartChangeColorAlpha()
    {
        startChangeColorAlpha = true;
        for (int i = 0; i < renderDatas[1].srs.Length; i++)
        {
            renderDatas[1].trans[i].localScale = renderDatas[1].scales[i];
        }
    }

    /// <summary>
    /// �޸��ϲ���ɫ͸�����Լ�͸���Ȳü��Ĺ���
    /// </summary>
    private void ChangeColorAlpha()
    {
        if (startChangeColorAlpha)
        {
            for (int i = 0; i < renderDatas[1].sms.Length; i++)
            {
                ChangeAlphaCutoffValue(renderDatas[1].sms[i]);
                ChangeAlphaValue(renderDatas[1].srs[i], renderDatas[1].aValues[i]);
            }
        }
    }

    /// <summary>
    /// ����ı�ĳһ��������Ⱦ����͸����ֵ
    /// </summary>
    /// <param name="sr">ָ������Ⱦ��</param>
    /// <param name="targetValue">Ŀ��ֵ</param>
    private void ChangeAlphaValue(SpriteRenderer sr, float targetValue)
    {
        if (sr.color.a <= targetValue)
        {
            sr.color += new Color(0, 0, 0, 0.2f) * Time.deltaTime;
        }
    }

    /// <summary>
    /// �����޸ĵײ���ɫ͸���Ȳü��Ŀ��أ��ⲿ���ã�
    /// </summary>
    public void StartChangeBGAlphaCutoff()
    {
        startChangeBGAlphaCutoff = true;
        for (int i = 0; i < renderDatas[0].srs.Length; i++)
        {
            renderDatas[0].trans[i].localScale = renderDatas[0].scales[i];
            renderDatas[0].srs[i].color += new Color(0, 0, 0, 1);
        }
    }

    /// <summary>
    /// �޸ĵײ㱳����ɫ͸���Ȳü�����
    /// </summary>
    private void ChangeBGAlphaCutoff()
    {
        if (startChangeBGAlphaCutoff)
        {
            for (int i = 0; i < renderDatas[0].sms.Length; i++)
            {
                ChangeAlphaCutoffValue(renderDatas[0].sms[i]);
            }
        }
    }

    /// <summary>
    /// ����ı�ĳһ����������͸���Ȳü�ֵ
    /// </summary>
    /// <param name="sm"></param>
    private void ChangeAlphaCutoffValue(SpriteMask sm)
    {
        if (sm.alphaCutoff >= 0.2f)
        {
            sm.alphaCutoff -= 0.08f * Time.deltaTime;
        }
    }

    /// <summary>
    /// ��ȡԭʼ״̬�����ݲ�����Ĭ��ֵ
    /// </summary>
    /// <param name="targetTrans">��Ҫ��¼�����Ӷ�����Ϣ�ĸ���Ϸ�����Transform���</param>
    /// <param name="index">��洢������������λ��</param>
    private void GetDatas(Transform targetTrans, int index)
    {
        //��ȡ������Ⱦ�������Ŀ���Ǵ洢alphaֵ���ں�����alphaֵ���ûؾ�����Ⱦ����
        renderDatas[index].srs = targetTrans.GetComponentsInChildren<SpriteRenderer>();
        int renderDataslength = renderDatas[index].srs.Length;
        renderDatas[index].trans = new Transform[renderDataslength];
        renderDatas[index].scales = new Vector3[renderDataslength];
        renderDatas[index].aValues = new float[renderDataslength];

        for (int i = 0; i < renderDatas[index].srs.Length; i++)
        {
            //�洢����
            //��ȡ�о�����Ⱦ�������Transform,Ŀ���Ǻ������԰�����ֵ���ûض�Ӧ����Ϸ������
            renderDatas[index].trans[i] = renderDatas[index].srs[i].transform;
            //��ȡ�о�����Ⱦ�������Transform�е�����ֵ
            renderDatas[index].scales[i] = renderDatas[index].srs[i].transform.localScale;
            //��ȡ�о�����Ⱦ�������alphaֵ
            renderDatas[index].aValues[i] = renderDatas[index].srs[i].color.a;

            //����Ĭ��ֵ
            renderDatas[index].trans[i].localScale = Vector3.zero;
            renderDatas[index].srs[i].color = new Color(
                renderDatas[index].srs[i].color.r,
                renderDatas[index].srs[i].color.g,
                renderDatas[index].srs[i].color.b, 0);
        }

        renderDatas[index].sms = targetTrans.GetComponentsInChildren<SpriteMask>();
        for (int i = 0; i < renderDatas[index].sms.Length; i++)
        {
            renderDatas[index].sms[i].alphaCutoff = 1;
        }
    }

}

[System.Serializable]
public class RenderData
{
    public SpriteMask[] sms;
    public SpriteRenderer[] srs;
    public Vector3[] scales;
    public float[] aValues;
    public Transform[] trans;
}
