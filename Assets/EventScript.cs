using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class DragEvent:EventArgs{
	public int typeOfEvent;
	public DragEvent(int typeOfEvent)
	{
		this.typeOfEvent = typeOfEvent;
	}

}

public class EventScript : MonoBehaviour {


	private RectTransform initialPosition;
	private float initialY;
	private RectTransform finalPosition;
	private float finalY;
	public event EventHandler mouseEvent;
	public static EventScript Instance;



	public void Awake()
	{
		Instance = this;
		Debug.Log ("called ");

	}
	#region DragEventsRegion

		public void OnDragBegin()
		{
			if (mouseEvent != null) {
			mouseEvent (this,new DragEvent(0));
			}
		}
		public void OnDrag()
		{
			if (mouseEvent != null) {
				mouseEvent (this, new DragEvent(1));

			}
		}

		public void OnDragEnd()
		{
			if (mouseEvent != null) {
				mouseEvent (this,new DragEvent(2));
			}	
		}
	#endregion

	#region mouseDownAndUpEvents 

	public void OnMouseDown()
	{
		if (mouseEvent != null) {
			mouseEvent (this,new DragEvent(3));
		}
	}
	public void OnMouseUp()
	{
		if (mouseEvent != null) {
			mouseEvent (this, new DragEvent (4));
		}
	}
	public void OnMouseExit()
	{
		if (mouseEvent != null) {
			mouseEvent (this, new DragEvent (5));
		}
	}

	#endregion
	public void OnDisable()
	{
	}

}






