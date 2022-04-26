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
		private bool dragging = false ;
		private bool[] leftClicked, rightClicked, upClicked ;
		private GameObject selected ;
		private Obj selectedObj ;
		
		private static Obj emptyObj = new Obj(null, 0, 0) ;
		Vector3 mousePosition3 ;

		public int nb_datasets ;
		private int sortedDatasets ;
		public bool showButtons ;
		Obj[] propositionSlots ;
		public Obj[] datasets ;
		DragDropInitializer initializer = new DragDropInitializer() ;
		public OnDoubleClickFocus doubleClick ;	
		
		void Start()
		{
			propositionSlots = initializer.InitializeObj("Proposition_Slot", nb_datasets) ;
			datasets = initializer.InitializeObj("Dataset", nb_datasets) ;
		}

		Obj FindAssociatedObj(GameObject _gObject, Obj[] _objArray)
		{
			Obj associatedObj = null ;
			
			if (_gObject != null)
			{
				foreach (Obj objct in _objArray)
	        	{
					if (_gObject == objct.obj)
	        		{
	           			associatedObj = objct ;
	           		}
	        	}
				return associatedObj ;
			}
			else
			{
				return emptyObj ;
			}
		}
		void PlaceInIndexedSlot(GameObject dataset, Obj[] datasetList, Obj[] slotList)
		{
			Obj datasetObj = null ;

			datasetObj = FindAssociatedObj(dataset, datasetList) ;

	        foreach (Obj b in slotList)
	        {
	            if (datasetObj.index == b.index)
	            {
	                dataset.transform.position = b.obj.transform.position ;
	            }
	        }
		}

		void SetToNull()
		{
			selected = null ;
			dragging = false ;
			selectedObj = null ;
		}
		
		//Give information about object 1 and object 2 it was placed upon. 
		//This function does not work as a standalone. It only is a subpart of ClickedSelection().
		GameObject[] Selected(string buttonCode, string tag1, string tag2, int layer)
		{
			GameObject[] selection = new GameObject[2];

			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit1, hit2 ;
			if (Physics.Raycast(ray, out hit1) && hit1.collider.tag == tag1)
			{
				selection[0] = hit1.collider.gameObject ;
				if (Physics.Raycast(ray, out hit2, Mathf.Infinity, layer) && hit2.collider.tag == tag2)
				{
					selection[1] = hit2.collider.gameObject ;
				}
				else
				{
					selection[1] = null ;
				}
			}
			else
			{
				selection[0] = null ;
				selection[1] = null ;
			}
			return selection ;
		}

		GameObject[] ClickedSelection(string buttonCode, bool downPress, string tag1, string tag2, int layer)
		{
			GameObject[] clickedSelection = new GameObject[2];
			if (downPress)
			{
				if (Input.GetButtonDown(buttonCode))
				{	
					clickedSelection = Selected(buttonCode, tag1, tag2, layer) ;
				}
			}
			if (!downPress)
			{
				if (Input.GetButtonUp(buttonCode))
				{	
					clickedSelection = Selected(buttonCode, tag1, tag2, layer) ;
				}
			}

			return clickedSelection ;
		}

		bool[] RayCastClicked(string buttonCode, bool downPress, string tag1, string tag2, int layer)
		{
			GameObject[] clickedSelection = ClickedSelection(buttonCode, downPress, tag1, tag2, layer) ;
			bool[] clicked = new bool[clickedSelection.Length] ;
			
			for (int i = 0 ; i < clicked.Length ; ++ i)
			{
				clicked[i] = clickedSelection[i] != null ;
			}
			return clicked ;
		}

		void Update()
		{
			//If a dataset is right clicked, move it to its associated slot and change the latter's color.
			rightClicked = RayCastClicked("Fire2", true, "Dataset", "Slot", 9) ;
			if (rightClicked[0])
			{
				selected = ClickedSelection("Fire2", true, "Dataset", "Slot", 9)[0] ;
				PlaceInIndexedSlot(selected, datasets, propositionSlots) ;
				SetToNull() ;
				if (rightClicked[1])
				{
					//Then change the color of the slot to unoccupied state:
					slotMaterial = ClickedSelection("Fire2", true, "Dataset", "Slot", 9)[1].GetComponent<Renderer>().material ;
					slotMaterial.color = Color.red ;
					sortedDatasets -= (sortedDatasets!=0)?1:0 ;
				}
			}

			//If a dataset is left clicked, enable dragging and change its slot's color.
			leftClicked = RayCastClicked("Fire1", true, "Dataset", "Slot", 9) ;
			if (leftClicked[0])
			{
				selected = ClickedSelection("Fire1", true, "Dataset", "Slot", 9)[0] ;
				dragging = true ;
				if (leftClicked[1])
				{
					//Then change the color of the slot to unoccupied state:
					slotMaterial = ClickedSelection("Fire1", true, "Dataset", "Slot", 9)[1].GetComponent<Renderer>().material ;
					slotMaterial.color = Color.red ;
					sortedDatasets -= (sortedDatasets!=0)?1:0 ;
				}
			}

			//The dropping part of the drag and drop mechanic.
			upClicked = RayCastClicked("Fire1", false, "Dataset", "Slot", 9) ;
			if (!doubleClick.Onfocus && upClicked[0])
			{

				if (upClicked[1])
				{
					selected.transform.position = ClickedSelection("Fire1", false, "Dataset", "Slot", 9)[1].transform.position ;
					SetToNull() ;

					//Change the appearance of the slot when object is placed in it:
					slotMaterial = ClickedSelection("Fire1", false, "Dataset", "Slot", 9)[1].GetComponent<Renderer>().material ;
					slotMaterial.color = Color.blue ; 
					sortedDatasets += (sortedDatasets<8)?1:0 ;
				}
				else if (RayCastClicked("Fire1", false, "Dataset", "Proposition_Slot", 9)[1])
				{
					PlaceInIndexedSlot(selected, datasets, propositionSlots) ;
					SetToNull() ;
				}
				else if (!doubleClick.Onfocus)
				{
					mousePosition3 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y) ;
					selected.transform.position = cam.ScreenToWorldPoint(mousePosition3) ;
					SetToNull() ;
				}
			}
			

			//If an object is selected, make it follow the mouse's world position.
			if (dragging && !doubleClick.Onfocus)
			{
				mousePosition3 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y) ;
				selected.transform.position = cam.ScreenToWorldPoint(mousePosition3) ;
			}

			//If an object is being inspected, measure the time that is spent focusing on it.
			if (doubleClick.Onfocus)
			{
				selectedObj = FindAssociatedObj(selected, datasets) ;
				datasets[selectedObj.index].inspectionTime += Time.deltaTime ;	
			}
			//When all datasets placed in our 7 slots.
			showButtons = sortedDatasets == 7 ;
			// Debug.Log("Test: " + Selection("Fire1", true, "Dataset", "", 9)) ;
			// Debug.Log("Dragging: " + dragging) ;
		}
	}
}