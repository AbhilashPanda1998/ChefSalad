using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVeg : MonoBehaviour
{
    [SerializeField]
    private float m_WaitTime;
    private Transform m_ParentTransform;

    private void Start()
    {
        m_ParentTransform = transform;
        Vegetable.SpawnVegtable += SpawnVegAtLocation;
    }

    private void OnDestroy()
    {
        Vegetable.SpawnVegtable -= SpawnVegAtLocation;
    }

    private void SpawnVegAtLocation(GameObject Vegetable, Vector3 Position )
    {
        StartCoroutine(MyCoroutine(m_WaitTime, Vegetable,Position));
    }

    IEnumerator MyCoroutine(float time,GameObject Vegetable, Vector3 Position)
    {
        yield return new WaitForSeconds(m_WaitTime);
        GameObject veg = Instantiate(Vegetable, Position, Vegetable.transform.rotation, m_ParentTransform);
    }
}
