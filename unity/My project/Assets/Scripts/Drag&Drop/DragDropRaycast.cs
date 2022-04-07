using System.Collections;
using System.Collections.Generic;
using UnityEngine;	

namespace draganddrop.raycast
{
	public class DragDropRaycast : MonoBehaviour
	{

		public Camera cam ;
		public bool selection = false ;
		GameObject selected ;
		Obj selectedObj ;

		GameObject empty ;
		Obj emptyObj ;
		Vector3 mousePosition3 ;

		public int nb_datasets ;
		Obj[] propositionSlots ;
		Obj[] datasets ;
		DragDropInitializer initializer = new DragDropInitializer() ;
		public OnDoubleClickFocus doubleClick ;	
		
		void Start()
		{
			propositionSlots = initializer.InitializeObj("Proposition_Slot", nb_datasets) ;
			datasets = initializer.InitializeObj("Dataset", nb_datasets) ;
		}

		void Update()
		{
			if (Input.GetButtonDown("Fire2") && !doubleClick.Onfocus)
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
	            RaycastHit hit;
	 
	            if (Physics.Raycast(ray, out hit))
	            {
	                if (hit.collider.tag == "Dataset")
	                {
	                    selected = hit.collider.gameObject ;
	                    foreach (Obj a in datasets)
	                    {
	                    	if (selected.transform.position == a.obj.transform.position)
	                    	{
	                    		selectedObj = a ;
	                    	}
	                    }
	                    foreach (Obj b in propositionSlots)
	                    {
	                    	if (selectedObj.index == b.index)
	                    	{
	                    		selected.transform.position = b.obj.transform.position ;
	                    	}
	                    }
	                    selected = empty ;
	                    selectedObj = emptyObj ;
	                }
	            }
			}

			if (Input.GetButtonDown("Fire1") && !doubleClick.Onfocus)
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
	            RaycastHit hit;
	 
	            if (Physics.Raycast(ray, out hit))
	            {
	                if (hit.collider.tag == "Dataset")
	                {
	                    selected = hit.collider.gameObject ;
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
						selected = empty ;
	                }

	            }
	            else
	           	{	
					if (doubleClick.Onfocus)
					{
						foreach (Obj a in datasets)
						{
							if (selected.transform.position == a.obj.transform.position)
							{
								selectedObj = a ;
							}
						}
						foreach (Obj b in propositionSlots)
						{
							if (selectedObj.index == b.index)
							{
								selected.transform.position = b.obj.transform.position ;
							}
						}
						selected = empty ;
						selectedObj = emptyObj ;
						selection = false ;
					}
					if (!doubleClick.Onfocus)
					{
						mousePosition3 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y) ;
						selected.transform.position = cam.ScreenToWorldPoint(mousePosition3) ;
						selected = empty ;
						selectedObj = emptyObj ;
						selection = false ;
					}
				}
					
			}

			if (selection && !doubleClick.Onfocus)
			{
				mousePosition3 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y) ;
				selected.transform.position = cam.ScreenToWorldPoint(mousePosition3) ;
			}
		}
	    
	}
}