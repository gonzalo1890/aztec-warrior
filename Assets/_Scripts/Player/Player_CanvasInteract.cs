using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Player_CanvasInteract : MonoBehaviour
{

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;


    void Update()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);

        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if(results.Count > 0)
        {
            if(results[0].gameObject.GetComponent<ItemSaved>() != null)
            {
                GameManager.Instance.canvasManager.ItemSelected(results[0].gameObject.GetComponent<RectTransform>());
            }
            else
            {
                GameManager.Instance.canvasManager.ItemDeselected();
            }
        }
        else
        {
            GameManager.Instance.canvasManager.ItemDeselected();
        }

    }

}