using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    public enum VegetableType       //Vegetable Types
    {
        A,
        B,
        C,
        D,
        E,
        F
    }

    [SerializeField]
    private VegetableType m_VegetableType;
}
