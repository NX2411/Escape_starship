using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTyping : MonoBehaviour
{
    public Text m_TypingText;
    public string[] m_Message1;
    public string[] m_Message2;
    public string[] m_Message3;
    public string[] m_Message4;
    public float m_Speed = 0.1f;
    public bool textFinish1;
    public bool textFinish2;
    public bool textFinish3;
    public bool textFinish4;

    private float timeLeft = 4f;
    private float nextTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

        textFinish1 = false;
        textFinish2 = false;
        textFinish3 = false;
        textFinish4 = false;


        firstMessage();



    }

    void Update()
    {

    }

    public void firstMessage()
    {
        m_Message1 = new string[8];

        m_Message1[0] = "\"거기, 내 목소리 들려?\"";
        m_Message1[1] = "\"네가 돌아오지 않아서 연락했어.\"";
        m_Message1[2] = "\"...\"";
        m_Message1[3] = "\"아, 이런 우주선이 망가졌구나.\"";
        m_Message1[4] = "\"우주 괴물도 침입한 것 같아.\"";
        m_Message1[5] = "\"더 큰 괴물이 침입하기 전에 탈출해야 하는데...\"";
        m_Message1[6] = "\"일단 망가진 부분을 고쳐야할 것 같아.\"";
        m_Message1[7] = "\"미니맵을 보고 망가진 우주선을 고치자!\"";

        StartCoroutine(Typing(m_TypingText, m_Message1, m_Speed, "1"));
        
    }

    public void secondMessage()
    {
        m_Message2 = new string[5];
        m_Message2[0] = "\"잘했어! 이제 우주선을 작동시킬 수 있게 됐어!\"";
        m_Message2[1] = "\"아, 그런데 조종실로 가는 문이 잠겼네.\"";
        m_Message2[2] = "\"우주선 내에 있는 괴물들때문인가봐...\"";
        m_Message2[3] = "\"일단 괴물들을 모두 물리쳐야겠어.\"";
        m_Message2[4] = "\"괴물들을 모두 물리치면 조종실로 가는 문이 열릴거야.\"";

        StartCoroutine(Typing(m_TypingText, m_Message2, m_Speed, "2"));

    }

    public void thirdMessage()
    {
        m_Message3 = new string[5];
        m_Message3[0] = "\"문이 열렸어!\"";
        m_Message3[1] = "\"앗, 아까 침입한 괴물때문에 보안 시스템이 작동했나봐\"";
        m_Message3[2] = "\"조종실로 가는 길에 함정이 잔뜩 깔렸네...\"";
        m_Message3[3] = "\"시간이 얼마 없어서 함정을 피해가는 수 밖에 없겠어.\"";
        m_Message3[4] = "\"함정을 피해서 조종실까지 달리자!\"";

        StartCoroutine(Typing(m_TypingText, m_Message3, m_Speed, "3"));
    }

    public void fourthMessage()
    {
        m_Message4 = new string[2];
        m_Message4[0] = "\"수고했어!\"";
        m_Message4[1] = "\"자 이제 발사 버튼을 눌러서 탈출하자!\"";

        StartCoroutine(Typing(m_TypingText, m_Message4, m_Speed, "4"));
    }

    IEnumerator Typing(Text typingText, string[] message, float speed, string type)
    {
        for (int i = 0; i < message.Length; i++)
        {
            for(int j = 0; j < message[i].Length; j++)
            {
                typingText.text = message[i].Substring(0, j + 1);
                yield return new WaitForSeconds(speed);
            }
            yield return new WaitForSeconds(0.8f);
        }

        if (type == "1")
        {
            textFinish1 = true;
        }
        else if(type == "2")
        {
            textFinish2 = true;
        }
        else if(type == "3")
        {
            textFinish3 = true;
        }
        else if(type == "4")
        {
            textFinish4 = true;
        }
        
    }
}
