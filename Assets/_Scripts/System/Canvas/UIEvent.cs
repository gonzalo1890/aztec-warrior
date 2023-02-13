using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEventEnter;

    public FMODUnity.EventReference fmodEventDown;

    public bool onStart = false;
    private void Start()
    {
        if (onStart)
        {
            Destroy(gameObject, 2);
        }
    }

    public void PlayEnter(int value = -1, string name = "")
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEventEnter);
        if (value != -1)
        {
            instance.setParameterByName(name, value);
        }
        instance.start();
    }

    public void PlayDown(int value = -1, string name = "")
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEventDown);
        if (value != -1)
        {
            instance.setParameterByName(name, value);
        }
        instance.start();
    }

    public void Stop()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        PlayEnter();
    }

    public void OnPointerDown(PointerEventData ped)
    {
        PlayDown();
    }
}