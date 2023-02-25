using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

    public bool onStart = false;
    private void Start()
    {
        if(onStart)
        {
            Play();
            Destroy(gameObject, 2);
        }
    }

    public void Play(int value = -1, string name = "")
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        if (value != -1)
        {
            instance.setParameterByName(name, value);
        }
        instance.start();
    }

    public void SetParameter(int value = -1, string name = "")
    {
        instance.setParameterByName(name, value);
    }


    public void Stop()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
