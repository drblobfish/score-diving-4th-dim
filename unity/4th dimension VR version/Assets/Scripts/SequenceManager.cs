using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
    public GameObject sortingButton;
    public GameObject background;
    public Text timer;
    public Text sequenceIndicator;
    public GameObject playButton;
    public GameObject pauseButton;

    public InputActionProperty playPauseAction;


    void Start()
    {
        datasetAnim = dataset.GetComponent<DatasetAnim>();
    }

    private void Awake()
    {
        playPauseAction.action.performed += onPlayPausePerformed;
    }

    private void onPlayPausePerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Play/Pause");
        if (isPaused)
        {
            OnButtonPlayClick();

        }
        else
        {
            OnButtonPauseClick();
        }
    }

    public void BeginSequences()//Start a new experiment of 3 sequences
    {
        timesPlayed = 0;
        PlaySequence();
    }

    void PlaySequence()
    {
        timesPlayed++;
        sequenceIndicator.text = "Times Played: " + timesPlayed.ToString() + "/3";
        Debug.Log("play sequence " + timesPlayed.ToString());
        datasetAnim.StartAnim();
    }

    public void EndAnimation() // Once the animation is finished, DatasetAnim call this function
    {
        Debug.Log("End animation");
        if (timesPlayed < nbSequence) // play a timer if we still need to play a sequence
        {
            StartTimer();
        }
        else //end of the sequences
        {
            EndSequences();
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

    // Update is called once per frame
    void Update()
    {
        if (timerrunning) // decrease and show the value of the time
        {
            if (timeRemaining > 0) 
            {
                timeRemaining-= Time.deltaTime;
                timer.text = "Time remaining before next sequence: " + timeRemaining.ToString("0.0");
            }
            else // at the end of the timer play a new sequence
            {
                //UI
                background.SetActive(false);
                timer.enabled = false;
                // play new sequence
                PlaySequence();
                timerrunning = false;
            }
        }

    }

    void EndSequences()
    {
        mainMenu.SetActive(true);
        sortingButton.SetActive(true);
        dataset.SetActive(false);
    }

    // Pause and Play with buttons
    public void OnButtonPlayClick()
    {
        datasetAnim.Play();
        isPaused = false;

        // UI
        pauseButton.SetActive(true);
        playButton.SetActive(false);
    }
    public void OnButtonPauseClick()
    {
        datasetAnim.Pause();
        isPaused = true;

        //UI
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }
}
