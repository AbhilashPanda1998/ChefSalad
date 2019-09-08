using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVeg : MonoBehaviour
{
    [System.Serializable]
    public struct VegItems
    {
        public GameObject Veg;
        public Vector3 Position;
    }

    [SerializeField]
    private List<VegItems> m_Item;
    private List<VegItems> m_Temp;

    private void Start()
    {
        for (int i = 0; i < m_Item.Count; i++)
        {
            var v = m_Item[i];
            v.Position = v.Veg.transform.localPosition;
            m_Item[i] = v;
        }
        m_Temp = m_Item;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
        }
    }

    void Check()
    {
        for (int n = 0; n < m_Temp.Count; n++)
        {
            if (!m_Item.Contains(m_Temp[n]))
            {
                Debug.Log(m_Temp[n].Veg.name);
            }
        }
    }
}
