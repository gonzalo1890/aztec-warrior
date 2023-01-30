using UnityEngine;

[CreateAssetMenu(menuName = "Interaction/State")]
public class State : ScriptableObject
{
    public Interaction[] interactions;

    public void Tick(float d)
    {
        for (int i = 0; i < interactions.Length; i++)
        {
            interactions[i].Execute(d);
        }
    }
}