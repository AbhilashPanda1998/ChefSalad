using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public enum PlayerIndex
    {
        PLAYER1,
        PLAYER2
    }

    [SerializeField]
    private PlayerIndex m_PlayerIndex;
    [SerializeField]
    private float m_Speed;
    public static Action<PlayerController, PlayerIndex> TriggerInput;

    public PlayerIndex PlayerIndexValue
    {
        get { return m_PlayerIndex; }
    }

    void Update()
    {
        switch (m_PlayerIndex)
        {
            case PlayerIndex.PLAYER1:

                Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
                transform.position += move * m_Speed * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    if (TriggerInput != null)
                        TriggerInput(this,m_PlayerIndex);
                }
                break;

            case PlayerIndex.PLAYER2:
                Vector3 move2 = new Vector3(Input.GetAxis("Player2Horizontal"), 0, Input.GetAxis("Player2Vertical"));
                transform.position += move2 * m_Speed * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    if (TriggerInput != null)
                        TriggerInput(this,m_PlayerIndex);
                }
                break;
            default:
                break;
        }
    }
}