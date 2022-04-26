using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace draganddrop
{
    public class Obj 
    {
        public GameObject obj ;
        public int index ;
        public float inspectionTime ;

        public Obj(GameObject _obj, int _index, float _inspectionTime)
        {
            obj = _obj ;
            index = _index ;
            inspectionTime = _inspectionTime ;
        }
    }
    
    public class DragDropInitializer
    {
        public GameObject[] InitializeWithTag(string _tag)
        {
            GameObject[] _gameobject = GameObject.FindGameObjectsWithTag(_tag) ;
            return _gameobject ;
        }

        public Obj[] InitializeObj(string _tag, int _nb_elements)
        {
            GameObject[] gameobjects = InitializeWithTag(_tag) ;
            Obj[] objList = new Obj[_nb_elements] ;
            int index = 0 ;

            foreach (GameObject go in gameobjects)
            {
                objList[index] = new Obj(go, index, 0f) ;
                index += 1 ;
            }

            return objList ;
        }
    
    }
}