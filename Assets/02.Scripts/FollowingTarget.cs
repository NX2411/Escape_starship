using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingTarget : MonoBehaviour
{
    public bool isIn = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isIn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isIn = false;
        }
    }
}
