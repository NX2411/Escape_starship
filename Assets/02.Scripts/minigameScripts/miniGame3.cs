using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class miniGame3 : MonoBehaviour
{
    public bool isFinish;
    public GameObject spaceShip;

    bool isCorrect1;

    private float timeLeft = 4f;
    private float nextTime = 0.0f;

    [SerializeField]
    Text text1;

    // Start is called before the first frame update
    void Start()
    {
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        isCorrect1 = spaceShip.GetComponent<DragAndDrop>().enabled;
        if (!isCorrect1)
        {
            isFinish = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isFinish)
        {
            if(Time.time > nextTime)
            {
                nextTime = Time.time + timeLeft;
                StartCoroutine(textChanger());
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    IEnumerator textChanger()
    {
        
        yield return new WaitForSeconds(1f);
        text1.text = "항로 설정중.";
        yield return new WaitForSeconds(1f);
        text1.text = "항로 설정중..";
        yield return new WaitForSeconds(1f);
        text1.text = "항로 설정중...";

    }
}
