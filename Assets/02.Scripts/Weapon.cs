using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range};
    public Type type;
    public int damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;

    public Text gunAmmmoText;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform CasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if(type== Type.Range && curAmmo > 0)
        {
            curAmmo--;
            gunAmmmoText.text = curAmmo + "/" + maxAmmo;
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.4f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        //√—æÀ πﬂªÁ
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletrb = instantBullet.GetComponent<Rigidbody>();
        bulletrb.velocity = bulletPos.forward * 50;

        //≈∫«« πË√‚
        yield return null;
        GameObject instantCase = Instantiate(bulletCase, CasePos.position, CasePos.rotation);
        Rigidbody Caserb = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = CasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        Caserb.AddForce(caseVec, ForceMode.Impulse);
        Caserb.AddTorque(Vector3.up * 8, ForceMode.Impulse);

    }

}
