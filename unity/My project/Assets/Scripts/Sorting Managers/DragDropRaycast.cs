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
		
		Vector3 mousePosition3 ;

		public int nb_datasets ;
		private int sortedDatasets ;
		public bool showButtons ;
		public GameObject[] datasetsGO ;
		Obj[] propositionSlots ;
		public Obj[] datasets ;
		DragDropInitializer initializer = new DragDropInitializer() ;
		DragDropSelection selector = new DragDropSelection() ;
		ObjFinder objFinder = new ObjFinder() ;
		public OnDoubleClickFocus doubleClick ;	
		
		void Start()
		{
			propositionSlots = initializer.InitializeObj("Proposition_Slot", nb_datasets, null) ;
			datasets = initializer.InitializeObj("Dataset", nb_datasets, null) ;
		}

		void SetToNull()
		{
			selected = null ;
			dragging = false ;
			selectedObj = null ;
		}
		
		void Update()
		{
			//If a dataset is right clicked, move it to its associated slot and change the latter's color.
			rightClicked = selector.RayCastClicked("Fire2", true, "Dataset", "Slot", 9, cam) ;
			if (rightClicked[0])
			{
				selected = selector.ClickedSelection("Fire2", true, "Dataset", "Slot", 9, cam)[0] ;
				objFinder.PlaceInIndexedSlot(selected, datasets, propositionSlots) ;
				SetToNull() ;
				if (rightClicked[1])
				{
					//Then change the color of the slot to unoccupied state:
					slotMaterial = selector.ClickedSelection("Fire2", true, "Dataset", "Slot", 9, cam)[1].GetComponent<Renderer>().material ;
					slotMaterial.color = Color.red ;
					sortedDatasets -= (sortedDatasets!=0)?1:0 ;
				}
				//Change focus to false if the object was focused on:
				doubleClick.Onfocus = doubleClick.Onfocus?false:false ;
			}

			//If a dataset is left clicked, enable dragging and change its slot's color.
			leftClicked = selector.RayCastClicked("Fire1", true, "Dataset", "Slot", 9, cam) ;
			if (leftClicked[0])
			{
				selected = selector.ClickedSelection("Fire1", true, "Dataset", "Slot", 9, cam)[0] ;
				dragging = true ;
				if (leftClicked[1])
				{
					//Then change the color of the slot to unoccupied state:
					slotMaterial = selector.ClickedSelection("Fire1", true, "Dataset", "Slot", 9, cam)[1].GetComponent<Renderer>().material ;
					slotMaterial.color = Color.red ;
					sortedDatasets -= (sortedDatasets!=0)?1:0 ;
				}
			}

			//The dropping part of the drag and drop mechanic.
			upClicked = selector.RayCastClicked("Fire1", false, "Dataset", "Slot", 9, cam) ;
			if (!doubleClick.Onfocus && upClicked[0])
			{

				if (upClicked[1])
				{
					selected.transform.position = selector.ClickedSelection("Fire1", false, "Dataset", "Slot", 9, cam)[1].transform.position ;
					SetToNull() ;

					//Change the appearance of the slot when object is placed in it:
					slotMaterial = selector.ClickedSelection("Fire1", false, "Dataset", "Slot", 9, cam)[1].GetComponent<Renderer>().material ;
					slotMaterial.color = Color.blue ; 
					sortedDatasets += (sortedDatasets<8)?1:0 ;
				}
				else if (selector.RayCastClicked("Fire1", false, "Dataset", "Proposition_Slot", 9, cam)[1])
				{
					objFinder.PlaceInIndexedSlot(selected, datasets, propositionSlots) ;
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
				selectedObj = objFinder.FindAssociatedObj(selected, datasets) ;
				datasets[selectedObj.index].inspectionTime += Time.deltaTime ;	
			}
			//When all datasets placed in our 7 slots.
			showButtons = sortedDatasets == 7 ;
		}
	}
}