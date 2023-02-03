using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    public List<GameObject> BloodObjects;

    public DamageElement damageElement;
    public List<GameObject> damageElementObjects;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void CreateBloodParticle(Vector3 positionParticle, Transform parentSelect = null, bool isParent = false)
    {
        GameObject particle = Instantiate(BloodObjects[0], positionParticle, Quaternion.identity) as GameObject;

        if (isParent)
        {
            particle.transform.SetParent(parentSelect);
        }
        Destroy(particle, 3f);
    }
    public void CreateElementParticle(Vector3 positionParticle, Transform parentSelect = null, bool isParent = false)
    {
        GameObject elementParticle = null;

        if (damageElement == DamageElement.None)
        {
            elementParticle = damageElementObjects[0];
        }
        if (damageElement == DamageElement.Fire)
        {
            elementParticle = damageElementObjects[1];
        }
        if (damageElement == DamageElement.Ice)
        {
            elementParticle = damageElementObjects[2];
        }
        if (damageElement == DamageElement.Electricity)
        {
            elementParticle = damageElementObjects[3];
        }
        if (damageElement == DamageElement.Poison)
        {
            elementParticle = damageElementObjects[4];
        }

        GameObject particle = Instantiate(elementParticle, positionParticle, Quaternion.identity) as GameObject;

        if(isParent)
        {
            particle.transform.SetParent(parentSelect);
        }

        Destroy(particle, 1f);
    }
}
