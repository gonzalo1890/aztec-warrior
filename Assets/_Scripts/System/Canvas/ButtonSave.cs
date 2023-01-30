using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSave : MonoBehaviour
{
    public GameObject buttonSelected;
    public int saveId;

    public void SetButtonActive(bool value)
    {
        buttonSelected.SetActive(value);
    }
    public void ClickButton()
    {
        GameManager.Instance.canvasManager.SetSelectedItem(gameObject);
    }
}
