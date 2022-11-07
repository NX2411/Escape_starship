using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniGame1 : MonoBehaviour
{
    public bool sliderUp1;
    public bool sliderUp2;
    public bool sliderUp3;

    public Slider slider1;
    public Slider slider2;
    public Slider slider3;

    public Slider slider1C;
    public Slider slider2C;
    public Slider slider3C;

    public bool toggleT1;
    public bool toggleT2;
    public bool toggleT3;

    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;

    public bool isFinish;


    // Start is called before the first frame update
    private void Awake()
    {
        sliderUp1 = false;
        sliderUp2 = false;
        sliderUp3 = false;

        toggleT1 = false;
        toggleT2 = false;
        toggleT3 = false;

        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider1.value > 0.5)
        {
            slider1C.enabled = false;
            sliderUp1 = true;
        }
        if (slider2.value > 0.9)
        {
            slider2C.enabled = false;
            sliderUp2 = true;
        }
        if (slider3.value > 0.25)
        {
            slider3C.enabled = false;
            sliderUp3 = true;
        }

        if (toggle1.isOn)
        {
            toggle1.GetComponentInChildren<Text>().text = "Success";
            toggleT1 = true;
            toggle1.enabled = false;
        }
        if (toggle2.isOn)
        {
            toggle2.GetComponentInChildren<Text>().text = "Success";
            toggleT2 = true;
            toggle2.enabled = false;
        }
        if (toggle3.isOn)
        {
            toggle3.GetComponentInChildren<Text>().text = "Success";
            toggleT3 = true;
            toggle3.enabled = false;
        }

        if(sliderUp1 && sliderUp2 && sliderUp3 && toggleT1 && toggleT2 && toggleT3)
        {
            isFinish = true;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
