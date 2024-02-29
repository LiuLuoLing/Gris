using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPlace : MonoBehaviour
{
    private Transform grisTrans;
    private AudioClip audioClipNormal;
    private AudioClip audioClipJudge;
    private AudioClip audioClipToNextLeve1;
    private AudioSource audioSource;
    private AsyncOperation ao;
    private CameraCtroller cc;
    private RenderColor rc;
    private Gris gris;
    private ToNextLevelScript levelScript;
    private GameObject birds;
    private CameraPosMove cmp;

    private void Start()
    {
        grisTrans = GameObject.Find("Gris").transform;
        audioClipNormal = Resources.Load<AudioClip>("Gris/Audioclips/BG2");
        audioClipJudge = Resources.Load<AudioClip>("Gris/Audioclips/BG3");
        audioClipToNextLeve1 = Resources.Load<AudioClip>("Gris/Audioclips/BG4");
        audioSource = GameObject.Find("Evn").GetComponent<AudioSource>();
        cc = Camera.main.GetComponent<CameraCtroller>();
        rc = GameObject.Find("RenderColors").GetComponent<RenderColor>();
        gris = grisTrans.GetComponent<Gris>();
        levelScript = grisTrans.GetComponent<ToNextLevelScript>();
        ao = SceneManager.LoadSceneAsync(1);
        ao.allowSceneActivation = false;
        birds = Resources.Load<GameObject>("Prefabs/Birds");
        cmp = GameObject.Find("CameraTargetPos").GetComponent<CameraPosMove>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Gris")
        {
            if (audioSource.isPlaying)
            {
                if (JudageTearNumEnough())
                {
                    //ͨ��
                    if (audioSource.clip != audioClipToNextLeve1)
                    {
                        StartCoroutine("ToNextLeve1");
                        //Invoke("PlayNormalClip", 24);
                    }
                }
                else
                {
                    if (audioSource.clip.name != audioClipJudge.name)
                    {
                        audioSource.clip = audioClipJudge;
                        audioSource.loop = false;
                        audioSource.Play();
                        Invoke("PlayNormalClip", 30);
                    }
                }
            }
        }
    }

    private bool JudageTearNumEnough()
    {
        for (int i = 0; i < grisTrans.childCount - 1; i++)
        {
            if (grisTrans.GetChild(i).childCount <= 0)
            {
                //������������������δͨ��
                return false;
            }
        }
        //����ͨ��
        return true;
    }

    private void PlayNormalClip()
    {
        audioSource.clip = audioClipNormal;
        audioSource.loop = true;
        audioSource.Play();
    }

    IEnumerator ToNextLeve1()
    {
        //�첽���صڶ�������
        audioSource.Stop();
        yield return new WaitForSeconds(0.6f);

        //�����ʧȥ��Grisly�Ŀ���Ȩ
        gris.enabled = false;
        levelScript.enabled = true;
        levelScript.SetRigibodyType(RigidbodyType2D.Kinematic);

        //�����л����鶯��״̬1
        levelScript.SetAnimator("Cry");
        yield return new WaitForSeconds(1.167f);

        //�����������У��л����鶯��״̬2
        levelScript.StartMove(new Vector2(328, 3));
        yield return new WaitForSeconds(0.3f);//�ȴ��������� 328,3

        //��������
        audioSource.clip = audioClipToNextLeve1;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(2.7f);

        //����С��
        levelScript.SetAnimator("Fly");
        Instantiate(birds);
        Instantiate(Resources.Load<GameObject>("Prefabs/RedFlowers"));
        cc.SetColor(new Color((float)252 / 255, (float)235 / 255, (float)228 / 255));

        //�����л�����ǰ����Ч������Զ�����
        cmp.SetPos(new Vector3(-8.8f, 4.4f, 10));
        cc.SetSize(10);

        yield return new WaitForSeconds(1);
        rc.StartChangeBGAlphaCutoff();
        yield return new WaitForSeconds(3);
        rc.StartChangeColorAlpha();
        yield return new WaitForSeconds(4);
        rc.StartChangeAlphaAndScaleValues();
        yield return new WaitForSeconds(5);
        //���ؾ�ͷ
        cmp.SetPos(new Vector3(-13.4f, 6.6f, 10));
        cc.SetSize(5);
        yield return new WaitForSeconds(2);

        //�������� 328 -3
        levelScript.StartMove(new Vector2(328, -3));
        yield return new WaitForSeconds(1.6f);
        levelScript.SetAnimator("ToIdle");
        yield return new WaitForSeconds(2f);
        cmp.SetPos(new Vector3(14.6f, 6.8f, 10));
        yield return new WaitForSeconds(2f);
        //�л��ؿ�2
        ao.allowSceneActivation = true;
    }
}
