using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] InputField iField;
    // Start is called before the first frame update
    void Start()
    {
        iField.onValueChanged.AddListener(delegate { SetExperimentID(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetExperimentID()
    {
        PlayerPrefs.SetString("experiment_ID", iField.text);
        Debug.Log(PlayerPrefs.GetString("experiment_ID"));
    }
}
