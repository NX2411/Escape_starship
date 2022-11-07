using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public gamemanager gm;
    public AudioClip shotAudio;
    public AudioSource audioSource;
    public GameObject[] weapons;

    public GameObject mini1;
    public GameObject mini2;
    public GameObject mini3;
    public GameObject mini4;
    public GameObject mini5;


    public Text HealthText;
    public Text GunAmmoText;
    public Text AmmoText;

    public float speed;
    public float turnSpeed;
    public float jumpPower;

    public int ammo;
    public int health;

    public int maxAmmo;
    public int maxHealth;

    float hAxis;
    float vAxis;
    float rAxis;

    bool wDown, jDown;
    bool fDown;
    bool rDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;

    bool isJump;
    bool isSwap;
    bool isReload;
    bool isFireReady = true;
    bool isBorder;
    bool isDamage;
    bool isDead;

    bool isPlayingAudio;

    private Transform tr;
    private Rigidbody rb;
    private MeshRenderer[] meshes;

    Animator anim;
    Weapon equipWeapon;
    float fireDelay;

    void Awake()
    {
        isDead = true;
        // 컴포넌트를 추출해 변수에 대입
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        meshes = GetComponentsInChildren<MeshRenderer>();

        equipWeapon = weapons[0].GetComponent<Weapon>();
        equipWeapon.gameObject.SetActive(true);

        GunAmmoText.text = "-";
        AmmoText.text = ammo + "/" + maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(!mini1.activeSelf && !mini2.activeSelf && !mini3.activeSelf && !mini4.activeSelf && !mini5.activeSelf)
        {
            if (isDead && !gm.isPause && !gm.isMinigamePlaying && gm.text1finished)
            {
                GetInput();
                Move();
                Jump();
                Swap();
                Attack();
                Reload();
            }
        }

        if (health <= 0)
        {
            HealthText.text = "0/" + maxHealth;
            OnDie();
        }

    }

    void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        rAxis = Input.GetAxis("Mouse X");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1");
        rDown = Input.GetButtonDown("Reload");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
    }

    

    void Move()
    {
        // 전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * vAxis) + (Vector3.right * hAxis);
        
        Vector3 moveInput = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

        if (!isBorder)
        {
            // Translate(이동 방향 * 속력 * Time.deltaTime)
            if (!wDown)
            {
                //rb.position += moveInput * speed * 0.01f;
                tr.Translate(moveDir.normalized * speed * Time.deltaTime);
            }
                
            else
            {
                //rb.position += moveInput * speed * 0.0001f;
                tr.Translate(moveDir.normalized * speed * 0.3f * Time.deltaTime);
            }
                

            // 에니메이터 동작
            anim.SetBool("IsRun", moveDir.normalized != Vector3.zero);
            anim.SetBool("IsWalk", wDown);
        }


       // Quaternion rotation = Quaternion.LookRotation(moveInput, Vector3.up);
        //rb.rotation = Quaternion.Lerp(rb.rotation, rotation, Time.fixedDeltaTime * 0.5f);

        // Vector3.up 축을 기준으로 turnSpeed만큼의 속도로 회전
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * rAxis);

    }

    void Jump()
    {
        if (jDown && !isJump && !isSwap)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("IsJump", true);
            anim.SetTrigger("DoJump");
            isJump = true;
        }
    }

    void FreezeRotation()
    {
        rb.angularVelocity = Vector3.zero;
    }

    void StopTpWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 1, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

     void FixedUpdate()
    {
        FreezeRotation();
        StopTpWall();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("IsJump", false);
            isJump = false;
        }

        else if (collision.gameObject.tag == "Obstacle")
        {
            Vector3 reactVec = transform.position - collision.gameObject.transform.position;
            reactVec = reactVec.normalized;
            reactVec += -Vector3.back;
            rb.AddForce(reactVec * 15, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    AmmoText.text = ammo + "/" + maxAmmo;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    HealthText.text = health + "/" + maxHealth;
                    break;
            }

            StartCoroutine(usingItem(other.gameObject));

        }
        else if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                Debug.Log("들어옴");
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;
                HealthText.text = health+"/"+maxHealth;
                StartCoroutine(OnDamage());
            }
        }

    }

    

    IEnumerator usingItem(GameObject g)
    {
        g.SetActive(false);
        yield return new WaitForSeconds(5f);
        g.SetActive(true);
    }

    IEnumerator OnDamage()
    {
        isDamage = true;

        foreach(MeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.yellow;
        }
        yield return new WaitForSeconds(1f);
        
        isDamage = false;

        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.white;
        }

        if (health <= 0)
        {
            HealthText.text = "0/"+maxHealth;
            OnDie();
        }


    }

    void OnDie()
    {
        anim.SetTrigger("DoDie");
        isDead = false;
        gm.GameOver();
    }

    void Swap()
    {
        int weaponIndex = 0;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if((sDown1 || sDown2 || sDown3) && !isJump)
        {
            if(equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            
            if(equipWeapon.type == Weapon.Type.Range)
            {
                GunAmmoText.text = equipWeapon.curAmmo + "/" + equipWeapon.maxAmmo;
            }
            else
            {
                GunAmmoText.text = "-";
            }

            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("DoSwap");
            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    void Attack()
    {
        if (equipWeapon == null) return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(fDown && isFireReady && !isSwap)
        {
            
            if(equipWeapon.type == Weapon.Type.Melee)
            {
                equipWeapon.Use();
                anim.SetTrigger("DoSwing");
            }
            else if(equipWeapon.type == Weapon.Type.Range && equipWeapon.curAmmo >0)
            {
                playAudio();
                equipWeapon.Use();
                anim.SetTrigger("DoShot");
            }
            fireDelay = 0;
        }

    }

    void Reload()
    {
        if (equipWeapon == null || equipWeapon.type == Weapon.Type.Melee) return;
        if (ammo == 0) return;

        if(rDown && !isJump && !isSwap && isFireReady)
        {
            anim.SetTrigger("DoReload");
            isReload = true;

            Invoke("ReloadOut", 2f);
        }
    }

    void ReloadOut()
    {
        int resAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = resAmmo;
        ammo -= resAmmo;
        AmmoText.text = ammo + "/" + maxAmmo;
        GunAmmoText.text = equipWeapon.curAmmo + "/" + equipWeapon.maxAmmo;
        isReload = false;
    }

    void playAudio()
    {
        isPlayingAudio = false;
        if (!isPlayingAudio)
        {
            audioSource.PlayOneShot(shotAudio);
        }
        isPlayingAudio = true;
    }


}
