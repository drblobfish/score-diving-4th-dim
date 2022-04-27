using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;



public class SequenceManager : MonoBehaviour
{
    [SerializeField] private int nbSequence=3;
    private int timesPlayed;

    // Animation
    [SerializeField] private ButtonManager btnManager;
    [SerializeField] private GameObject dataset;
    private DatasetAnim datasetAnim;

    private bool isPaused = false;

    // Timer
    private bool timerrunning = false;
    [SerializeField] private float timerSetting;
    private float timeRemaining;

    float pauseTime;

    // GUI
    public GameObject mainMenu;
    public GameObject sortingButton;
    public GameObject timerCanvas;
    public TextMeshProUGUI message;
    public TextMeshProUGUI timer;
    public Text sequenceIndicator;
    public GameObject playButton;
    public GameObject pauseButton;
    private bool nextScene = false;

    public InputActionProperty LeftPlayPauseAction;
    public InputActionProperty RightPlayPauseAction;

    void Start()
    {
        datasetAnim = dataset.GetComponent<DatasetAnim>();
    }

    private void Awake()
    {
        LeftPlayPauseAction.action.performed += onPlayPausePerformed;
        RightPlayPauseAction.action.performed += onPlayPausePerformed;
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
        try
        {
            dataset = btnManager.studiedDataset;
            datasetAnim = dataset.GetComponent<DatasetAnim>();
            timesPlayed++;
            sequenceIndicator.text = "Times Played: " + timesPlayed.ToString() + "/3";
            datasetAnim.StartAnim();
        }
        catch (UnassignedReferenceException)
        {
            dataset = null;
        }
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
        dataset.SetActive(false);
        timerCanvas.SetActive(true);
        timerrunning = true;
        timeRemaining = timerSetting;
    }

    void StopTimer()
    {
        dataset.SetActive(true);
        timerCanvas.SetActive(false);
        timerrunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerrunning) // decrease and show the value of the time
        {
            if (timeRemaining > 0) 
            {
                timeRemaining-= Time.deltaTime;
                timer.text = timeRemaining.ToString("0.0");
            }
            else // at the end of the timer play a new sequence
            {
                //UI
                StopTimer();
                // play new sequence
                if (nextScene)
                {
                    LoadSortingScene();
                } else
                {
                    PlaySequence();
                }
            }
        }
        //Measure time spent in pause mode.
        pauseTime += isPaused ? Time.deltaTime : 0;
    }

    void EndSequences()
    {
        mainMenu.SetActive(false);
        sortingButton.SetActive(false);
        dataset.SetActive(false);
        timerCanvas.SetActive(true);
        message.text = "You will be asked to sort the shapes in order in:";
        timerrunning = true;
        timeRemaining = 5;
        nextScene = true;
    }

    void LoadSortingScene()
    {
        PlayerPrefs.SetFloat("Pause Time", pauseTime);
        SceneManager.LoadScene("VR Sorting Scene");
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
