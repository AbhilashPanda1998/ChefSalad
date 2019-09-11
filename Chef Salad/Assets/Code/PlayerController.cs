using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerIndex
    {
        NONE,
        PLAYER1,
        PLAYER2
    }

    #region Variables
    private float m_TotalScore;
    private List<Vegetable.VegetableType> m_OrderOfColection = new List<Vegetable.VegetableType>();
    public static Action<PlayerController> TriggerInput;
    public static Action<PlayerController> CheckTime;
    [SerializeField]
    private float m_Timer;
    [SerializeField]
    private Text m_TimerText;
    [SerializeField]
    private PlayerIndex m_PlayerIndex;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private Text m_TextStatus;
    [SerializeField]
    private Text m_PlayerScore;
    #endregion

    #region Properties
    public PlayerIndex PlayerIndexValue
    {
        get { return m_PlayerIndex; }
    }

    public List<Vegetable.VegetableType> OrderOfCollection
    {
        get { return m_OrderOfColection; }
    }

    public Text TextStatus
    {
        get { return m_TextStatus; }
    }

    public float TotalScore
    {
        get { return m_TotalScore; }
    }
    #endregion

    #region Unity callbacks
    private void Start()
    {
        m_TotalScore = 0;
    }

    void Update()
    {
        switch (m_PlayerIndex)    //Two player playing in Keyboard,(Player 1 - W/A/S/D and action key LeftCtrl)  and (Player 2 - arrow keys and action key RightCtrl)       
        {
            case PlayerIndex.PLAYER1:

                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                transform.position += move * m_Speed * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    if (TriggerInput != null)
                        TriggerInput(this);
                }
                
                break;

            case PlayerIndex.PLAYER2:
                Vector3 move2 = new Vector3(Input.GetAxis("Player2Horizontal"), 0, Input.GetAxis("Player2Vertical"));
                transform.position += move2 * m_Speed * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    if (TriggerInput != null)
                        TriggerInput(this);
                }
                break;
            default:
                break;
        }

        m_Timer -= Time.deltaTime;
        m_TimerText.text = Mathf.RoundToInt(m_Timer).ToString();
        if (m_Timer <= 0)
        {
            m_TimerText.text = "TimeUp";
            if (CheckTime != null)
                CheckTime(this);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        IOnPickedUp pickedUp = other.GetComponent<IOnPickedUp>();
        if (pickedUp != null)
            pickedUp.OnPickedUp(this);
    }
    #endregion

    #region Class Functions
    public void ChangeSpeed(float speed)     //To Chnange Player Speed
    {
        m_Speed = speed;
    }

    public void UpdateScoreForPlayer(float value)  //To Update Player Score
    {
        m_TotalScore += value;
        m_PlayerScore.text = m_TotalScore.ToString();
    }

    public void IncreaseTimerValue(float amount)  // To increase player timer by TimePickUp Pickable object
    {
        m_Timer = m_Timer + amount;
    }
    #endregion
}