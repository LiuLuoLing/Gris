using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private GameObject tearsGo;
    private Transform[] roadsTrans;
    private int tearNum;
    [HideInInspector]
    public int stopTearNum;
    private GameObject grisGo;
    private bool startEndScript;
    private AudioSource audioSource;
    private AudioClip audioClip;
    private bool soundVolume;
    private bool bg2Volume;
    private Gris gris;
    private Rigidbody2D rigid;
    //private CameraCtroller cc;
    private CameraPosMove cmp;

    void Start()
    {
        tearsGo = Resources.Load<GameObject>(@"Prefabs/Tear");
        Transform pointsTrans = transform.Find("Points");
        roadsTrans = new Transform[pointsTrans.childCount];
        for (int i = 0; i < roadsTrans.Length; i++)
        {
            roadsTrans[i] = pointsTrans.GetChild(i);
        }
        Invoke("StartCreatingTears", 4.5f);
        grisGo = GameObject.Find("Gris");
        audioSource = GameObject.Find("Evn").GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>(@"Gris/Audioclips/BG2");
        gris = grisGo.GetComponent<Gris>();
        rigid = gris.GetComponent<Rigidbody2D>();
        //cc = Camera.main.GetComponent<CameraCtroller>();
        cmp = GameObject.Find("CameraTargetPos").GetComponent<CameraPosMove>();
        ;
    }

    void Update()
    {
        if (tearNum >= 5)
        {
            CancelInvoke();
            tearNum = 0;
        }
        if (stopTearNum >= 5 && !startEndScript)
        {
            EndScriptOneSet();
            startEndScript = true;
        }
        if (startEndScript)
        {
            if (!soundVolume)
            {
                audioSource.volume -= 0.004f;
                if (audioSource.volume == 0)
                {
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    soundVolume = true;
                }
            }
            if (soundVolume)
            {
                if (!bg2Volume)
                {
                    audioSource.volume += 0.002f;
                    if (audioSource.volume == 1)
                    {
                        bg2Volume = true;
                    }
                }
                if (bg2Volume)
                {
                    Destroy(this);
                }
            }
        }
    }

    void StartCreatingTears()
    {
        InvokeRepeating("CreateTear", 0, 2);
    }

    void CreateTear()
    {
        tearNum++;
        GameObject go = Instantiate(tearsGo, roadsTrans[0].transform.position, Quaternion.identity);
        Tear tear = go.GetComponent<Tear>();
        tear.roadsTrans = roadsTrans;
        tear.finalIndex = roadsTrans.Length - tearNum;
        tear.stone = this;
    }

    private void EndScriptOneSet()
    {
        gris.enabled = true;
        rigid.bodyType = RigidbodyType2D.Dynamic;
        //Camera.main.transform.parent = grisGo.transform;
        cmp.SetPos(new Vector3(-13.4f, 6.75f, 10));
    }
}
