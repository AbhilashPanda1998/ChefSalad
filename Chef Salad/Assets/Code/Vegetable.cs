using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vegetable : MonoBehaviour
{
    public enum VegetableType       //Vegetable Types... Can be named as Real vegtables name later...
    {
        A,
        B,
        C,
        D,
        E,
        F
    }

    public enum STATE               // Veghetable States throughout the game
    {
        IDLE,
        PICKED,
        TOBECHOPPED,
        CHOPPED,
        READY
    }

    #region Variables
    [SerializeField]
    private STATE m_VegetableState;
    [SerializeField]
    private VegetableType m_VegetableType;
    private Vector3 m_OriginalPosition;
    private PlayerController.PlayerIndex m_OwnerPlayerIndex;
    private PlayerController m_OwnerPlayerController;
    private ChoppingBoard m_ChoppingBoardOwner;
    private List<PlayerController.PlayerIndex> m_PlayerInZone = new List<PlayerController.PlayerIndex>();  // to check if player is in zone to do action with vegetable related things
    public static Action<VegetableType, ChoppingBoard.ChoppingBoardType> AddvegetableToSalad;
    public static Action<GameObject, Vector3> SpawnVegtable;
    #endregion

    #region Properties
    public VegetableType VegetanbleType
    {
        get { return m_VegetableType; }
        set { m_VegetableType = value; }
    }

    public STATE VegetableState
    {
        get { return m_VegetableState; }
        set { m_VegetableState = value; }
    }
    #endregion

    #region Unity callbacks
    private void Start()
    {
        PlayerController.TriggerInput += SubscribeInput;
        m_OriginalPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_OwnerPlayerController = other.GetComponent<PlayerController>();
            m_PlayerInZone.Add(m_OwnerPlayerController.PlayerIndexValue);
            m_OwnerPlayerIndex = m_OwnerPlayerController.PlayerIndexValue;
        }
        if (other.GetComponent<ChoppingBoard>())
        {
            m_ChoppingBoardOwner = other.GetComponent<ChoppingBoard>();
            if (!m_PlayerInZone.Contains(m_OwnerPlayerIndex))
                m_PlayerInZone.Add(m_OwnerPlayerIndex);
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
            m_PlayerInZone.Remove(m_OwnerPlayerIndex);
        }
    }

    private void OnDestroy()
    {
        PlayerController.TriggerInput -= SubscribeInput;
    }
    #endregion

    #region Class Functions
    private void SubscribeInput(PlayerController playerController)
    {
        if (!m_PlayerInZone.Contains(playerController.PlayerIndexValue))
            return;

        switch (m_VegetableState)
        {
            case STATE.IDLE:
                if (playerController.OrderOfCollection.Count == 2 || playerController.OrderOfCollection.Contains(m_VegetableType)) //Can pickup only 2 veg, also checks not to pick up duplicate veg again
                    return;
                if (SpawnVegtable != null)
                    SpawnVegtable(this.gameObject, m_OriginalPosition);
                gameObject.layer = m_OwnerPlayerController.gameObject.layer;
                playerController.OrderOfCollection.Add(m_VegetableType);
                m_OwnerPlayerController.TextStatus.text = "Picked " + m_VegetableType.ToString();
                m_VegetableState = STATE.PICKED;
                this.transform.SetParent(playerController.transform);
                break;
            case STATE.PICKED:
                if (playerController.OrderOfCollection[0] == m_VegetableType)  
                {
                    GetComponent<Collider>().enabled = false;
                    this.transform.SetParent(m_ChoppingBoardOwner.transform);
                    this.transform.localPosition = new Vector3(UnityEngine.Random.Range(-0.2f,0.2f), 0, UnityEngine.Random.Range(-0.2f, 0.2f));
                    m_OwnerPlayerController.TextStatus.text = "Placed " + m_VegetableType.ToString() + " On Board";
                    m_VegetableState = STATE.TOBECHOPPED;
                }
                break;
            case STATE.TOBECHOPPED:
                if (!m_ChoppingBoardOwner.m_IsBeingChopped && m_ChoppingBoardOwner.m_IsPlayerInArea) //veg can only be chopped if other veg is not being chopped and player is in the collision area
                {
                    m_OwnerPlayerController.ChangeSpeed(0);
                    m_ChoppingBoardOwner.CurrentVegetableType = m_VegetableType;
                    m_ChoppingBoardOwner.m_IsBeingChopped = true;
                    m_VegetableState = STATE.CHOPPED;
                }
                break;
            case STATE.CHOPPED:
                if (!m_ChoppingBoardOwner.m_IsBeingChopped && m_ChoppingBoardOwner.m_IsPlayerInArea)  //veg can only be kept in plate if it is chopped and player is in the collision area
                {
                    m_OwnerPlayerController.ChangeSpeed(8);
                    this.transform.SetParent(m_ChoppingBoardOwner.Plate.transform);
                    this.transform.localPosition = new Vector3(UnityEngine.Random.Range(-0.027f, 0.027f), 19f, UnityEngine.Random.Range(-0.16f, 0.2f));
                    this.transform.localScale = new Vector3(.5f, .5f, .5f);
                    m_OwnerPlayerController.TextStatus.text = m_VegetableType.ToString() + " Placed On Plate";
                    if (AddvegetableToSalad != null)
                        AddvegetableToSalad(m_VegetableType, m_ChoppingBoardOwner.ChoppingBoardTypeEnum);
                    m_OwnerPlayerController.OrderOfCollection.RemoveAt(0);
                    m_VegetableState = STATE.READY;
                }
                break;
            case STATE.READY:
                return;
            default:
                break;
        }

    }
    #endregion
}
