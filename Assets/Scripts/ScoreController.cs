using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	public Text player1;
	public Text player2;
	private Player Player1;
	private Player Player2;
	int c1=0;
	int c2=0;
	// Use this for initialization
	void Awake()
	{
		

	}
	void Start () {



	}

	
	// Update is called once per frame
	void Update () {
		if (PoolGameController.GameInstance.CurrentPlayer != null && c1 == 0) {
			Player1 = PoolGameController.GameInstance.CurrentPlayer;
			c1 = 1;
		}
		if (PoolGameController.GameInstance.OtherPlayer != null && c2==0) {
			Player2 = PoolGameController.GameInstance.OtherPlayer;
			c2 = 1;
		}
		//var text = GetComponent<UnityEngine.UI.Text>();
		var currentPlayer = PoolGameController.GameInstance.CurrentPlayer;
		var otherPlayer = PoolGameController.GameInstance.OtherPlayer;
	//	text.text = String.Format("* {0} - {1}\n{2} - {3}", currentPlayer.Name, currentPlayer.Points, otherPlayer.Name, otherPlayer.Points);
		player1.text = Player1.Name+":"+Player1.Points.ToString();
		player2.text = Player2.Name + ":" + Player2.Points.ToString ();
	}
}
