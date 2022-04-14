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
		private GameObject selected ;
		
		GameObject empty ;
		Vector3 mousePosition3 ;

		public int nb_datasets ;
		private int sortedDatasets ;
		public bool showButtons ;
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
						sortedDatasets -= (sortedDatasets!=0)?1:0 ;
					}
				}
			}

			if (Input.GetButtonDown("Fire1"))
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
	            RaycastHit hit, slotRay ;
	 
	            if (Physics.Raycast(ray, out hit) && !doubleClick.Onfocus)
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
						sortedDatasets -= (sortedDatasets!=0)?1:0 ;
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
						//Change the appearance of the slot when object is placed in it:
						slotMaterial = hit.collider.gameObject.GetComponent<Renderer>().material ;
						slotMaterial.color = Color.blue ; 
						sortedDatasets += (sortedDatasets<8)?1:0 ;

	                    selected.transform.position = hit.transform.position ;
	                    selection = false ;
						selected = empty ;
	                }
					if (hit.collider.tag == "Proposition_Slot")
					{
						PlaceInIndexedSlot(selected, datasets, propositionSlots) ;
						selection = false ;
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
			//When all datasets placed in our 7 slots.
			showButtons = sortedDatasets == 7 ;
			//Debug.Log(sortedDatasets) ;
		}
	    
	}
}