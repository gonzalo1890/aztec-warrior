using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerTextDamage : MonoBehaviour
{
    private Text damageText;
    private RectTransform damageTransform;
    public int damageApply;
    private void Start()
    {
        damageText = GetComponent<Text>();
        damageTransform = GetComponent<RectTransform>();

        damageText.text = damageApply.ToString();
        damageText.CrossFadeAlpha(0, 1.5f, false);
        Destroy(gameObject, 2f);
    }
    void Update()
    {
        if(damageTransform != null)
        {
            damageTransform.anchoredPosition = new Vector2(damageTransform.anchoredPosition.x, damageTransform.anchoredPosition.y + 50 * Time.deltaTime);
        }
    }
}
