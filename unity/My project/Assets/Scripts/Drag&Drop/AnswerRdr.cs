
namespace draganddrop.answerrdr
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class AnswerRdr : MonoBehaviour
    {
        //Ugliest function ever:
        string[] Associations (GameObject[] _list1, GameObject[] _list2)
        {
            string[] answers = new string[_list1.Length] ;

            int index = 0 ;
            foreach (GameObject go2 in _list2)
            {
                foreach (GameObject go1 in _list1)
                {
                    if (go1.transform.position == go2.transform.position)
                    {
                        answers[index] = go1.name + " was placed in " + go2.name ;
                    }
                }

                index += 1 ;
            }
            return answers ;
        }


        public Button verify ;
        string[] answerList ;
        void SortOnClick()
        {
            answerList = Associations(datasets, slots) ;
            foreach (string proposition in answerList)
            {
                System.IO.File.AppendAllText("/home/criuser/Desktop/SCORE/answers.txt","\n"+ proposition);
            }
        }

        GameObject[] datasets, slots ;
        DragDropInitializer initializer = new DragDropInitializer() ;
        void Start()
        {
            datasets = initializer.InitializeWithTag("Dataset") ;
            slots = initializer.InitializeWithTag("Slot") ;
 
            verify.onClick.AddListener(SortOnClick) ;
        }
    }
}