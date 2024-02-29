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

        //获取背景底色信息
        GetDatas(transform.GetChild(0), 0);
        //获取晕开颜色信息
        GetDatas(transform.GetChild(1), 1);
        //获取其他颜色信息
        GetDatas(transform.GetChild(2), 2);
    }

    void Update()
    {
        ChangeBGAlphaCutoff();
        ChangeColorAlpha();
        ChangeAlphaAndScaleValues();
    }

    /// <summary>
    /// 启动修改最下层颜色透明度与缩放值的开关（外部调用）
    /// </summary>
    public void StartChangeAlphaAndScaleValues()
    {
        startChangeAlphaAndScaleValues = true;
    }

    /// <summary>
    /// 修改下放颜色透明度以及缩放功能
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
    /// 具体改变某一个带有精灵渲染器的游戏物体的缩放值
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
    /// 启动修改最上层颜色透明度与透明度裁剪的开关（外部调用）
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
    /// 修改上层颜色透明度以及透明度裁剪的功能
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
    /// 具体改变某一个精灵渲染器中透明度值
    /// </summary>
    /// <param name="sr">指定的渲染器</param>
    /// <param name="targetValue">目标值</param>
    private void ChangeAlphaValue(SpriteRenderer sr, float targetValue)
    {
        if (sr.color.a <= targetValue)
        {
            sr.color += new Color(0, 0, 0, 0.2f) * Time.deltaTime;
        }
    }

    /// <summary>
    /// 启动修改底层颜色透明度裁剪的开关（外部调用）
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
    /// 修改底层背景颜色透明度裁剪功能
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
    /// 具体改变某一个精灵遮罩透明度裁剪值
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
    /// 获取原始状态的数据并设置默认值
    /// </summary>
    /// <param name="targetTrans">想要记录所有子对象信息的跟游戏物体的Transform组件</param>
    /// <param name="index">想存储到的数组索引位置</param>
    private void GetDatas(Transform targetTrans, int index)
    {
        //获取精灵渲染器组件，目的是存储alpha值并在后续吧alpha值设置回精灵渲染器里
        renderDatas[index].srs = targetTrans.GetComponentsInChildren<SpriteRenderer>();
        int renderDataslength = renderDatas[index].srs.Length;
        renderDatas[index].trans = new Transform[renderDataslength];
        renderDatas[index].scales = new Vector3[renderDataslength];
        renderDatas[index].aValues = new float[renderDataslength];

        for (int i = 0; i < renderDatas[index].srs.Length; i++)
        {
            //存储数据
            //获取有精灵渲染器组件的Transform,目的是后续可以把缩放值设置回对应的游戏物体上
            renderDatas[index].trans[i] = renderDatas[index].srs[i].transform;
            //获取有精灵渲染器组件的Transform中的缩放值
            renderDatas[index].scales[i] = renderDatas[index].srs[i].transform.localScale;
            //获取有精灵渲染器组件的alpha值
            renderDatas[index].aValues[i] = renderDatas[index].srs[i].color.a;

            //设置默认值
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
