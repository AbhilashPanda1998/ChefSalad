using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    #region Variables
    private List<PlayerController.PlayerIndex> m_PlayerInZone = new List<PlayerController.PlayerIndex>();  // To Check if player is in zone to pickup Plate
    private PlayerController m_OwnerPlayerController;
    private List<Vegetable.VegetableType> m_SaladCombination = new List<Vegetable.VegetableType>();
    private Transform m_StartPos;
    private Transform m_Parent;
    #endregion

    #region Properties
    public List<Vegetable.VegetableType> SaladCombination
    {
        get { return m_SaladCombination; }
    }
    #endregion

    #region Unity callbacks
    private void Start()
    {
        m_StartPos = transform;
        m_Parent = transform.parent;
        PlayerController.TriggerInput += SubscribeInput;
        Vegetable.AddvegetableToSalad += AddVegToSaladCombination;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_OwnerPlayerController = other.GetComponent<PlayerController>();
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
        Vegetable.AddvegetableToSalad -= AddVegToSaladCombination;
    }
    #endregion

    #region Class Functions
    private void SubscribeInput(PlayerController playerController)
    {
        if (!m_PlayerInZone.Contains(playerController.PlayerIndexValue) || m_SaladCombination.Count == 0)
            return;
        transform.SetParent(m_OwnerPlayerController.transform);
        m_OwnerPlayerController.TextStatus.text = "Picked Plate";
        GameObject plateClone = Instantiate(gameObject);
        plateClone.name = "Plate";
        plateClone.transform.SetParent(m_Parent);
        plateClone.transform.position = m_StartPos.position;
        plateClone.transform.rotation = m_StartPos.rotation;
        plateClone.GetComponentInParent<ChoppingBoard>().Plate = plateClone;
        foreach (Transform child in plateClone.transform)
        {
            Destroy(child.gameObject);
        }

    }

    private void AddVegToSaladCombination(Vegetable.VegetableType vegetableType, ChoppingBoard.ChoppingBoardType ChoppingBoardEnum)
    {
        if (GetComponentInParent<ChoppingBoard>().ChoppingBoardTypeEnum == ChoppingBoardEnum)
            m_SaladCombination.Add(vegetableType);
    }
    #endregion
}
