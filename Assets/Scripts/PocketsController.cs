using UnityEngine;
using System.Collections;

public class PocketsController : MonoBehaviour {
	public GameObject redBalls;
	public GameObject cueBall;
	private PoolGameController gameController;

	private Vector3 originalCueBallPosition;

	void Start() {
		originalCueBallPosition = cueBall.transform.position;
		if (PoolGameController.GameInstance != null)
			gameController = PoolGameController.GameInstance;
	}


	void OnCollisionEnter(Collision collision) {
		foreach (var transform in redBalls.GetComponentsInChildren<Transform>()) {
			if (transform.name == collision.gameObject.name) {
				var objectName = collision.gameObject.name;
				GameObject.Destroy(collision.gameObject);
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
