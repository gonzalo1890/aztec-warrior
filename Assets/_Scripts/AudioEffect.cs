using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

    public void Play()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }
}
