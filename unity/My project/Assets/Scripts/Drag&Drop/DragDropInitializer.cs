using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace draganddrop
{
    public class Obj 
    {
        public GameObject obj ;
        public int index ;

        public Obj(GameObject _obj, int _index)
        {
            obj = _obj ;
            index = _index ;
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
                objList[index] = new Obj(go, index) ;
                index += 1 ;
            }

            return objList ;
        }
    
    }
}