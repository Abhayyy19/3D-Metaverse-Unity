using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action<bool> OnSuccessChanged;

    public static void TriggerSuccessChanged(bool isSuccess)
    {
        OnSuccessChanged?.Invoke(isSuccess);
    }
}