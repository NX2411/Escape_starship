using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class testCollider : MonoBehaviour
{
    public AudioClip audio;
    private AudioSource audioSource;
    public gamemanager gm;
    public GameObject image;
    public GameObject finishText;
    public GameObject miniGameP;

    public GameObject miniGame1;
    public GameObject miniGame2;
    public GameObject miniGame3;
    public GameObject miniGame4;
    public GameObject miniGame5;

    public Text miniGame1Title;
    public Text miniGame2Title;
    public Text miniGame3Title;
    public Text miniGame5Title;

    miniGame1 GetMiniGame1;
    miniGame2 GetMiniGame2;
    miniGame3 GetMiniGame3;
    miniGame4 GetMiniGame4;
    miniGame5 GetMiniGame5;

    private bool isPlayerStay;
    private bool isMiniGame1;

    private bool isMiniGame1Finish;
    private bool isMiniGame2Finish;
    private bool isMiniGame3Finish;
    private bool isMiniGame4Finish;
    private bool isMiniGame5Finish;

    private bool isPlayingAudio;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (miniGame1 != null) GetMiniGame1 = miniGame1.GetComponent<miniGame1>();
        if (miniGame2 != null) GetMiniGame2 = miniGame2.GetComponent<miniGame2>();
        if (miniGame3 != null) GetMiniGame3 = miniGame3.GetComponent<miniGame3>();
        if (miniGame4 != null) GetMiniGame4 = miniGame4.GetComponent<miniGame4>();
        if (miniGame5 != null) GetMiniGame5 = miniGame5.GetComponent<miniGame5>();

        isMiniGame1Finish = false;
        isMiniGame2Finish = false;
        isMiniGame3Finish = false;
        isMiniGame4Finish = false;
        isMiniGame5Finish = false;
    }

    void Update()
    {
        if (miniGame1 != null)
            game1();
        if (miniGame2 != null)
            game2();
        if (miniGame3 != null)
            game3();
        if (miniGame4 != null)
            game4();
        if (miniGame5 != null)
            game5();
    }

    void playAudio()
    {
        if (!isPlayingAudio)
        {
            audioSource.PlayOneShot(audio);
        }
        isPlayingAudio = true;
    }

    void game1()
    {
        // 미니게임 1번
        if (!miniGame1.activeSelf)
        {
            gm.isMinigamePlaying = false;
            if (Input.GetKeyDown(KeyCode.V) && isPlayerStay && !isMiniGame1Finish)
            {
                miniGame1.SetActive(true);
                
            }
        }
        else if (miniGame1.activeSelf)
        {
            gm.isMinigamePlaying = true;
            image.SetActive(false);
            if (GetMiniGame1.isFinish)
            {
                playAudio();
                StartCoroutine(endMinigame1());
            }
                
        }
    }

    void game2()
    {
        // 미니게임 2번
        if (!miniGame2.activeSelf)
        {
            gm.isMinigamePlaying = false;
            if (Input.GetKeyDown(KeyCode.V) && isPlayerStay && !isMiniGame2Finish)
            {
                miniGame2.SetActive(true);
            }

        }
        else if (miniGame2.activeSelf)
        {
            gm.isMinigamePlaying = true;
            image.SetActive(false);
            if (GetMiniGame2.isEnd)
            {
                playAudio();
                StartCoroutine(endMinigame2());
            }
                
        }

    }

    void game3()
    {
        // 미니게임 3번
        if (!miniGame3.activeSelf)
        {
            gm.isMinigamePlaying = false;
            if (Input.GetKeyDown(KeyCode.V) && isPlayerStay && !isMiniGame3Finish)
            {
                miniGame3.SetActive(true);
            }
        }
        else if (miniGame3.activeSelf)
        {
            gm.isMinigamePlaying = true;
            image.SetActive(false);
            if (GetMiniGame3.isFinish)
            {
                playAudio();
                StartCoroutine(endMinigame3());
            }
                
        }
    }

    void game4()
    {
        // 미니게임 4번
        if (!miniGame4.activeSelf)
        {
            gm.isMinigamePlaying = false;
            if (Input.GetKeyDown(KeyCode.V) && isPlayerStay && !isMiniGame4Finish)
            {
                miniGame4.SetActive(true);
            }

        }
        else if (miniGame4.activeSelf)
        {
            gm.isMinigamePlaying = true;
            image.SetActive(false);
            if (GetMiniGame4.isFinish)
            {
                playAudio();
                StartCoroutine(endMinigame4());
            }
                
        }
    }

    void game5()
    {
        // 미니게임 5번
        if (!miniGame5.activeSelf)
        {
            gm.isMinigamePlaying = false;
            if (Input.GetKeyDown(KeyCode.V) && isPlayerStay && !isMiniGame5Finish)
            {
                miniGame5.SetActive(true);
            }

        }
        else if (miniGame5.activeSelf)
        {
            gm.isMinigamePlaying = true;
            image.SetActive(false);
            if (GetMiniGame5.isFinish)
                StartCoroutine(endMinigame5());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && (!isMiniGame1Finish || !isMiniGame2Finish || !isMiniGame3Finish || !isMiniGame4Finish))
        {
            image.SetActive(true);
            isPlayerStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            image.SetActive(false);
            isPlayerStay = false;
        }
    }

    IEnumerator endMinigame1()
    {
        miniGame1Title.text = "시스템 가동 성공";
        yield return new WaitForSeconds(3f);
        miniGame1.SetActive(false);
        isMiniGame1Finish = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        gm.mission1 = true;
        gm.isMinigamePlaying = false;
    }

    IEnumerator endMinigame2()
    {
        miniGame2Title.text = "몬스터 판별기 재설정 완료";
        yield return new WaitForSeconds(3f);
        miniGame2.SetActive(false);
        isMiniGame2Finish = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        gm.mission2 = true;
        gm.isMinigamePlaying = false;
    }

    IEnumerator endMinigame3()
    {
        miniGame3Title.text = "항로 설정 성공";
        yield return new WaitForSeconds(3f);
        miniGame3.SetActive(false);
        isMiniGame3Finish = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        gm.mission3 = true;
        gm.isMinigamePlaying = false;
    }

    IEnumerator endMinigame4()
    {
        yield return new WaitForSeconds(3f);
        miniGame4.SetActive(false);
        isMiniGame4Finish = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        gm.mission4 = true;
        gm.isMinigamePlaying = false;
    }

    IEnumerator endMinigame5()
    {
        miniGame5Title.text = "스타쉽 스페이스쉽\n항해 경로 설정 완료\n\n목적지로 출발하겠습니다!";
        yield return new WaitForSeconds(3f);
        miniGame5.SetActive(false);
        isMiniGame5Finish = true;
        gm.isMinigamePlaying = false;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Ending");
    }

}
