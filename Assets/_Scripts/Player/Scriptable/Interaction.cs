﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : ScriptableObject
{
    public abstract void Execute(float d);
}
