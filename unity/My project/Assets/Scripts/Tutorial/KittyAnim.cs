using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutorial
{
    public class KittyAnim : MonoBehaviour
    {
        [SerializeField] private GameObject mesh;
        private Animator m_Animator;

        [SerializeField] private GameObject sequenceManagerGO;
        private TutoVideoManager sequenceManager;
        [SerializeField] private float speed;

        void Start()
        {
            m_Animator = mesh.GetComponent<Animator>();
            sequenceManager = sequenceManagerGO.GetComponent<TutoVideoManager>();
        }

        public void Pause()
        {
            m_Animator.SetFloat ("speed", 0);
        }

        public void Play()
        {
            m_Animator.SetFloat ("speed", speed);
        }

        public void StartAnim()
        {
            m_Animator.Play("Play", -1,0.0f);
            
            m_Animator.SetFloat("speed", speed);
        }

        public void SetSpeed(float setSpeed)
        {
            speed = setSpeed;
            m_Animator.SetFloat ("speed", speed);
        }
    }
}

