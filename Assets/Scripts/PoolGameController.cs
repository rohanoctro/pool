using UnityEngine;
using System.Collections;
using System;

public class PoolGameController : MonoBehaviour {
	public GameObject cue;
	public GameObject cueBall;
	public GameObject redBalls;
	public GameObject mainCamera;
	public GameObject scoreBar;
	public GameObject winnerMessage;
	public static Vector3 cuePosition;
	public float factor;

	public GameObject image;
	public GameObject imagePanel;



	public float maxForce;
	public float minForce;
	public Vector3 strikeDirection;

	public const float MIN_DISTANCE = 27.5f;
	public const float MAX_DISTANCE = 32f;
	
	public IGameObjectState currentState;

	public Player CurrentPlayer;
	public Player OtherPlayer;

	private bool currentPlayerContinuesToPlay = false;

	// This is kinda hacky but works
	static public PoolGameController GameInstance {
		get;
		private set;
	}

	void Start() {
		factor = 0;
		cuePosition =Vector3.zero;
		strikeDirection = Vector3.forward;
		CurrentPlayer = new Player("John");
		OtherPlayer = new Player("Doe");
		imagePanel = GameObject.FindGameObjectWithTag ("imagePanel");
		imagePanel.SetActive (false);
		image = GameObject.FindGameObjectWithTag ("cueImage");

		GameInstance = this;
		winnerMessage.GetComponent<Canvas>().enabled = false;

		currentState = new GameStates.WaitingForStrikeState(this);
	}
	
	void Update() {
		currentState.Update();
	}

	void FixedUpdate() {
		currentState.FixedUpdate();
	}

	void LateUpdate() {
		currentState.LateUpdate();
	}

	public void BallPocketed(int ballNumber) {
		currentPlayerContinuesToPlay = true;
		CurrentPlayer.Collect(ballNumber);
	}

	public void NextPlayer() {
		if (currentPlayerContinuesToPlay) {
			currentPlayerContinuesToPlay = false;
			Debug.Log(CurrentPlayer.Name + " continues to play");
			return;
		}

		Debug.Log(OtherPlayer.Name + " will play");
		var aux = CurrentPlayer;
		CurrentPlayer = OtherPlayer;
		OtherPlayer = aux;
	}

	public void EndMatch() {
		Player winner = null;
		if (CurrentPlayer.Points > OtherPlayer.Points)
			winner = CurrentPlayer; 
		else if (CurrentPlayer.Points < OtherPlayer.Points)
			winner = OtherPlayer;

		var msg = "Game Over\n";

		if (winner != null)
			msg += string.Format("The winner is '{0}'", winner.Name);
		else
			msg += "It was a draw!";

		var text = winnerMessage.GetComponentInChildren<UnityEngine.UI.Text>();
		text.text = msg;
		winnerMessage.GetComponent<Canvas>().enabled = true;
	}
	public void OnClickLeftButton()
	{
		PlayerPrefs.SetFloat ("MoveCue",1.0f);
	}
	public void OnClickRightButton()
	{
		PlayerPrefs.SetFloat ("MoveCue",-1.0f);
	}
	public void OnClickStop()
	{
		PlayerPrefs.SetFloat ("MoveCue",0);
	}

}
