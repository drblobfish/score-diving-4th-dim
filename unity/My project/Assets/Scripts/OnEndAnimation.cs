using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sequence.anim
{
    public class OnEndAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject dataset;
        private DatasetAnim datasetAnim;
        // Start is called before the first frame update
        void Start()
        {
            datasetAnim = dataset.GetComponent<DatasetAnim>();
        }

        public void EndAnimation()
        {
            datasetAnim.EndAnimation();
        }

    }
}