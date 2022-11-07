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

        m_Message1[0] = "\"�ű�, �� ��Ҹ� ���?\"";
        m_Message1[1] = "\"�װ� ���ƿ��� �ʾƼ� �����߾�.\"";
        m_Message1[2] = "\"...\"";
        m_Message1[3] = "\"��, �̷� ���ּ��� ����������.\"";
        m_Message1[4] = "\"���� ������ ħ���� �� ����.\"";
        m_Message1[5] = "\"�� ū ������ ħ���ϱ� ���� Ż���ؾ� �ϴµ�...\"";
        m_Message1[6] = "\"�ϴ� ������ �κ��� ���ľ��� �� ����.\"";
        m_Message1[7] = "\"�̴ϸ��� ���� ������ ���ּ��� ��ġ��!\"";

        StartCoroutine(Typing(m_TypingText, m_Message1, m_Speed, "1"));
        
    }

    public void secondMessage()
    {
        m_Message2 = new string[5];
        m_Message2[0] = "\"���߾�! ���� ���ּ��� �۵���ų �� �ְ� �ƾ�!\"";
        m_Message2[1] = "\"��, �׷��� �����Ƿ� ���� ���� ����.\"";
        m_Message2[2] = "\"���ּ� ���� �ִ� �����鶧���ΰ���...\"";
        m_Message2[3] = "\"�ϴ� �������� ��� �����ľ߰ھ�.\"";
        m_Message2[4] = "\"�������� ��� ����ġ�� �����Ƿ� ���� ���� �����ž�.\"";

        StartCoroutine(Typing(m_TypingText, m_Message2, m_Speed, "2"));

    }

    public void thirdMessage()
    {
        m_Message3 = new string[5];
        m_Message3[0] = "\"���� ���Ⱦ�!\"";
        m_Message3[1] = "\"��, �Ʊ� ħ���� ���������� ���� �ý����� �۵��߳���\"";
        m_Message3[2] = "\"�����Ƿ� ���� �濡 ������ �ܶ� ��ȳ�...\"";
        m_Message3[3] = "\"�ð��� �� ��� ������ ���ذ��� �� �ۿ� ���ھ�.\"";
        m_Message3[4] = "\"������ ���ؼ� �����Ǳ��� �޸���!\"";

        StartCoroutine(Typing(m_TypingText, m_Message3, m_Speed, "3"));
    }

    public void fourthMessage()
    {
        m_Message4 = new string[2];
        m_Message4[0] = "\"�����߾�!\"";
        m_Message4[1] = "\"�� ���� �߻� ��ư�� ������ Ż������!\"";

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
