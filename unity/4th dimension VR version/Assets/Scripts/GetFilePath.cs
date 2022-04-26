using UnityEngine;
using UnityEngine.UI;

public class GetFilePath : MonoBehaviour
{
    public InputField iField ;
    // Start is called before the first frame update
    void StoreFilePath()
    {
        PlayerPrefs.SetString("experiment_ID",iField.text) ;
    }
    public Button enterFilePath ;
    void Start()
    {
        enterFilePath.onClick.AddListener(StoreFilePath) ;
    }
   
}
