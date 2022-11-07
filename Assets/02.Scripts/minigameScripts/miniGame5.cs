using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniGame5 : MonoBehaviour
{
    public bool isclick;
    public bool isFinish;
    public GameObject btn;

    // Start is called before the first frame update
    void Start()
    {
        isclick = false;
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isclick)
        {
            isFinish = true;
            btn.GetComponent<Button>().enabled = false;
        }
            
    }

    public void clicked()
    {
        Debug.Log(isclick);

        if (!isclick)
        {
            isclick = true;
        }
        else
            isclick = false;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
