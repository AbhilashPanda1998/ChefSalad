using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    #region Variables
    private List<PlayerController.PlayerIndex> m_PlayerInZone = new List<PlayerController.PlayerIndex>();   // To Check if player is in zone to throw wrong Order or Vegetable
    private PlayerController m_OwnerPlayerController;
    #endregion

    #region Unity callbacks
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

    private void OnDestroy()
    {
        PlayerController.TriggerInput -= ThrowTrash;
    }
    #endregion

    #region Class Function
    private void ThrowTrash(PlayerController playerController)
    {
        if (!m_PlayerInZone.Contains(playerController.PlayerIndexValue))
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
    #endregion
}
