using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceManager : MonoBehaviour
{
    private int timesPlayed;

    // Animation
    [SerializeField] private GameObject dataset;
    private DatasetAnim datasetAnim;

    private bool isPaused = false;

    // Timer
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

    // Update is called once per frame
    void Update()
    {
        if (timesPlayed == 0)
        {
            timesPlayed++;
            datasetAnim.StartAnim();
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
