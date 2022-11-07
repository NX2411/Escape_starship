using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGame2 : MonoBehaviour
{
    public GameObject puzzle1;
    public GameObject puzzle2;
    public GameObject puzzle3;
    public GameObject puzzle4;

    bool isCorrect1;
    bool isCorrect2;
    bool isCorrect3;
    bool isCorrect4;

    public bool isEnd;

    // Start is called before the first frame update
    void Start()
    {
        isEnd = false;
        isCorrect1 = true;
        isCorrect2 = true;
        isCorrect3 = true;
        isCorrect4 = true;
    }

    // Update is called once per frame
    void Update()
    {
        isCorrect1 = puzzle1.GetComponent<DragAndDrop>().enabled; 
        isCorrect2 = puzzle2.GetComponent<DragAndDrop>().enabled;
        isCorrect3 = puzzle3.GetComponent<DragAndDrop>().enabled;
        isCorrect4 = puzzle4.GetComponent<DragAndDrop>().enabled;

        if (!isCorrect1 && !isCorrect2 && !isCorrect3 && !isCorrect4)
            isEnd = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
