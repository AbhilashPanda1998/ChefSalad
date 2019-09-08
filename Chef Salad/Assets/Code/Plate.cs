using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private List<PlayerController.PlayerIndex> m_PlayerInZone = new List<PlayerController.PlayerIndex>();
    private PlayerController m_OwnerPlayerController;
    private List<Vegetable.VegetableType> m_SaladCombination = new List<Vegetable.VegetableType>();

    public List<Vegetable.VegetableType> SaladCombination
    {
        get { return m_SaladCombination; }
    }

    private void Start()
    {
        PlayerController.TriggerInput += SubscribeInput;
        Vegetable.AddvegetableToSalad += AddVegToSaladCombination;
    }

    private void SubscribeInput(PlayerController playerController, PlayerController.PlayerIndex playerIndex)
    {
        if (!m_PlayerInZone.Contains(playerIndex) || m_SaladCombination.Count ==0)
            return;
        transform.SetParent(m_OwnerPlayerController.transform);
        m_OwnerPlayerController.TextStatus.text = "Picked Plate";
    }

    private void AddVegToSaladCombination (Vegetable.VegetableType vegetableType)
    {
        m_SaladCombination.Add(vegetableType);
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
}
