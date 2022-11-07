using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniGame4 : MonoBehaviour
{
    public Slider slTimer;
    public Text percentText;
    public float fSliderBarTime;
    public bool isFinish;

    void Start()
    {
        isFinish = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && slTimer.value < 100.0f)
        {
            slTimer.value += 2.5f;
            
            if(slTimer.value == 100.0f)
            {
                isFinish = true;
                percentText.text = "�ý��� ���� �ٿ�ε� : " + slTimer.value + "%";
            }
        }

        if (slTimer.value > 0.0f && !isFinish)
        {
            // �ð��� ������ ��ŭ slider Value ������ �մϴ�.
            slTimer.value -= Time.deltaTime * 10f;
        }

        if(!isFinish)
            percentText.text = "�ý��� ���� �ٿ�ε� : " + Mathf.Round(slTimer.value) + "%";
    }

    public void Close()
    {
        gameObject.SetActive(false);
        
    }
}
