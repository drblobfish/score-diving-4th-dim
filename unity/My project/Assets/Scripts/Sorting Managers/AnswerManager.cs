using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class AnswerManager : MonoBehaviour
    {
        [SerializeField] private float spacing = 10.0F;
        [Range(0,50)] public int zOffset ;
        [SerializeField] private GameObject[] answersObjects, answersObjects1, answersObjects2 ;
        [SerializeField] private GameObject[] propSlotObjects ;
        public GameObject[] slotObjects ;
        private GameObject[] answers ;
        private GameObject[] propSlots ;
        public GameObject[] slots ;
        private Vector3[] grid,slotgrid;
        private System.Random _random = new System.Random();

        // Start is called before the first frame update
        void Start()
        {
            switch (PlayerPrefs.GetInt("studied_dataset"))
            {
                case 1:
                    answersObjects = answersObjects1;
                    break;
                 case 2:
                    answersObjects = answersObjects2;
                    break;
                case 0:
                    break;
            }

            answers = new GameObject[answersObjects.Length];
            propSlots = new GameObject[propSlotObjects.Length] ;
            slots = new GameObject[slotObjects.Length] ;

            grid = Grid(2,answersObjects.Length/2, Vector3.zero);
            slotgrid = Grid(1,slotObjects.Length, Vector3.forward*(-zOffset));
            Shuffle(grid);
            
            for (int i = 0; i < answersObjects.Length; i++)
            {
                //Initialize datasets:
                GameObject answerParent = Instantiate(answersObjects[i],grid[i],gameObject.transform.rotation,gameObject.transform);
                GameObject child = answerParent.transform.GetChild(0).gameObject;
                child.name = answerParent.name;
                child.transform.parent = gameObject.transform;
                Destroy(answerParent);
                child.AddComponent<BoxCollider>();
                child.tag = "Dataset";
                child.layer = 9 ;
                answersObjects[i]=child;
            }

            for (int i = 0; i<propSlotObjects.Length; i++)
            {
                //Initialize Proposition slots:
                propSlots[i] = Instantiate(propSlotObjects[i],grid[i],gameObject.transform.rotation,gameObject.transform) ;
            }

            for (int i = 0; i<slotObjects.Length; i++)
            {
                //Initialize answering slots:
                slots[i] = Instantiate(slotObjects[i],slotgrid[i],gameObject.transform.rotation,gameObject.transform) ;
            }
        }

        private Vector3[] Grid(int n, int m, Vector3 offset)
        {
            Vector3[] grid = new Vector3[n*m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m ; j++)
                {
                    grid[(i*m)+j] = gameObject.transform.position + Vector3.forward * spacing * i + Vector3.right *spacing* j + offset ;
                }
            }
            return grid;
        }
        void Shuffle(Vector3[] array)
        {
            int p = array.Length;
            for (int n = p - 1; n > 0; n--)
            {
                int r = _random.Next(0, n);
                Vector3 t = array[r];
                array[r] = array[n];
                array[n] = t;
        }
    }
}

