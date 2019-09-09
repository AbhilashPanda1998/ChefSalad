using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private List<PlayerController.PlayerIndex> m_PlayerInZone = new List<PlayerController.PlayerIndex>();
    private PlayerController m_OwnerPlayerController;

    private void Start()
    {
        PlayerController.TriggerInput += ThrowTrash;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_OwnerPlayerController = other.GetComponentInParent<PlayerController>();
            if (!m_PlayerInZone.Contains(m_OwnerPlayerController.PlayerIndexValue))
                m_PlayerInZone.Add(m_OwnerPlayerController.PlayerIndexValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_PlayerInZone.Remove(other.GetComponentInParent<PlayerController>().PlayerIndexValue);
        }
    }

    private void ThrowTrash(PlayerController playerController, PlayerController.PlayerIndex playerIndex)
    {
        if (!m_PlayerInZone.Contains(playerIndex))
            return;
        if(playerController.transform.childCount!=0)
        {
            playerController.OrderOfCollection.Clear();
            foreach (Transform child in playerController.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

}
