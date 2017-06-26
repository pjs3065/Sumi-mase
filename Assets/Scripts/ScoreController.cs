using UnityEngine;
using System;
using System.Collections;

public class ScoreController : MonoBehaviour {

	void Start () {
	
	}
	
	//현재 게임 진행하는 플레이어와 점수를 나타낸다.
	void Update () {
		var text = GetComponent<UnityEngine.UI.Text>();
		var currentPlayer = PoolGameController.GameInstance.CurrentPlayer;
		var otherPlayer = PoolGameController.GameInstance.OtherPlayer;
		text.text = String.Format("* {0} - {1}\n{2} - {3}", currentPlayer.Name, currentPlayer.Points, otherPlayer.Name, otherPlayer.Points);
	}
}
