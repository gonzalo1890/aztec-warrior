using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum consumableType { Health, Spirit, Bullet, Shell, Misil, Granade}
public class Consumable : Item
{
    public consumableType consumableType;
}
