using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVeg : MonoBehaviour
{
    #region Variables
    private Transform m_ParentTransform;
    [SerializeField]
    private float m_WaitTime;
    #endregion

    #region Unity callbacks
    private void Start()
    {
        m_ParentTransform = transform;
        Vegetable.SpawnVegtable += SpawnVegAtLocation;
    }

    private void OnDestroy()
    {
        Vegetable.SpawnVegtable -= SpawnVegAtLocation;
    }
    #endregion

    #region Class Functions
    private void SpawnVegAtLocation(GameObject OriginalVegetable, Vector3 Position )
    {
        StartCoroutine(SpawnVegCouroutine(m_WaitTime, OriginalVegetable, Position));
    }

    IEnumerator SpawnVegCouroutine(float time,GameObject OriginalVegetable, Vector3 Position) //Spawns the clone of that vegetable after some time if it gets picked up.. Then it Resets that veghetable properties
    { 
        yield return new WaitForSeconds(m_WaitTime);
        GameObject cloneVegetable = Instantiate(OriginalVegetable);
        cloneVegetable.GetComponent<Vegetable>().VegetableState = Vegetable.STATE.IDLE;
        cloneVegetable.transform.SetParent(m_ParentTransform);
        cloneVegetable.transform.position = Position;
        cloneVegetable.transform.rotation = Quaternion.identity;
        cloneVegetable.transform.localScale = new Vector3(1, 1, 1);
        cloneVegetable.name = OriginalVegetable.GetComponent<Vegetable>().VegetanbleType.ToString();
        cloneVegetable.layer = 0;
        cloneVegetable.GetComponent<Collider>().enabled = true;
        cloneVegetable.GetComponent<Vegetable>().VegetanbleType = OriginalVegetable.GetComponent<Vegetable>().VegetanbleType;
    }
    #endregion
}
