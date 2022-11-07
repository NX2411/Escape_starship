using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public gamemanager gm;
    public GameObject coll;
    Animator anim;
    public enum Type { glass, mini2, stage1, stage2 };
    public Type type;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (type == Type.mini2)
            {
                if (gm.mission1 && gm.mission2)
                {
                    coll.SetActive(false);
                    anim.SetBool("character_nearby", true);
                }
            }
            else if (type == Type.stage1)
            {
                if (gm.mission1 && gm.mission2 && gm.mission3 && gm.mission4)
                {
                    coll.SetActive(false);
                    anim.SetBool("character_nearby", true);
                }
            }
            else if (type == Type.glass)
            {
                anim.SetBool("character_nearby", true);
            }
            else if (type == Type.stage2)
            {
                if (gm.isKilledEnemy)
                {
                    coll.SetActive(false);
                    anim.SetBool("character_nearby", true);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (type == Type.mini2)
            {
                if(gm.mission1 && gm.mission2)
                {
                    coll.SetActive(true);
                    anim.SetBool("character_nearby", false);
                }
            }
            else if (type == Type.stage1)
            {
                if(gm.mission1 && gm.mission2 && gm.mission3 && gm.mission4)
                {
                    coll.SetActive(true);
                    anim.SetBool("character_nearby", false);
                }
            }
            else if (type == Type.glass)
            {
                anim.SetBool("character_nearby", false);
            }
            else if (type == Type.stage2)
            {
                if (gm.isKilledEnemy)
                {
                    coll.SetActive(true);
                    anim.SetBool("character_nearby", false);
                }
            }
        }

    }
}
