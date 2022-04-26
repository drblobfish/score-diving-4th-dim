using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using draganddrop;

public class AnswerRdr : MonoBehaviour
{

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

    [SerializeField] GameObject[] slotGameObjects;

    DragDropInitializer initializer = new DragDropInitializer();
    Obj[] slots;

    float timeFromLoad;

    String jsonResult;

    void Start()
    {
        timeFromLoad = 0;
    }
    public void SortOnClick()
    {
        Debug.Log("Sort on click");
        slots = initializer.InitializeObj(slotGameObjects, 7);

        Result answers = new Result();
        answers.Pairs = new List<Pair>();

        DateTime now = DateTime.Now;
        answers.time = now.ToString();

        answers.experimentID = PlayerPrefs.GetString("experiment_ID");

        answers.pauseTime = PlayerPrefs.GetFloat("Pause Time");

        answers.sortingTime = timeFromLoad;

        foreach (Obj slot in slots)
        {
            Pair pair = new Pair();

            XRSocketInteractor socketInteractor = slot.obj.GetComponent<XRSocketInteractor>();
            IXRSelectInteractable selectedInteractable = socketInteractor.GetOldestInteractableSelected();

            if (selectedInteractable != null)
            {

                pair.slotIndex = slot.index;
                pair.answer = selectedInteractable.transform.gameObject.name;
                Debug.Log(pair.answer);

                answers.Pairs.Add(pair);
                Debug.Log("new pair " + pair.slotIndex + " " + pair.answer);
            }
            
        }

        jsonResult = JsonUtility.ToJson(answers, true);
        Debug.Log(Application.persistentDataPath + "/" + "experimentID" + "_VR.json");
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + DateTime.Now.ToString("dd-MM-HH-mm") + "_VR.json", jsonResult);
    }

    private void Update()
    {
        timeFromLoad += Time.deltaTime;
    }

}
