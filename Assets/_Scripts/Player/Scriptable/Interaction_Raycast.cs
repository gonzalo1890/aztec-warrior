using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Interaction/")]
public class Interaction_Raycast : Interaction
{
    Iaction iAction;
    Iaction iActionBackup;
    public override void Execute(float d)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {

            
            iAction = hit.transform.GetComponent<Iaction>();
            if (iAction != null && Vector3.Distance(hit.point, GameManager.Instance.player.transform.position) < 2)
            {
                iActionBackup = iAction;
                iAction.OnOver(hit.transform.gameObject);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    iAction.OnAction(hit.transform.gameObject);
                }
            }
            else
            {
                if (iActionBackup != null)
                {
                    iActionBackup.OnOver(null);
                    iActionBackup = null;
                }
            }
        }
    }
}
