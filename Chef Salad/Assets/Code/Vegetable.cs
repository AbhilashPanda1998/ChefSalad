using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private PlayerController.PlayerIndex m_OwnerPlayer;
    private ChoppingBoard m_ChoppingBoardOwner;
    private List<PlayerController.PlayerIndex> m_PlayerInZone = new List<PlayerController.PlayerIndex>();
    private void Start()
    {
        PlayerController.TriggerInput += SubscribeInput;
    }

    private void SubscribeInput(PlayerController playerController, PlayerController.PlayerIndex playerIndex)
    {
        if (!m_PlayerInZone.Contains(playerIndex))
            return;

        switch (m_VegetableState)
        {
            case STATE.IDLE:
                if (playerController.OrderOfCollection.Count == 2)
                    return;
                playerController.OrderOfCollection.Add(m_VegetableType);
                m_VegetableState = STATE.PICKED;
                this.transform.SetParent(playerController.transform);
                break;
            case STATE.PICKED:
                if (playerController.OrderOfCollection[0] == m_VegetableType)
                {
                    GetComponent<Collider>().enabled = false;
                    playerController.OrderOfCollection.RemoveAt(0);
                    this.transform.SetParent(m_ChoppingBoardOwner.transform);
                    m_VegetableState = STATE.BEINGCHOPPED;
                }
                break;
            case STATE.BEINGCHOPPED:
                m_ChoppingBoardOwner.m_IsBeingChopped = true;
                break;
            case STATE.CHOPPED:
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_PlayerInZone.Add(other.GetComponent<PlayerController>().PlayerIndexValue);
            m_OwnerPlayer = other.GetComponent<PlayerController>().PlayerIndexValue;
        }
        if (other.GetComponent<ChoppingBoard>())
        {
            m_ChoppingBoardOwner = other.GetComponent<ChoppingBoard>();
            if (!m_PlayerInZone.Contains(m_OwnerPlayer))
                m_PlayerInZone.Add(m_OwnerPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_PlayerInZone.Remove(other.GetComponent<PlayerController>().PlayerIndexValue);
        }
        if (other.GetComponent<ChoppingBoard>())
        {
            m_PlayerInZone.Remove(other.GetComponent<PlayerController>().PlayerIndexValue);
        }
    }

    private void OnDestroy()
    {
        PlayerController.TriggerInput -= SubscribeInput;
    }
}
