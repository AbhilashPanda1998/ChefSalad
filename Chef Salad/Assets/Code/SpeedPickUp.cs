using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickUp : MonoBehaviour, IOnPickedUp
{
    [SerializeField]
    private float m_SpeedBoostTime;
    [SerializeField]
    private float m_SpeedBoostValue;
    [SerializeField]
    private float m_NormalSpeed;

    public void OnPickedUp(PlayerController playerController)
    {
        playerController.ChangeSpeed(m_SpeedBoostValue);
        StartCoroutine(NormalSpeed(playerController));
        Destroy(gameObject);
    }

    IEnumerator NormalSpeed(PlayerController pc)
    {
        yield return new WaitForSeconds(m_SpeedBoostTime);
        pc.ChangeSpeed(m_NormalSpeed);
    }

}