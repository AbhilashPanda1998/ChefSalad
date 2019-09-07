using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoppingBoard : MonoBehaviour
{
    public bool m_IsBeingChopped;
    private Slider m_Slider;
    void Start()
    {
        m_Slider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsBeingChopped)
        m_Slider.value = Mathf.Lerp(m_Slider.value, 1, Time.deltaTime);
    }
}
