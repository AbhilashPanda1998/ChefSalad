using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    public enum VegetableType       //Vegetable Types
    {
        A,
        B,
        C,
        D,
        E,
        F
    }

    public enum STATE
    {
        IDLE,
        PICKED,
        BEINGCHOPPED,
        CHOPPED,
    }

    [SerializeField]
    private STATE m_VegetableState;
    [SerializeField]
    private VegetableType m_VegetableType;
    private List<PlayerController.PlayerIndex> m_PlayerInZone = new List<PlayerController.PlayerIndex>();
    private void Start()
    {
        PlayerController.TriggerInput += SubscribeInput;
    }

    private void SubscribeInput(PlayerController playerController,  PlayerController.PlayerIndex playerIndex)
    {
        if (!m_PlayerInZone.Contains(playerIndex))
            return;

        switch (m_VegetableState)
        {
            case STATE.IDLE:
                m_VegetableState = STATE.PICKED;
                this.transform.SetParent(playerController.transform);
                break;
            case STATE.PICKED:
                break;
            case STATE.BEINGCHOPPED:
                break;
            case STATE.CHOPPED:
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            m_PlayerInZone.Add(other.GetComponent<PlayerController>().PlayerIndexValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_PlayerInZone.Remove(other.GetComponent<PlayerController>().PlayerIndexValue);
        }
    }

    private void OnDestroy()
    {
        PlayerController.TriggerInput -= SubscribeInput;
    }
}
