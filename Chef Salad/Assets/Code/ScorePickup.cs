using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour, IOnPickedUp
{
    public void OnPickedUp(PlayerController playerController)
    {
        playerController.UpdateScoreForPlayer(50);
        Destroy(gameObject);
    }
}
