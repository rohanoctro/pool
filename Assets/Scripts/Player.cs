using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player {
	private IList<Int32> ballsCollected = new List<Int32>();
	private GameObject panel;



	public Player(string name,string panelName) {
		Name = name;
		panel = GameObject.Find (panelName);
		if (panel == null) {
			Debug.LogError ("Error occure panel not found for "+name);
		}

	
	}

	public string Name {
		get;
		private set;
	}

	public int Points {
		get { return ballsCollected.Count; }
	}

	public void Collect(int ballNumber) {
		Debug.Log(Name + " collected ball " + ballNumber);
		ballsCollected.Add(ballNumber);
		Transform ithChlid = panel.transform.GetChild (ballsCollected.Count - 1);
		Transform childText = ithChlid.GetChild (0);
		childText.GetComponent<Text>().text = ballNumber.ToString ();
	}
}
