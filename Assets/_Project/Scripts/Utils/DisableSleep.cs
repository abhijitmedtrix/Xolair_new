using UnityEngine;
using System.Collections;

public class DisableSleep : MonoBehaviour
{
    void Awake()
    {
        Screen.sleepTimeout = (int)0f;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
