using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using draganddrop.raycast;

namespace draganddrop.answerrdr
{

    public class AnswerRdr : MonoBehaviour
    {
        //Ugliest function ever:
        string[] Associations (Obj[] _list1, Obj[] _list2)
        {
            string[] answers = new string[_list1.Length] ;

            int index = 0 ;
            foreach (Obj obj2 in _list2)
            {
                foreach (Obj obj1 in _list1)
                {
                    if (obj1.obj.transform.position == obj2.obj.transform.position)
                    {
                        answers[index] = obj1.obj.name + " was placed in " + obj2.obj.name + obj2.index ;
                    }
                }

                index += 1 ;
            }
            return answers ;
        }


        public Button verify ;
        public GameObject buttonManager ;
        string[] answerList ;
        string path ;
        void SortOnClick()
        {
            path = PlayerPrefs.GetString("filePath") ;
            answerList = Associations(datasets, slots) ;
            foreach (string proposition in answerList)
            {
                System.IO.File.AppendAllText(path,"\n"+ proposition);
            }
        }

        Obj[] datasets, slots ;
        DragDropInitializer initializer = new DragDropInitializer() ;
        void Start()
        {
            datasets = initializer.InitializeObj("Dataset", 14) ;
            slots = initializer.InitializeObj("Slot", 7) ;

            verify.onClick.AddListener(SortOnClick) ;
            buttonManager.SetActive(false) ;
        }

        //Show or not the buttons. Although this could be executed in DragDropRaycast.cs, these steps are done here,
        //causing extra script communication, for the sake of reading clarity.
        public DragDropRaycast dragdropManager ;
        void Update()
        {
            if (dragdropManager.showButtons)
            {
                buttonManager.SetActive(true) ;
            }
            else
            {
                buttonManager.SetActive(false) ;
            }
        }
    }
}