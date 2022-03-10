using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDownAnimation : MonoBehaviour
{
    public Text timer;
    public float timerSetting;
    private float timeRemaining;
    public Text sequenceIndicator;
    private int timesPlayed=1;
    private Animator mAnimator;
    public string animationName;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        sequenceIndicator.text = "Times Played: " + timesPlayed.ToString() + "/3";
        if (timesPlayed <= 3)
        {
            mAnimator.Play(animationName,0, 0f); //Plays again the animation until its played 3 times
            timesPlayed++;
            if (mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime==1.0f)
            {
                timeRemaining = timerSetting;
                if (timeRemaining > 0)
                    {
                    timer.enabled = true;
                     timeRemaining -= Time.deltaTime;
                        timer.text = timeRemaining.ToString();
                    }
                if (timeRemaining == 0)
                {
                    timer.enabled = false;
                }
            }
        }

        else if (timesPlayed > 3)
        {
            mAnimator.GetComponent<Animator>().enabled = false;
            //add something to disable the play pause buttons
        }
    }
    public void OnButtonPlayClick()
    {
     mAnimator.GetComponent<Animator>().enabled = true;
    }
    public void OnButtonPauseClick()
    {
     mAnimator.GetComponent<Animator>().enabled = false;
    }

}
