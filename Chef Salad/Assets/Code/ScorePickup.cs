using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour, IOnPickedUp
{
    [SerializeField]
    private float BonusScoreAmount;
    public void OnPickedUp(PlayerController playerController)   //Score Pickable object function
    {
        playerController.UpdateScoreForPlayer(BonusScoreAmount);
        Destroy(gameObject);
    }
}
