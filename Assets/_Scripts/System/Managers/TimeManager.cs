using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    bool isEnd = false;
    private void Update()
    {
        if (isEnd)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }
    public void DoSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .5f;
        isEnd = true;
    }

    public void DoSlowmotionTime(float time)
    {
        isEnd = false;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .5f;
        CancelInvoke(nameof(ManualEnd));
        Invoke(nameof(ManualEnd), time);
    }

    public void ManualEnd()
    {
        isEnd = true;
    }
}
