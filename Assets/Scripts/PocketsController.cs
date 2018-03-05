﻿using UnityEngine;
using System.Collections;

public class PocketsController : MonoBehaviour {
	public GameObject redBalls;
	public GameObject cueBall;
	private PoolGameController gameController;
	private GameObject collectionBox;
	private Vector3 originalCueBallPosition;

	void Start() {
		originalCueBallPosition = cueBall.transform.position;
		if (PoolGameController.GameInstance != null)
			gameController = PoolGameController.GameInstance;

		collectionBox = GameObject.FindGameObjectWithTag ("CollectionBox");
		if (collectionBox == null) {
			Debug.LogError ("Not found");
		}
	}


	void OnCollisionEnter(Collision collision) {
		foreach (var transform in redBalls.GetComponentsInChildren<Transform>()) {
			if (transform.name == collision.gameObject.name) {

				var objectName = collision.gameObject.name;
				//GameObject.Destroy(collision.gameObject);
				collision.gameObject.transform.parent=collectionBox.transform;
				collision.gameObject.SetActive (false);
				collision.gameObject.transform.localPosition = new Vector3 (0, 0, 0);
			//	Debug.LogError ("collision is happening ");
				var ballNumber = int.Parse(objectName.Replace("Ball", ""));
	//			Debug.LogError (ballNumber);
				PoolGameController.GameInstance.BallPocketed(ballNumber);
			}
		}

		if (cueBall.transform.name == collision.gameObject.name) {
			cueBall.transform.position = originalCueBallPosition;
			Debug.Log ("Cue Ball was potted ");
			cueBall.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			PoolGameController.GameInstance.check = 1;
		//	gameController.currentState = new GameStates.PlaceCueBallState (gameController);
		//	gameController.gameState = StatesOfGame.PLACE_CUE_BALL_STATE;
			gameController.cueBallWasPotted = true;
		}
	}
}
