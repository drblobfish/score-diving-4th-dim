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
        [Serializable]
        public struct Result
        {
            public String time;
            public String experimentID;
            public List<Pair> Pairs;
        }
        
        [Serializable]
        public struct Pair
        {
            public String slot;
            public String answer;
        }

        Result Associations(Obj[] _list1, Obj[] _list2, String experimentID)
        {
            DateTime now = DateTime.Now;
            Result answers = new Result();
            answers.experimentID = experimentID;
            answers.time = now.ToString();
            answers.Pairs = new List<Pair>();
            int index = 0;
            foreach (Obj obj2 in _list2)
            {
                foreach (Obj obj1 in _list1)
                {
                    if (obj1.obj.transform.position == obj2.obj.transform.position)
                    {
                        Pair pair = new Pair();
                        pair.slot = obj1.obj.name;
                        pair.answer = obj2.obj.name;
                        answers.Pairs.Add(pair);
                    }
                }
                index += 1;
            }
            return answers;
        }


        public Button verify;
        public GameObject buttonManager;
        Result result;
        String jsonResult;
        String experimentID;
        void SortOnClick()
        {
            experimentID = PlayerPrefs.GetString("experiment_ID");
            result = Associations(datasets, slots,experimentID);
            jsonResult = JsonUtility.ToJson(result, true);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/"+ DateTime.Now.ToString("dd-MM-HH-mm") + ".json", jsonResult);
            Debug.Log(Application.persistentDataPath);
        }

        Obj[] datasets, slots;
        DragDropInitializer initializer = new DragDropInitializer();
        void Start()
        {
            datasets = initializer.InitializeObj("Dataset", 14);
            slots = initializer.InitializeObj("Slot", 7);

            verify.onClick.AddListener(SortOnClick);
            buttonManager.SetActive(false);
        }

        //Show or not the buttons. Although this could be executed in DragDropRaycast.cs, these steps are done here,
        //causing extra script communication, for the sake of reading clarity.
        public DragDropRaycast dragdropManager ;
        void Update()
        {
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