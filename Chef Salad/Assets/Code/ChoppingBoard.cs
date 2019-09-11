using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoppingBoard : MonoBehaviour
{
    public enum ChoppingBoardType      // Distinguish Choppingg Board in Scene
    {
        NONE,
        ChoppingBoard_A,
        ChoppingBoard_B
    }

    #region Variables
    private float m_CurrentLerpTime;
    public bool m_IsBeingChopped;
    public bool m_IsPlayerInArea;
    private PlayerController m_PlayerController;
    private Vegetable.VegetableType m_CurrentVegetableType;
    [SerializeField]
    private ChoppingBoardType m_ChoppingBoardType;
    [SerializeField]
    private Slider m_Slider;
    [SerializeField]
    private GameObject m_Plate;
    [SerializeField]
    private float m_ChoppingTime;
    #endregion

    #region Properties
    public Slider Slider
    {
        get { return m_Slider; }
    }

    public ChoppingBoardType ChoppingBoardTypeEnum
    {
        get { return m_ChoppingBoardType; }
    }

    public Vegetable.VegetableType CurrentVegetableType
    {
        get {return m_CurrentVegetableType; }
        set { m_CurrentVegetableType = value; }
    }

    public GameObject Plate
    {
        get { return m_Plate; }
        set { m_Plate = value; }
    }
    #endregion

    #region Unity callbacks
    private void Update()
    {
        if (m_IsBeingChopped)
        {
            if (m_CurrentLerpTime <= m_ChoppingTime)
            {
                m_CurrentLerpTime += Time.deltaTime;
                m_Slider.value = Mathf.Lerp(0, 1, m_CurrentLerpTime / m_ChoppingTime);       // Lerp Sllider Value for showing chopping takes time
                m_PlayerController.TextStatus.text = "Chopping " + m_CurrentVegetableType.ToString();
            }
            if (m_Slider.value>=1f)
            {
                m_Slider.value = 0;
                m_CurrentLerpTime = 0;
                m_IsBeingChopped = false;
                m_PlayerController.TextStatus.text = "Chopped " + m_CurrentVegetableType.ToString();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_IsPlayerInArea = true;
            m_PlayerController = other.GetComponent<PlayerController>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_IsPlayerInArea = false;
        }
    }
    #endregion
}
