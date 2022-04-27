using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace draganddrop
{
	public class Obj
	{
		public GameObject obj;
		public int index;
		public float inspectionTime;

		public Obj(GameObject _obj, int _index, float _inspectionTime)
		{
			obj = _obj;
			index = _index;
			inspectionTime = _inspectionTime;
		}
	}

	public class DragDropInitializer
	{
		public GameObject[] InitializeWithTag(string _tag)
		{
			GameObject[] _gameobject = GameObject.FindGameObjectsWithTag(_tag);
			return _gameobject;
		}

		public Obj[] InitializeObj(GameObject[] gameobjects, int _nb_elements)
		{
			Obj[] objList = new Obj[_nb_elements];
			int index = 0;

			foreach (GameObject go in gameobjects)
			{
				objList[index] = new Obj(go, index, 0f);
				index += 1;
			}

			return objList;
		}

	}

	public class DragDropSelection
	{
		//Give information about object 1 and object 2 it was placed upon. 
		//This function does not work as a standalone. It only is a subpart of ClickedSelection().
		public GameObject[] Selected(string buttonCode, string tag1, string tag2, int layer, Camera cam)
		{
			GameObject[] selection = new GameObject[2];

			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit1, hit2;
			if (Physics.Raycast(ray, out hit1) && hit1.collider.tag == tag1)
			{
				selection[0] = hit1.collider.gameObject;
				if (Physics.Raycast(ray, out hit2, Mathf.Infinity, layer) && hit2.collider.tag == tag2)
				{
					selection[1] = hit2.collider.gameObject;
				}
				else
				{
					selection[1] = null;
				}
			}
			else
			{
				selection[0] = null;
				selection[1] = null;
			}
			return selection;
		}

		public GameObject[] ClickedSelection(string buttonCode, bool downPress, string tag1, string tag2, int layer, Camera cam)
		{
			GameObject[] clickedSelection = new GameObject[2];
			if (downPress)
			{
				if (Input.GetButtonDown(buttonCode))
				{
					clickedSelection = Selected(buttonCode, tag1, tag2, layer, cam);
				}
			}
			if (!downPress)
			{
				if (Input.GetButtonUp(buttonCode))
				{
					clickedSelection = Selected(buttonCode, tag1, tag2, layer, cam);
				}
			}

			return clickedSelection;
		}

		public bool[] RayCastClicked(string buttonCode, bool downPress, string tag1, string tag2, int layer, Camera cam)
		{
			GameObject[] clickedSelection = ClickedSelection(buttonCode, downPress, tag1, tag2, layer, cam);
			bool[] clicked = new bool[clickedSelection.Length];

			for (int i = 0; i < clicked.Length; ++i)
			{
				clicked[i] = clickedSelection[i] != null;
			}
			return clicked;
		}
	}

	public class ObjFinder
	{
		public Obj FindAssociatedObj(GameObject _gObject, Obj[] _objArray)
		{
			Obj associatedObj = null;

			if (_gObject != null)
			{
				foreach (Obj objct in _objArray)
				{
					if (_gObject == objct.obj)
					{
						associatedObj = objct;
					}
				}
				return associatedObj;
			}
			else
			{
				return null;
			}
		}
		public void PlaceInIndexedSlot(GameObject dataset, Obj[] datasetList, Obj[] slotList)
		{
			Obj datasetObj = null;

			datasetObj = FindAssociatedObj(dataset, datasetList);

			foreach (Obj b in slotList)
			{
				if (datasetObj.index == b.index)
				{
					dataset.transform.position = b.obj.transform.position;
				}
			}
		}
	}
}