using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMinimap : MonoBehaviour
{
    public GameObject miniMap1P;
    public GameObject miniMap1Back;
    public GameObject miniMap1Cam;
    public GameObject miniMap2P;
    public GameObject miniMap2Back;
    public GameObject miniMap2Cam;

    private bool isMinimap1;
    private bool isMinimap2;

    private void Start()
    {
        isMinimap1 = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (miniMap1Cam.activeSelf)
            {
                miniMap1P.SetActive(false);
                miniMap1Back.SetActive(false);
                miniMap1Cam.SetActive(false);
                miniMap2P.SetActive(true);
                miniMap2Cam.SetActive(true);
                miniMap2Back.SetActive(true);
                isMinimap1 = false;
            }
            else
            {
                miniMap1P.SetActive(true);
                miniMap1Back.SetActive(true);
                miniMap1Cam.SetActive(true);
                miniMap2P.SetActive(false);
                miniMap2Cam.SetActive(false);
                miniMap2Back.SetActive(false);
                isMinimap1 = false;
            }
        }


    }
}
