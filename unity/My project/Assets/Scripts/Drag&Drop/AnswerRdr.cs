using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using draganddrop.raycast;
using System;

namespace draganddrop.answerrdr
{

    public class AnswerRdr : MonoBehaviour
    {
        public DragDropRaycast dragdropManager ;

        [Serializable]
        public struct Result
        {
            public String time;
            public String experimentID;
            public List<Pair> Pairs;
            public List<InspectionTime> InspectionTimes;
            public float pauseTime;
            public float sortingTime;
        }

        [Serializable]
        public struct InspectionTime
        {
            public float observationTime;
            public string name;
        }
        
        [Serializable]
        public struct Pair
        {
            public int slotIndex;
            public String answer;
        }


        Result Associations(Obj[] _list1, Obj[] _list2, String experimentID, float pauseDuration, float timeFromStart)
        {
            DateTime now = DateTime.Now;
            Result answers = new Result();
            answers.experimentID = experimentID;
            answers.time = now.ToString();
            answers.Pairs = new List<Pair>();
            answers.InspectionTimes = new List<InspectionTime>();
            answers.pauseTime = pauseDuration;
            answers.sortingTime = timeFromStart;
            //Read pairs.
            foreach (Obj obj2 in _list2)
            {
                foreach (Obj obj1 in _list1)
                {
                    if (obj1.obj.transform.position == obj2.obj.transform.position)
                    {
                        Pair pair = new Pair();
                        pair.slotIndex = obj2.index;
                        pair.answer = obj1.obj.name;
                        answers.Pairs.Add(pair);
                    }
                }
            }
            //Take inspection time of each dataset.
            foreach (Obj obj1 in _list1)
            {
                InspectionTime inspctTime = new InspectionTime();
                inspctTime.name = obj1.obj.name;
                inspctTime.observationTime = obj1.inspectionTime;
                answers.InspectionTimes.Add(inspctTime);
            }
            Debug.Log(answers) ;
            return answers;
        }


        public Button verify;
        public GameObject buttonManager;
        Result result;
        String jsonResult;
        String experimentID;
        float pauseDuration;
        void SortOnClick()
        {
            //Initialize object arrays with new values (i.e inspection time) ; 
            datasets = dragdropManager.datasets ;
            slots = initializer.InitializeObj("Slot", 7);

            experimentID = PlayerPrefs.GetString("experiment_ID");
            pauseDuration = PlayerPrefs.GetFloat("Pause Time");
            result = Associations(datasets, slots,experimentID,pauseDuration, timeFromLoad);
            jsonResult = JsonUtility.ToJson(result, true);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/"+experimentID+".json", jsonResult);
        }

        float timeFromLoad;
        Obj[] datasets, slots;
        DragDropInitializer initializer = new DragDropInitializer();
        void Start()
        {
            verify.onClick.AddListener(SortOnClick);
            buttonManager.SetActive(false);

            timeFromLoad = 0;
        }

        //Show or not the buttons. Although this could be executed in DragDropRaycast.cs, these steps are done here,
        //causing extra script communication, for the sake of reading clarity.
        void Update()
        {
            timeFromLoad += Time.deltaTime;
            if (dragdropManager.showButtons)
            {
                buttonManager.SetActive(true);
            }
            else
            {
                buttonManager.SetActive(false);
            }
        }
    }
}