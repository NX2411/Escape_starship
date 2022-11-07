using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public GameObject[] e;
    public GameObject[] enemies1;
    public GameObject[] enemies2;
    public GameObject[] enemies3;

    public Text enemyText1;
    public Text enemyText2;
    public Text enemyText3;

    int enemy1;
    int enemy2;
    int enemy3;


    // Start is called before the first frame update
    void Start()
    {
        e = GameObject.FindGameObjectsWithTag("Enemy");
        
    }

    // Update is called once per frame
    void Update()
    {
        //enemyText1.text = "X " + enemies1.Length;
        //enemyText2.text = "X " + enemies2.Length;
        //enemyText3.text = "X " + enemies3.Length;
    }
}
