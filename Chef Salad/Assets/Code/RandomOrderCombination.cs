using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomOrderCombination : MonoBehaviour {

    #region Variables
    [SerializeField]
    private List<Vegetable.VegetableType> m_ListOfVegetables = new List<Vegetable.VegetableType>();             //A list containing different type of vegetables
    private List<Vegetable.VegetableType> m_CustomerOrderCombination = new List<Vegetable.VegetableType>();     // The list which will hold combination of customer's order vegetables
    #endregion

    #region Properties
    public List<Vegetable.VegetableType> CustomerOrderCombination
    {
        get { return m_CustomerOrderCombination; }
    }
    #endregion

    #region Unity callbacks
    private void OnEnable()
    {
        AddVegetables();
    }
    #endregion

    #region Class Functions
    public void AddVegetables()     // A recurive Function which adds no of Vevegtables(Randomly) to customers order
    {
        int maxNoOfVegetable = Random.Range(1, 4);
        int random = Random.Range(0, m_ListOfVegetables.Count);
        if(!m_CustomerOrderCombination.Contains(m_ListOfVegetables[random]))
            m_CustomerOrderCombination.Add(m_ListOfVegetables[random]);

        if (m_CustomerOrderCombination.Count < maxNoOfVegetable)
            AddVegetables();
        else
            GetComponentInChildren<Text>().text = string.Join(",", m_CustomerOrderCombination);
        return;
    }

    public void ResetVegetables()
    {
        m_CustomerOrderCombination.Clear();  //Clears the customer combination for next order
    }
    #endregion
}
