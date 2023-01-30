using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum consumableType { Health, Food, Water, Stamina, Poison}
public class Consumable : Item
{
    public consumableType consumableType;
}
