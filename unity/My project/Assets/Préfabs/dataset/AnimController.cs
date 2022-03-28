using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimController : MonoBehaviour
{
    Animator m_Animator;
    //Value from the slider, and it converts to speed level
    
    float speed;

    private bool animFinished1 = false;
    private bool animFinished2 = false;
    private bool animFinished3 = false;
    public Text timer;
    public float timerSetting;
    private float timeRemaining;
    public Text sequenceIndicator;
    private int timesPlayed;
    public GameObject mainMenu;
    public GameObject background;

    void Start()
    {
        //Get the animator, attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.enabled = false;
        speed = 1;
        timer.enabled = false;
        timeRemaining = timerSetting;
    }

    private void Player(int numberPlayed)
    {

        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > numberPlayed && !m_Animator.IsInTransition(0))
        {

            if (animFinished1 == false)
            {

                m_Animator.enabled = false;

                if (timeRemaining > 0)
                {
                    timer.enabled = true;
                    timeRemaining -= Time.deltaTime;
                    background.SetActive(true);
                    timer.text = "Time remaining before next sequence: " + timeRemaining.ToString();
                }

                else
                {
                    background.SetActive(false);
                    timer.enabled = false;
                    timeRemaining = timerSetting;
                    timesPlayed++;
                    if (numberPlayed == 1)
                    {
                        animFinished1 = true;
                        m_Animator.enabled = true;
                    }
                    if (numberPlayed == 2)
                    {
                        animFinished2 = true;
                        m_Animator.enabled = true;
                    }
                    if (numberPlayed == 3)
                    {
                        animFinished3 = true;
                        mainMenu.SetActive(true);
                    }
                    
                }
            }
        }
    }

    private void Update()
    {
        sequenceIndicator.text = "Times Played: " + timesPlayed.ToString() + "/3";

        //1st animation playing
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !m_Animator.IsInTransition(0))
        {

            if (animFinished1 == false)
            {

                m_Animator.enabled = false;

                if (timeRemaining > 0)
                {
                    timer.enabled = true;
                    timeRemaining -= Time.deltaTime;
                    background.SetActive(true);
                    timer.text = "Time remaining before next sequence: " + timeRemaining.ToString();
                }

                else
                {
                    background.SetActive(false);
                    timer.enabled = false;
                    animFinished1 = true;
                    timeRemaining = timerSetting;
                    timesPlayed++;
                    m_Animator.enabled = true;
                }
            }
        }

        //second animation playing
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 2 && !m_Animator.IsInTransition(0))
        {

            if (animFinished2 == false)
            {

                m_Animator.enabled = false;

                if (timeRemaining > 0)
                {
                    timer.enabled = true;
                    timeRemaining -= Time.deltaTime;
                    background.SetActive(true);
                    timer.text = "Time remaining before next sequence: " + timeRemaining.ToString();
                }

                else
                {
                    background.SetActive(false);
                    timer.enabled = false;
                    animFinished2 = true;
                    timeRemaining = timerSetting;
                    m_Animator.enabled = true;
                    timesPlayed++;
                }
            }
        }

        //3rd sequence playing

        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 3 && !m_Animator.IsInTransition(0))
        {

            if (animFinished3 == false)
            {

                m_Animator.enabled = false;

                if (timeRemaining > 0)
                {
                    timer.enabled = true;
                    timeRemaining -= Time.deltaTime;
                    background.SetActive(true);
                    timer.text = "Time remaining before next sequence: " + timeRemaining.ToString();
                }

                else
                {
                    background.SetActive(false);
                    timer.enabled = false;
                    animFinished3 = true;
                    timesPlayed++;
                    mainMenu.SetActive(true);
                }
            }
        }

        //Pause/Play with only space bar


        if (Input.GetKeyDown(KeyCode.Space))  //Play
        {
            if (!m_Animator.enabled)
            {
                m_Animator.enabled = true;            

            }  else
            {
                m_Animator.enabled = false;
            }
        }
    }


    //control only with pause and play Button
    public void OnButtonPlayClick()
    {
        m_Animator.enabled = true;
    }
    public void OnButtonPauseClick()
    {
        m_Animator.enabled = false;
    }
}