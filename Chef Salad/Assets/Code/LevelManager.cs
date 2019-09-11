using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Variables
    private bool Player1TimeUp;
    private bool Player2TimeUp;
    private PlayerController m_Player1;
    private PlayerController m_Player2;
    PlayerController.PlayerIndex m_Winner;
    [SerializeField]
    private Text m_GameOver;
    #endregion

    #region Unity callbacks
    private void Start()
    {
        PlayerController.CheckTime += CheckTimer;
    }

    private void Update()
    {
        if(Player1TimeUp && Player2TimeUp)
        {
            m_Winner = m_Player1.TotalScore > m_Player2.TotalScore ? m_Player1.PlayerIndexValue : m_Player2.PlayerIndexValue;
            m_GameOver.text = "Game Over.  " + m_Winner.ToString() + " Won. " + "Press E to Restart";
            if(Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnDestroy()
    {
        PlayerController.CheckTime -= CheckTimer;
    }
    #endregion

    #region Class Functions
    private void CheckTimer(PlayerController playerController)
    {
        if(playerController.PlayerIndexValue == PlayerController.PlayerIndex.PLAYER1)
        {
            m_Player1 = playerController;
            playerController.ChangeSpeed(0);
            Player1TimeUp = true;
        }
        else
        {
            m_Player2 = playerController;
            playerController.ChangeSpeed(0);
            Player2TimeUp = true;
        }
    }
    #endregion
}
