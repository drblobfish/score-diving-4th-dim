using System.Collections;
using System.Collections.Generic;
using UnityEngine;	

namespace draganddrop.raycast
{
	public class DragDropRaycast : MonoBehaviour
	{

		//Material of an occupied and inoccupied slot:
		public Material slotMaterial ;
		public Camera cam ;
		public bool selection = false ;
		GameObject selected ;

		GameObject empty ;
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

		void PlaceInIndexedSlot(GameObject dataset, Obj[] datasetList, Obj[] slotList)
		{
			Obj datasetObj = null ;

			foreach (Obj a in datasetList)
	        {
	            if (dataset.transform.position == a.obj.transform.position)
	        	{
	           		datasetObj = a ;
	            }
	        }
	        foreach (Obj b in slotList)
	        {
	            if (datasetObj.index == b.index)
	            {
	                dataset.transform.position = b.obj.transform.position ;
	            }
	        }
		}

		void Update()
		{
			if (Input.GetButtonDown("Fire2"))
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
	            RaycastHit hit, slotRay ;
	 
	            if (Physics.Raycast(ray, out hit))
	            {
	                if (hit.collider.tag == "Dataset")
	                {
	                    selected = hit.collider.gameObject ;
	                    PlaceInIndexedSlot(selected, datasets, propositionSlots) ;
	                    selected = empty ;
	                }
	            }

				//Check if the dataset was on a slot:
				if (Physics.Raycast(ray, out slotRay, Mathf.Infinity, 9))
	            {
	                if (slotRay.collider.tag == "Slot")
	                {
						//Then change the color of the slot to inoccupied state:
						slotMaterial = slotRay.collider.gameObject.GetComponent<Renderer>().material ;
						slotMaterial.color = Color.red ;
					}
				}
			}

			if (Input.GetButtonDown("Fire1") && !doubleClick.Onfocus)
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
	            RaycastHit hit, slotRay ;
	 
	            if (Physics.Raycast(ray, out hit))
	            {
	                if (hit.collider.tag == "Dataset")
	                {
	                    selected = hit.collider.gameObject ;
	                    selection = true;
	                }
	            }
				//Check if the dataset was on a slot:
				if (Physics.Raycast(ray, out slotRay, Mathf.Infinity, 9))
	            {
	                if (slotRay.collider.tag == "Slot")
	                {
						//Then change the color of the slot to inoccupied state:
						slotMaterial = slotRay.collider.gameObject.GetComponent<Renderer>().material ;
						slotMaterial.color = Color.red ;
					}
				}
			}

			if (Input.GetButtonUp("Fire1") && selection && !doubleClick.Onfocus)
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
	            RaycastHit hit;

				if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
	            {
	                if (hit.collider.tag == "Slot")
	                {
						//Change the appearance of the slot when object is placed in it:
						slotMaterial = hit.collider.gameObject.GetComponent<Renderer>().material ;
						slotMaterial.color = Color.blue ; 

	                    selected.transform.position = hit.transform.position ;
	                    selection = false;
						selected = empty ;
	                }
	            }
	            else
	           	{	
					mousePosition3 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y) ;
					selected.transform.position = cam.ScreenToWorldPoint(mousePosition3) ;
					selected = empty ;
					selection = false ;
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