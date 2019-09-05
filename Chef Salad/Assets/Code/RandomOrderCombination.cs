using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomOrderCombination : MonoBehaviour {

    [SerializeField]
    private List<Vegetable.VegetableType> m_ListOfVegetables = new List<Vegetable.VegetableType>();             //A list containing different type of vegetables
    private List<Vegetable.VegetableType> m_CustomerOrderCombination = new List<Vegetable.VegetableType>();     // The list which will hold combination of customer's order vegetables

    public List<Vegetable.VegetableType> CustomerOrderCombination
    {
        get { return m_CustomerOrderCombination; }
    }

    private void Start()
    {
        AddVegetables();
        GetComponentInChildren<Text>().text = string.Join("," , m_CustomerOrderCombination);
    }

    private void Update()
    {

    }

    private void AddVegetables()
    {
        int random = Random.Range(0, m_ListOfVegetables.Count);
        if(!m_CustomerOrderCombination.Contains(m_ListOfVegetables[random]))
            m_CustomerOrderCombination.Add(m_ListOfVegetables[random]);

        if (m_CustomerOrderCombination.Count < 3)
            AddVegetables();
        else
            return;
    }
}
