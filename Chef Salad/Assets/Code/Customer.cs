using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Customer : MonoBehaviour
{
    public enum CustomerState      //Customer States, they can be angry or normal
    {
        IDLE,
        ANGRY
    }

    #region Variables
    private float m_WaitingTime;
    private float m_Timer;
    private Slider m_Slider;
    public bool m_GotWrongOrder;
    private RandomOrderCombination m_RandomOrderCombination;
    private CheckCombinationWithOrder m_CheckCombinationWithOrder;
    [SerializeField]
    private CustomerState m_CustomerState;
    [SerializeField]
    private float m_WaitingTimeMultiplier;
    #endregion

    #region Properties
    public CustomerState CustomerStateEnum
    {
        get {return m_CustomerState; }
        set { m_CustomerState = value; }
    }

    public float WaitingTime
    {
        get { return m_WaitingTime; }
    }

    public float CurrentTime
    {
        get { return m_Timer; }
    }
    #endregion

    #region Unity callbacks
    private void Start()
    {
        m_Slider = GetComponentInChildren<Slider>();
        m_RandomOrderCombination = GetComponent<RandomOrderCombination>();
        m_CheckCombinationWithOrder = GetComponent<CheckCombinationWithOrder>();
        m_WaitingTime = (float)m_RandomOrderCombination.CustomerOrderCombination.Count * m_WaitingTimeMultiplier;
    }
    private void Update()
    {
        switch (m_CustomerState)
        {
            case CustomerState.IDLE:
                break;
            case CustomerState.ANGRY:
                GetComponent<MeshRenderer>().material.color = Color.red;
                m_WaitingTime = WaitingTime / 2;
                m_Timer = 0;
                m_CustomerState = CustomerState.IDLE;
                break;
            default:
                break;
        }

        if (m_Timer <= m_WaitingTime)
        {
            m_Timer += Time.deltaTime;
            m_Slider.value = Mathf.Lerp(0, 1, m_Timer / m_WaitingTime);
        }
        if (m_Slider.value >= 1 && !m_GotWrongOrder)
        {
            PlayerController[] players = FindObjectsOfType<PlayerController>();
            foreach (PlayerController pc in players)
                pc.UpdateScoreForPlayer(-5f);
            NewCustomerOrder();
            return;
        }
        if (m_Slider.value >= 1 && m_GotWrongOrder)
        {
            m_CheckCombinationWithOrder.AngryPenalizablePlayer.UpdateScoreForPlayer(-20);
            GetComponent<MeshRenderer>().material.color = Color.gray;
            NewCustomerOrder();

        }
    }
    #endregion

    #region Class Functions
    public void NewCustomerOrder()
    {
        m_RandomOrderCombination.ResetVegetables();
        m_RandomOrderCombination.AddVegetables();
        m_Timer = 0;
        m_WaitingTime = (float)m_RandomOrderCombination.CustomerOrderCombination.Count * m_WaitingTimeMultiplier;
    }
    #endregion
}
