using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;

    private int i = 0;
    public bool isPause;

    private float timeLeft = 5f;
    private float nextTime = 0.0f;
    private bool textfinish = false;

    // �� ��
    public GameObject[] enemylist;
    public int enemy1_num;
    public int enemy2_num;
    public int enemy3_num;

    //UI
    public Text enemy1_numT;
    public Text enemy2_numT;
    public Text enemy3_numT;

    public bool text1finished;
    public bool text2finished;
    public bool text3finished;

    //��
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;

    public Sprite[] doorChecks;

    public GameObject[] elevators;
    public GameObject GameOverP;
    public GameObject PauseP;
    public GameObject EnemyListP;
    public GameObject NPCPanel;
    public GameObject MissionClearText;

    public bool isMinigamePlaying;
    public bool isMissionClear;
    public bool isKilledEnemy;
    public bool isClear;

    private bool isMissionText1;
    private bool isMissionText2;
    private bool isMissionText3;

    public GameObject mini1;
    public GameObject mini2;
    public GameObject mini3;
    public GameObject mini4;
    public GameObject mini5;


    //�������� �� �̼ǵ�
    public bool mission1;   //�̴ϰ���1
    public bool mission2;   //�̴ϰ���2
    public bool mission3;   //�̴ϰ���3
    public bool mission4;   //�̴ϰ���4

    //�������� �� �̼ǵ�
    public bool mission5;   //���������� �ִ� �� ��� ���̱�

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //�� �� �޾ƿ���
        enemy1_num = 0;
        enemy2_num = 0;
        enemy3_num = 0;

        var enemys = FindObjectsOfType<Enemy>();

        if (enemys != null)
        {
            for(int i = 0; i< enemys.Length; i++)
            {
                if (enemys[i].GetComponent<Enemy>().enemyType == Enemy.Type.A)
                {
                    enemy1_num++;
                }
                else if (enemys[i].GetComponent<Enemy>().enemyType == Enemy.Type.B)
                {
                    enemy2_num++;
                }
                else if (enemys[i].GetComponent<Enemy>().enemyType == Enemy.Type.C)
                {
                    enemy3_num++;
                }
            }
            enemy1_numT.text = "X " + enemy1_num;
            enemy2_numT.text = "X " + enemy2_num;
            enemy3_numT.text = "X " + enemy3_num;
        }
        

        //�⺻ ����
        isPause = false;
        isMissionClear = false;
        isMissionText1 = false;
        isMissionText2 = false;
        isMissionText3 = false;
        isMinigamePlaying = false;
        isClear = false;
        text1finished = false;

        GameStart();
    }

    public void GameStart()
    {
        //���� �� ��ҿ� ���� �ð��� �귯������ ��
        Time.timeScale = 1;
        //���ǽ��� ���� ���
        NPCPanel.SetActive(true);

        //Ŀ�� �ᱸ��
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        npcPanelCtrl();
        Pause();
        miniGamePlaying();
        CursurLock();
        enemyNumSetting();

        if (mission2)
        {
            //�� ���� �Ǻ���
            EnemyListP.SetActive(true);
        }

        //������ ����
        if(mission1 && mission2)
        {
            door1.GetComponent<SpriteRenderer>().enabled = false;
        }

        //�������� 2 ����
        if (mission1 && mission2 && mission3 && mission4)
        {
            isMissionClear = true;
            door2.GetComponent<SpriteRenderer>().enabled = false;
        }

        //���� ���� �������� �� ���� ����
        if (isKilledEnemy)
        {
            door3.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (NPCPanel.activeSelf)
        {
            text1finished = false;
        }
        else
        {
            text1finished = true;
        }
    }

    // �� �� ����
    void enemyNumSetting()
    {
        //�� �� �޾ƿ���
        enemy1_num = 0;
        enemy2_num = 0;
        enemy3_num = 0;

        var enemys = FindObjectsOfType<Enemy>();

        if (enemys != null)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                if (enemys[i].GetComponent<Enemy>().enemyType == Enemy.Type.A)
                {
                    enemy1_num++;
                }
                else if (enemys[i].GetComponent<Enemy>().enemyType == Enemy.Type.B)
                {
                    enemy2_num++;
                }
                else if (enemys[i].GetComponent<Enemy>().enemyType == Enemy.Type.C)
                {
                    enemy3_num++;
                }
            }
            enemy1_numT.text = "X " + enemy1_num;
            enemy2_numT.text = "X " + enemy2_num;
            enemy3_numT.text = "X " + enemy3_num;
        }

        if(enemy1_num == 0 && enemy2_num == 0 && enemy3_num == 0)
        {
            isKilledEnemy = true;
        }
    }


    // NPC �г� ��Ʈ��
    void npcPanelCtrl()
    {

        // 1��
        if (NPCPanel.GetComponent<TextTyping>().textFinish1 == true)
        {
            NPCPanel.SetActive(false);

            if (isMissionClear)
            {
                NPCPanel.GetComponent<TextTyping>().textFinish1 = false;
                NPCPanel.SetActive(true);
                NPCPanel.GetComponent<TextTyping>().secondMessage();
            }
        }

        //2��
        if (NPCPanel.GetComponent<TextTyping>().textFinish2 == true)
        {
            NPCPanel.SetActive(false);

            if (isKilledEnemy)
            {
                NPCPanel.GetComponent<TextTyping>().textFinish2 = false;
                NPCPanel.SetActive(true);
                NPCPanel.GetComponent<TextTyping>().thirdMessage();
            }
        }

        //3��
        if (NPCPanel.GetComponent<TextTyping>().textFinish3 == true)
        {
            NPCPanel.SetActive(false);
            text3finished = true;

            if (isClear)
            {
                NPCPanel.GetComponent<TextTyping>().textFinish3 = false;
                NPCPanel.SetActive(true);
                NPCPanel.GetComponent<TextTyping>().fourthMessage();
            }
        }

        //4��
        if (NPCPanel.GetComponent<TextTyping>().textFinish4 == true)
        {
            NPCPanel.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        if (!isMissionText1 && mission1 && mission2)
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + timeLeft;
                StartCoroutine(showText1("�̼� Ŭ���� : ������ ����"));
            }
        }

        if(!isMissionText2 && isMissionClear)
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + timeLeft;
                StartCoroutine(showText2("�̼� Ŭ���� : ����� ����"));
            }
        }

        if (!isMissionText3 && isKilledEnemy)
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + timeLeft;
                StartCoroutine(showText3("�̼� Ŭ���� : ���ؽ� ����"));
            }
        }
    }

    IEnumerator showText1(string t)
    {
        MissionClearText.GetComponent<Text>().text = t;
        MissionClearText.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(false);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(false);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(false);

        isMissionText1 = true;
    }

    IEnumerator showText2(string t)
    {
        MissionClearText.GetComponent<Text>().text = t;
        MissionClearText.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(false);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(false);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(false);

        isMissionText2 = true;
    }

    IEnumerator showText3(string t)
    {
        MissionClearText.GetComponent<Text>().text = t;
        MissionClearText.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(false);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(false);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(true);
        yield return new WaitForSeconds(1f);
        MissionClearText.SetActive(false);

        isMissionText3 = true;
    }

    public void miniGamePlaying()
    {
        //�̴ϰ��� Ȱ��ȭ �˻�
        if (mini1.activeSelf || mini2.activeSelf || mini3.activeSelf || mini4.activeSelf || mini5.activeSelf )
        {
            isMinigamePlaying = true;
        }
        else
        {
            isMinigamePlaying = false;
        }

    }

    public void CursurLock()
    {
        //�̴ϰ��� Ŀ�� Ǯ��
        if (mini1.activeSelf || mini2.activeSelf || mini3.activeSelf || mini4.activeSelf || mini5.activeSelf || PauseP.activeSelf || GameOverP.activeSelf)
        {
            //Ŀ�� Ǯ��
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            //Ŀ�� �ᱸ��
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }



    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause == false)
            {
                Time.timeScale = 0;
                isPause = true;

                PauseP.SetActive(true);

                return;
            }
            else
            {
                Time.timeScale = 1;
                isPause = false;

                PauseP.SetActive(false);

                return;
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverP.SetActive(true);

        //Ŀ�� Ǯ��
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }




    //��ư Ŭ�� �Լ���
    public void RestartGame()
    {
        audioSource.PlayOneShot(audioClip);
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(audioClip);
        Application.Quit();
    }

    public void GoMain()
    {
        audioSource.PlayOneShot(audioClip);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadEnding()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
