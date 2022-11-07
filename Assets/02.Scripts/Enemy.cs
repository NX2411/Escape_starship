using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public AudioClip DamageAudio;
    private AudioSource audioSource;
    private bool isPlayingAudio;

    public enum Type { A, B, C }
    public gamemanager gm;
    public Type enemyType;
    public int maxHealth;
    public int curHealth;
    public Transform Target;
    public BoxCollider meleeArea;
    public GameObject sphereCollider;
    public bool isChase;
    public bool isAttack;

    Rigidbody rb;
    BoxCollider boxCollider;

    FollowingTarget ft;
    Material mat;
    NavMeshAgent nav;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        //Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("IsWalk", true);
    }

    void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(Target.position);
            nav.isStopped = !isChase;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(curHealth > 0)
        {
            if (other.tag == "Melee")
            {
                Weapon weapon = other.GetComponent<Weapon>();
                curHealth -= weapon.damage;
                Vector3 reactVec = transform.position - other.transform.position;

                StartCoroutine(OnDamage(reactVec));
            }
            else if (other.tag == "BULLET")
            {
                Bullet bullet = other.GetComponent<Bullet>();
                curHealth -= bullet.damage;
                Vector3 reactVec = transform.position - other.transform.position;
                Destroy(other.gameObject);

                StartCoroutine(OnDamage(reactVec));
            }
        }
    }

    void FreezeVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Targeting()
    {
        float targetRadius = 1.5f;
        float targetRange = 3f;

        RaycastHit[] rayHits = 
            Physics.SphereCastAll(transform.position,
                                  targetRadius,
                                  transform.forward,
                                  targetRange,
                                  LayerMask.GetMask("Player"));
        if(rayHits.Length > 0 && !isAttack && curHealth > 0)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("IsAttack", true);
        

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true; 
        
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);
        isChase = true;
        isAttack = false;
        anim.SetBool("IsAttack", false);
    }

    void FixedUpdate()
    {
        if (!isChase)
        {
            if (sphereCollider.GetComponent<FollowingTarget>().isIn)
            {
                if (!isAttack)
                    ChaseStart();
                else
                {
                    isChase = false;
                }
            }
        }

        if (isChase)
        {
            if (!sphereCollider.GetComponent<FollowingTarget>().isIn)
            {
                isChase = false;
                anim.SetBool("IsWalk", false);
            }
        }

        FreezeVelocity();
        Targeting();
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0)
        {
            mat.color = Color.white;
            playAudio(DamageAudio);
        }
        else if(curHealth < 0 || curHealth == 0)
        {
            mat.color = Color.gray;
            gameObject.layer = 13;
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("DoDie");

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rb.AddForce(reactVec * 4, ForceMode.Impulse);

            if (enemyType == Type.A)
                gm.enemy1_num -= 1;

            if (enemyType == Type.B)
                gm.enemy2_num -= 1;

            if (enemyType == Type.C)
                gm.enemy3_num -= 1;

            Destroy(gameObject, 4);
        }
    }

    void playAudio(AudioClip audio)
    {
        isPlayingAudio = false;
        if (!isPlayingAudio)
        {
            audioSource.PlayOneShot(audio);
        }
        isPlayingAudio = true;
    }

}
