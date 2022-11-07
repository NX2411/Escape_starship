using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hologramCtrl : MonoBehaviour
{
    private float curspeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            curspeed = other.GetComponent<Player>().speed;
            other.GetComponent<Player>().speed = other.GetComponent<Player>().speed * 0.3f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().speed = curspeed;
        }
    }
}
