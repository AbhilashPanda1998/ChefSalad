using UnityEngine;
using System.Collections;

public interface IOnPickedUp                  //Common Contact for Pickable objects as they will do something after getting pickedup
{
    void OnPickedUp(PlayerController playerController);
}
