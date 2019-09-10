using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimePickUp : MonoBehaviour, IOnPickedUp
{
    public static Action<float> IncreaseWaitingTime;

    public void OnPickedUp(PlayerController playerController)
    {
        Destroy(gameObject);
    }
}
