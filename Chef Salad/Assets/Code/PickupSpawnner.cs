using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnner : MonoBehaviour
{
    private List<GameObject> m_SpawnPoints = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_Reward = new List<GameObject>();

    private void Start()
    {

        foreach (Transform item in transform)
        {
            m_SpawnPoints.Add(item.gameObject);
        }
        CheckCombinationWithOrder.RewardPlayer += SpawnPickup;
    }

    private void SpawnPickup(Customer m_Customer)
    {
        GameObject reward = Instantiate(m_Reward[Random.Range(0, m_Reward.Count)], m_SpawnPoints[Random.Range(0, m_SpawnPoints.Count)].transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        CheckCombinationWithOrder.RewardPlayer -= SpawnPickup;
    }
}
