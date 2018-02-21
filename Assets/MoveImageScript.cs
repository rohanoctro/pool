using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MoveImageScript : MonoBehaviour {

	private RectTransform orignal;
	private bool found=false ;
	private Vector3 oldPositon;
	private Vector3 orignalPosition;
	public event EventHandler handler;
	public static MoveImageScript Instance;
	private bool toLerp=false;
	private Vector3 mousePosition;
	private GameObject debugger;

	IEnumerator LerpMe(Vector3 positionTo)
	{
		Vector3 newPosition = gameObject.GetComponent<RectTransform> ().position;
		while (newPosition.y <= positionTo.y) {
			newPosition.y = Mathf.Lerp (orignal.position.y, positionTo.y, 0.07f);
			gameObject.GetComponent<RectTransform> ().position = newPosition;
		}
		yield return null;
	}



	public void Start()
	{

		Instance = this;
		PlayerPrefs.SetFloat ("factor",0);
		orignal = gameObject.GetComponent<RectTransform> ();
		Debug.LogError (orignal.name);
		orignalPosition = orignal.position;
		Debug.Log (orignalPosition);
		if (EventScript.Instance != null) {
			EventScript.Instance.mouseEvent += dragMe;
			found = true;
		} else {
			Debug.Log ("Done ");

		}
		debugger = GameObject.FindGameObjectWithTag ("Finish");
		debugger.GetComponent<Text> ().text = "";
	}
	private void dragMe(object sender,EventArgs arguments){
		
		//gameObject.GetComponent<RectTransform> ().position = new Vector3 (gameObject.GetComponent<RectTransform>().position.x,Input.mousePosition.y,gameObject.GetComponent<RectTransform>().position.z);
		DragEvent drag=(DragEvent)arguments;
		switch (drag.typeOfEvent) {
		case 0:
			{
				if (handler != null) {
					handler (this, new DragEnded (false));
				}
				//	Debug.Log ("Drag Began ");
				break;
			}
		case 1:
			{
				//orignal.position = new Vector3 (orignal.position.x, Input.mousePosition.y, orignal.position.z);
				if (orignal.localPosition.y > 0)
					orignal.position = orignalPosition;
				Debug.Log (gameObject.GetComponent<RectTransform> ().localPosition);
				Debug.Log (orignal.localPosition);
				Debug.Log (orignal.position);
			//	debugger.GetComponent<Text>().text=Input.touches.Length.ToString();
				if (Input.touches.Length > 0) {
					if (Input.GetTouch (0).phase == TouchPhase.Moved) {
				//		debugger.GetComponent<Text>().text += Input.GetTouch (0).deltaPosition.ToString();
						float deltaY = Input.GetTouch (0).deltaPosition.y;
						orignal.position = new Vector3 (orignal.position.x,orignal.position.y+deltaY,orignal.position.z);

					}
				}
				if (handler != null) {
					handler (this, new DragEnded (false));
				}
				//	Debug.Log ("Constantly dragging ");
				break;
			}
		case 2:
			{
				float factor = orignal.localPosition.y;
				if (factor < 0)
					factor = (-1) * factor;
				factor = factor / 557.0f;
				Debug.LogError (factor + "is the factor");
				PlayerPrefs.SetFloat ("factor", factor);
				Debug.LogError (factor + " is the factor value fired ");

				orignal.position = orignalPosition;
				//orignal represents the image object 
				//	Debug.Log ("Drag ended ");
				Debug.Log ("orignal Position 1 " + gameObject.GetComponent<RectTransform> ().localPosition);
				Debug.Log ("Orignal Position " + orignal.localPosition);

				if (handler != null) {
					handler (this, new DragEnded (true));
				}
				break;
			}
		case 3:
			{
		//		toLerp = true;
				mousePosition = Input.mousePosition;
			//	StartCoroutine (LerpMe (mousePosition));
				Debug.Log ("Mouse is pressed ");
				break;	
			}
		case 4:
			{
				Debug.Log ("Mouse is released ");
			//	orignal.position = orignalPosition;
				break;
			}
		case 5:
			{
				//Debug.Log ("Mouse exits ");
				orignal.position = orignalPosition;
				break;
			}
		}
	}


	public void Update()
	{
		if (toLerp == true) {
			
			Vector3 newPosition = gameObject.GetComponent<RectTransform> ().position;
			newPosition.y = Mathf.Lerp (orignal.position.y,mousePosition.y,0.5f);
			gameObject.GetComponent<RectTransform> ().position = newPosition;
			if (newPosition.y <= mousePosition.y)
				toLerp = false;
		}
	//	if (EventScript.Instance != null && found == false) {
	//		EventScript.Instance.mouseEvent += dragMe;
	//		found = true;
	//	}
	
	}


}
