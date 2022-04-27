using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] InputField iField;

    [SerializeField] private Button enterID, dataset1btn, dataset2btn, storeInformation;

    [SerializeField] private GameObject mainMenu, sequenceUI;

    public GameObject studiedDataset;

    [SerializeField] private SequenceManager sequenceManager;


    public int chosenDataset;

    public string experiment_ID = "";
    // Start is called before the first frame update
    void Start()
    {
        iField.onValueChanged.AddListener(delegate { SetExperimentID(); });

        dataset1btn.onClick.AddListener(ChoseDataset1);
        dataset2btn.onClick.AddListener(ChoseDataset2);
        storeInformation.onClick.AddListener(StoreInfo_StartExp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetExperimentID()
    {
        experiment_ID = iField.text;
        Debug.Log(experiment_ID);
    }

    void ChoseDataset1()
    {
        chosenDataset = 1;
    }

    void ChoseDataset2()
    {
        chosenDataset = 2;
    }

    void StoreInfo_StartExp()
    {
        if (experiment_ID == "")
        {
            Debug.Log("Please enter your ID");
        }
        else
        {
            PlayerPrefs.SetString("experiment_ID", experiment_ID);
            PlayerPrefs.SetInt("studied_dataset", chosenDataset);
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

            sequenceManager.BeginSequences();
            sequenceManager.OnButtonPauseClick();
            mainMenu.SetActive(false);
            sequenceUI.SetActive(true);
            studiedDataset.SetActive(true);
        }
    }
}
