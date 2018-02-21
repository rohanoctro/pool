using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace GameStates {
	public class WaitingForStrikeState  : AbstractGameObjectState {
		private GameObject cue;
		private GameObject cueBall;
		private GameObject mainCamera;
		private bool isDragEnded = true;

		private GameObject image;
		private bool found=false;



		private PoolGameController gameController;

		private GameObject imagePanel;



		public WaitingForStrikeState(MonoBehaviour parent) : base(parent) { 
			gameController = (PoolGameController)parent;

			cue = gameController.cue;
			cueBall = gameController.cueBall;
			mainCamera = gameController.mainCamera;
			cue.GetComponent<Renderer>().enabled = true;
			image = gameController.image;
			if (MoveImageScript.Instance != null) {
				MoveImageScript.Instance.handler += OnDragEvent;
			} else
				Debug.LogError ("NOT INITIALIZED ");
			imagePanel = gameController.imagePanel;
			imagePanel.SetActive (true);
		}
		private void OnDragEvent(object sender,EventArgs arguments)
		{
			DragEnded drag = (DragEnded)arguments;
			isDragEnded = drag.value;
		}
		public override void Update() {
			if(MoveImageScript.Instance!=null &&found==false)
				MoveImageScript.Instance.handler+=OnDragEvent;
			float factor = PlayerPrefs.GetFloat ("factor");
		//	var x = Input.GetAxis("Horizontal");

			var x = 0.0f;
			if (Input.touches.Length > 0 &&Input.GetTouch(0).phase==TouchPhase.Moved) {
				x = Input.GetTouch (0).deltaPosition.x;			
				if (Input.GetTouch (0).deltaPosition.y != 0) {
					x = x + Input.GetTouch (0).deltaPosition.y;
				}
			}
			x = (-1.0f )* (x);
				
		//	debugger.GetComponent<Text> ().text = "value Of X: "+x.ToString()+"value of Y: "+Input.GetTouch(0).deltaPosition.y.ToString();
			if (x != 0 && isDragEnded==true) {
				var angle = x * 2.0f * Time.deltaTime;
				gameController.strikeDirection = Quaternion.AngleAxis(angle, Vector3.up) * gameController.strikeDirection;
				mainCamera.transform.RotateAround(cueBall.transform.position, Vector3.up, angle);
				cue.transform.RotateAround(cueBall.transform.position, Vector3.up, angle);
			}
			Debug.DrawLine(cueBall.transform.position, cueBall.transform.position + gameController.strikeDirection * 10);
			if (isDragEnded == true && factor!=0) {
				gameController.factor = factor;
				gameController.currentState = new GameStates.StrikingState(gameController);
			}
		/*	if (Input.GetButtonDown("Fire1")) {



				gameController.currentState = new GameStates.StrikingState(gameController);
			}*/
			factor = 0;
			PlayerPrefs.SetFloat ("factor",0);
		}
	}
}