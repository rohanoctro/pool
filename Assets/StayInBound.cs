using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInBound : MonoBehaviour {
	private Vector3 orignalPosition;
	private GameObject obj;
	private bool outOfBound;
	// Use this for initialization
	void Start () {
		outOfBound = false;
		obj = GameObject.FindGameObjectWithTag ("Finish");
		if (obj != null) {
			Debug.Log("Found cube");
			Debug.Log(obj.transform.position);

			Debug.Log (obj.transform.localPosition);
		}
		orignalPosition = gameObject.transform.position;

		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.z>=40.0f || gameObject.transform.position.z<=-40 || gameObject.transform.position.x>=22 || gameObject.transform.position.x<=-22)
		{
			gameObject.transform.position = orignalPosition;
		}
		
	}
}
