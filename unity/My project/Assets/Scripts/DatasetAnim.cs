using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatasetAnim : MonoBehaviour
{
    [SerializeField] private GameObject mesh;
    private Animator m_Animator;

    [SerializeField] private GameObject sequenceManagerEmpty;
    private SequenceManager sequenceManager;
    [SerializeField] private float speed;

    // values for a placefolder slider
    float m_MySliderValue;
    [SerializeField] private bool placeHolderSlider = false;
    void Start()
    {
        m_Animator = mesh.GetComponent<Animator>();
        sequenceManager = sequenceManagerEmpty.GetComponent<SequenceManager>();
    }

    public void Pause()
    {
        Debug.Log("paused");
        m_Animator.SetFloat ("speed", 0);
    }

    public void Play()
    {
        Debug.Log("Played");
        m_Animator.SetFloat ("speed", speed);
    }

    public void StartAnim()
    {
        m_Animator.SetTrigger("Play");
        Debug.Log("started playing");
    }

    public void SetSpeed(float setSpeed)
    {
        speed = setSpeed;
        m_Animator.SetFloat ("speed", speed);
    }

    void OnGUI()
    {
        if (placeHolderSlider) {
            //Create a Label in Game view for the Slider
            GUI.Label(new Rect(0, 25, 40, 60), "Speed");
            //Create a horizontal Slider to control the speed of the Animator. Drag the slider to 1 for normal speed.

            m_MySliderValue = GUI.HorizontalSlider(new Rect(45, 25, 200, 60), m_MySliderValue, -1.0F, 1.0F);
            //Make the speed of the Animator match the Slider value
            SetSpeed(m_MySliderValue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !m_Animator.IsInTransition(0)){
            m_Animator.SetTrigger("Stop");
            sequenceManager.EndAnimation();
        }
    }
}
