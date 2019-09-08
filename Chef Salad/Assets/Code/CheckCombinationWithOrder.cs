using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCombinationWithOrder : MonoBehaviour
{
    private List<PlayerController.PlayerIndex> m_PlayerInZone = new List<PlayerController.PlayerIndex>();
    private PlayerController m_OwnerPlayerController;
    private RandomOrderCombination m_RandomCombination;
    private List<Vegetable.VegetableType> m_PlayerCookedCombination;
    private bool m_IsCorrectCombination;
    private void Start()
    {
        PlayerController.TriggerInput += MatchCombination;
        m_RandomCombination = GetComponent<RandomOrderCombination>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Plate>())
        {
            m_OwnerPlayerController = other.GetComponentInParent<PlayerController>();
            m_PlayerCookedCombination = m_OwnerPlayerController.GetComponentInChildren<Plate>().SaladCombination;
            if (!m_PlayerInZone.Contains(m_OwnerPlayerController.PlayerIndexValue))
                m_PlayerInZone.Add(m_OwnerPlayerController.PlayerIndexValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Plate>())
        {
            m_PlayerInZone.Remove(other.GetComponentInParent<PlayerController>().PlayerIndexValue);
        }
    }

    private void MatchCombination(PlayerController playerController, PlayerController.PlayerIndex playerIndex)
    {
        if (!m_PlayerInZone.Contains(playerIndex))
            return;
        m_IsCorrectCombination = CheckMatch();
        if(m_IsCorrectCombination)
        {
            m_OwnerPlayerController.TextStatus.text = "Correct Order";
        }
        else
        {
            m_OwnerPlayerController.TextStatus.text = "Wrong Order";
        }
    }

    private bool CheckMatch()
    {
        if (m_RandomCombination.CustomerOrderCombination.Count != m_PlayerCookedCombination.Count)
            return false;
        for (int i = 0; i < m_RandomCombination.CustomerOrderCombination.Count; i++)
        {
            if (!m_RandomCombination.CustomerOrderCombination.Contains(m_PlayerCookedCombination[i]))
                return false;
        }
        return true;
    }
}
