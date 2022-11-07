using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map3Timer : MonoBehaviour
{
    public gamemanager gm;
    public float LimitTime;
    public Text text_timer;
    public GameObject timer;

    private bool isIn;

    // Start is called before the first frame update
    void Start()
    {
        isIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIn && gm.text3finished)
        {
            LimitTime -= Time.deltaTime;
            text_timer.text = "제한 시간 : " + Mathf.Round(LimitTime);

            if(LimitTime <= 0)
            {
                gm.GameOver();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timer.SetActive(true);
            isIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timer.SetActive(false);
            isIn = false;
        }
    }
}
