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

    // 적 수
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

    //문
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


    //스테이지 원 미션들
    public bool mission1;   //미니게임1
    public bool mission2;   //미니게임2
    public bool mission3;   //미니게임3
    public bool mission4;   //미니게임4

    //스테이지 투 미션들
    public bool mission5;   //스테이지에 있는 적 모두 죽이기

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //적 수 받아오기
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
        

        //기본 세팅
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
        //시작 시 평소와 같이 시간이 흘러가도록 함
        Time.timeScale = 1;
        //엔피시의 설명 듣기
        NPCPanel.SetActive(true);

        //커서 잠구기
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
            //적 몬스터 판별기
            EnemyListP.SetActive(true);
        }

        //관제실 오픈
        if(mission1 && mission2)
        {
            door1.GetComponent<SpriteRenderer>().enabled = false;
        }

        //스테이지 2 오픈
        if (mission1 && mission2 && mission3 && mission4)
        {
            isMissionClear = true;
            door2.GetComponent<SpriteRenderer>().enabled = false;
        }

        //적을 전부 제거했을 시 문이 열림
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

    // 적 수 세팅
    void enemyNumSetting()
    {
        //적 수 받아오기
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


    // NPC 패널 컨트롤
    void npcPanelCtrl()
    {

        // 1번
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

        //2번
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

        //3번
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

        //4번
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
                StartCoroutine(showText1("미션 클리어 : 관제실 오픈"));
            }
        }

        if(!isMissionText2 && isMissionClear)
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + timeLeft;
                StartCoroutine(showText2("미션 클리어 : 제어실 오픈"));
            }
        }

        if (!isMissionText3 && isKilledEnemy)
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + timeLeft;
                StartCoroutine(showText3("미션 클리어 : 항해실 오픈"));
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
        //미니게임 활성화 검사
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
        //미니게임 커서 풀기
        if (mini1.activeSelf || mini2.activeSelf || mini3.activeSelf || mini4.activeSelf || mini5.activeSelf || PauseP.activeSelf || GameOverP.activeSelf)
        {
            //커서 풀기
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            //커서 잠구기
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

        //커서 풀기
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }




    //버튼 클릭 함수들
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
