using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iaction
{
    void OnOver(GameObject actionObject);
    void OnAction(GameObject actionObject);
}
