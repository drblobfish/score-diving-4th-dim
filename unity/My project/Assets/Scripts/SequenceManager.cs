using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private int nbSequence=3;
    private int timesPlayed;

    // Animation
    [SerializeField] private GameObject dataset;
    private DatasetAnim datasetAnim;

    private bool isPaused = false;

    // Timer
    private bool timerrunning = false;
    [SerializeField] private float timerSetting;
    private float timeRemaining;

    // GUI
    public GameObject mainMenu;
    public GameObject background;
    public Text timer;
    public Text sequenceIndicator;
    public GameObject playButton;
    public GameObject pauseButton;


    void Start()
    {
        datasetAnim = dataset.GetComponent<DatasetAnim>();
    }

    public void BeginSequences()
    {
        timesPlayed = 0;
        PlaySequence();
    }

    void PlaySequence()
    {
        timesPlayed++;
        sequenceIndicator.text = "Times Played: " + timesPlayed.ToString() + "/3";
        Debug.Log("play sequence");
        datasetAnim.StartAnim();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerrunning)
        {
            if (timeRemaining > 0) 
            {
                timeRemaining-= Time.deltaTime;
                timer.text = "Time remaining before next sequence: " + timeRemaining.ToString("0.0");
            }
            else 
            {
                background.SetActive(false);
                timer.enabled = false;
                PlaySequence();
                timerrunning = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))  //Play
        {
            if (isPaused)
            {
                OnButtonPlayClick();

            }  else
            {
                OnButtonPauseClick();
            }
        }
    }

    public void EndAnimation()
    {
        Debug.Log("End animation");
        if (timesPlayed < nbSequence) 
        {
            StartTimer();
        }
    }

    void StartTimer()
    {
        //UI
        timer.enabled = true;
        background.SetActive(true);

        // timer
        timerrunning = true;
        timeRemaining = timerSetting;
    }

    // Pause and Play with buttons
    public void OnButtonPlayClick()
    {
        datasetAnim.Play();
        isPaused = false;
        pauseButton.SetActive(true);
        playButton.SetActive(false);
    }
    public void OnButtonPauseClick()
    {
        datasetAnim.Pause();
        isPaused = true;
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }
}
