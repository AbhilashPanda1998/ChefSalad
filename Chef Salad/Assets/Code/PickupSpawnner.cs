using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnner : MonoBehaviour
{
    #region Variables
    private List<GameObject> m_SpawnPoints = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_Reward = new List<GameObject>();
    #endregion

    #region Unity callbacks
    private void Start()
    {
        foreach (Transform item in transform)
        {
            m_SpawnPoints.Add(item.gameObject);
        }
        CheckCombinationWithOrder.RewardPlayer += SpawnPickup;
    }

    private void OnDestroy()
    {
        CheckCombinationWithOrder.RewardPlayer -= SpawnPickup;
    }
    #endregion

    #region Class Functions
    private void SpawnPickup(PlayerController player)
    {
        GameObject reward = Instantiate(m_Reward[Random.Range(0, m_Reward.Count)], m_SpawnPoints[Random.Range(0, m_SpawnPoints.Count)].transform.position, Quaternion.identity);
        reward.layer = player.gameObject.layer;  // can only be picked up by the one who gave correct order
    }
    #endregion
}
