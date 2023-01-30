using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CanvasDamage : CanvasBillboard
{
    public float moveSpeed = 0.03f;
    public float scaleSpeed = -0.03f;
    public RectTransform rectTransform;
    public TMP_Text text;

    public Color damageStandard;
    public Color damageCritical;
    public Color damageFire;
    public Color damageIce;
    public Color damageElectricity;
    public Color damagePoison;

    //private Transform player;
    public override void Start()
    {
        base.Start();

        StartCoroutine(SetAlphaAnimation());
        Destroy(gameObject, 1);
    }


    public override void Update()
    {
        base.Update();

        float dist = Vector3.Distance(my_camera.transform.position, transform.position);
        if(dist < 1.5f) { dist = 1.5f; }
        //Debug.Log(dist);
        dist = dist * 0.00006f;
        transform.localScale = new Vector3(dist, dist, dist);

     //   Vector3 speed = new Vector3(0, moveSpeed, 0);
        //Vector3 scale = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);

        //Vector3 posA = transform.position;
        //Vector3 posB = player.position + new Vector3(0f,0f,0f);
        //Destination - Origin
        //Vector3 dir = (posB - posA).normalized;
       // dir = dir * 0.5f;
       // Vector3 aux = speed + dir;
       // transform.position += aux * Time.deltaTime;
        /*
        if (rectTransform.localScale.x > 0f)
        {
            rectTransform.localScale += scale * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        */
    }

    public void DamageVisual(int damage, DamageElement damageElement, bool isCriticalHit)
    {
        //player = _player;

        if (isCriticalHit)
        {
            //rectTransform.localScale = rectTransform.localScale*2;
            SetDamageText(damageCritical, damage);
        }
        else
        {

            if (damageElement == DamageElement.None)
            {
                SetDamageText(damageStandard, damage);
            }
            if (damageElement == DamageElement.Fire)
            {
                SetDamageText(damageFire, damage);
            }
            if (damageElement == DamageElement.Ice)
            {
                SetDamageText(damageIce, damage);
            }
            if (damageElement == DamageElement.Electricity)
            {
                SetDamageText(damageElectricity, damage);
            }
            if (damageElement == DamageElement.Poison)
            {
                SetDamageText(damagePoison, damage);
            }
        }
    }

    void SetDamageText(Color color, int value)
    {
        text.text = value.ToString();
        text.color = color;
    }


    IEnumerator SetAlphaAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        text.CrossFadeAlpha(0, 0.75f, false);
    }
}
