using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimePickUp : MonoBehaviour, IOnPickedUp
{
    [SerializeField]
    private float IncreaseTimeAmount;

    public void OnPickedUp(PlayerController playerController)           //Time Pickable Object function i.e  increase players time by some amount
    {
        playerController.IncreaseTimerValue(IncreaseTimeAmount);
        Destroy(gameObject);
    }
}
