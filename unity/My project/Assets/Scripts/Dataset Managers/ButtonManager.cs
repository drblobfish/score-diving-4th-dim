using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace sequence
{
    public class ButtonManager : MonoBehaviour
    {
        public InputField iField ;
        public GameObject mainMenu, sequenceUI, studiedDataset ;
        public SequenceManager manager ;

        public int chosenDataset ;
        public string experiment_ID ;
        //The three following voids are just events. And yes, that way of doing is ugly as fuck.
        void GetID()
        {
            experiment_ID = iField.text ;
        }
        void ChoseDataset1()
        {
            chosenDataset = 1 ;
        }
        void ChoseDataset2()
        {
            chosenDataset = 2 ;
        }

        void StoreInfo_StartExp()
        {
            PlayerPrefs.SetString("experiment_ID",experiment_ID) ;
            PlayerPrefs.SetInt("studied_dataset",chosenDataset) ;

            switch (chosenDataset)
            {
                case 1:
                    studiedDataset = GameObject.Find("dataset_1");
                    GameObject.Find("dataset_2").SetActive(false);
                    break;
                case 2:
                    studiedDataset = GameObject.Find("dataset_2");
                    GameObject.Find("dataset_1").SetActive(false);
                    break;
            }

            manager.BeginSequences() ;
            manager.OnButtonPauseClick() ;
            mainMenu.SetActive(false) ;
            sequenceUI.SetActive(true) ;

            studiedDataset = chosenDataset==1?GameObject.Find("dataset_1"):GameObject.Find("dataset_2") ;
        }

        void loadTutorial()
        {
            SceneManager.LoadScene("Tutorial") ;
        }
        public Button enterID, dataset1btn, dataset2btn, storeInformation, tutorialBtn ;
        void Start()
        {
            enterID.onClick.AddListener(GetID) ;
            dataset1btn.onClick.AddListener(ChoseDataset1) ;
            dataset2btn.onClick.AddListener(ChoseDataset2) ;
            storeInformation.onClick.AddListener(StoreInfo_StartExp) ;
            tutorialBtn.onClick.AddListener(loadTutorial) ;

            manager.playButton.onClick.AddListener(manager.OnButtonPlayClick) ;
            manager.pauseButton.onClick.AddListener(manager.OnButtonPauseClick) ;
        } 
    }
}