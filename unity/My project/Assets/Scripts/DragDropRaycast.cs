using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropRaycast : MonoBehaviour
{

	public Camera cam ;
	public bool selection = false ;
	public GameObject selected ;

	float distanceToDataSet ;
	GameObject empty ;
	Vector3 mousePosition3 ;


    float distance(float x1, float y1, float z1, float x2, float y2, float z2)
    {
    	return Mathf.Sqrt((x1-x2)*(x1-x2)+(y1-y2)*(y1-y2)+(z1-z2)*(z1-z2)) ;
    }

	
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
 
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Dataset")
                {
                    selected = hit.collider.gameObject ;
                    distanceToDataSet = hit.transform.position.z - cam.transform.position.z ;
                    selection = true;
                }
            }
		}

		if (Input.GetButtonUp("Fire1") && selection)
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
            {
                if (hit.collider.tag == "Slot")
                {
                    selected.transform.position = hit.transform.position ;
                    selection = false;
                    distanceToDataSet = 0 ;
					selected = empty ;
                }

            }
            else
           	{	
				mousePosition3 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distanceToDataSet) ;
				selected.transform.position = cam.ScreenToWorldPoint(mousePosition3) ;
				selection = false ;
				distanceToDataSet = 0 ;
				selected = empty ;
            }

		}

		if (selection)
		{
			mousePosition3 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distanceToDataSet) ;
			selected.transform.position = cam.ScreenToWorldPoint(mousePosition3) ;
		}
	}
    
}
