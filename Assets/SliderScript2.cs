using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class DragEnded : EventArgs
{
	public bool value = false;
	public DragEnded(bool v)
	{
		value = v;
	}
}
public class SliderScript2 : MonoBehaviour {
	public static SliderScript2 Instance;
	public event EventHandler handlerEndDrag;


	private void Awake()
	{
		Instance = this;
		
	}
	public void OnBeginDrag()
	{
		if (handlerEndDrag != null) {
			handlerEndDrag (this,new DragEnded(false));
		}
	}
	public void OnDrag()
	{
		if (handlerEndDrag != null) {
			handlerEndDrag (this,new DragEnded(false));
		}
	}
	public void OnEndDrag()
	{
		if (handlerEndDrag != null) {
			Debug.LogError ("calling is done ");
			handlerEndDrag (this,new DragEnded(true));
		}
	}

	public void OnSliderValueChanged()
	{
		
	}
}
