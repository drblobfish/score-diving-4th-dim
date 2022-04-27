using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutorial
{
    public class TutoVideoManager : MonoBehaviour
    {
        public TutoButtonManager btnManager;
        public bool isPaused = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))  //Play Pause
            {
                if (isPaused)
                {
                    btnManager.OnButtonPlayClick();
                }  
                else
                {
                    btnManager.OnButtonPauseClick();
                }
            }
        }
    }
}
