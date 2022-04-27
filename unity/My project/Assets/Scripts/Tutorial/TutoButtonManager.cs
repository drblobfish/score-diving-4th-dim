using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace tutorial
{
    public class TutoButtonManager : MonoBehaviour
    {
        public Button playButton, pauseButton, menuButton ;
        public TutoVideoManager vidManager ;
        [SerializeField] private GameObject kitty;
        private KittyAnim kittyAnim;
        void Start()
        {
            kittyAnim = kitty.GetComponent<KittyAnim>() ;

            playButton.onClick.AddListener(OnButtonPlayClick) ;
            pauseButton.onClick.AddListener(OnButtonPauseClick) ;
            menuButton.onClick.AddListener(LoadMenu) ;
        }

        public void OnButtonPlayClick()
        {
            kittyAnim.Play();
            vidManager.isPaused = false;

            // UI
            pauseButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
        }
        public void OnButtonPauseClick()
        {
            kittyAnim.Pause();
            vidManager.isPaused = true;

            //UI
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("Interface");
        }
 
    }
}
