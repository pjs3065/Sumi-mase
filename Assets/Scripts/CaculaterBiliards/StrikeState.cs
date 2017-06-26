
using UnityEngine;
using System;

namespace GameStates {
	public class StrikeState : AbstractGameObjectState {
		private PoolGameController gameController;
		
		private GameObject cue;
		private GameObject cueBall;
		private GameObject cueBall2;

		private float speed = 30f;
		private float force = 0f;
		
		public StrikeState(MonoBehaviour parent) : base(parent) { 
			//큣대가 공을 침
			gameController = (PoolGameController)parent;
			cue = gameController.cue;
			cueBall = gameController.cueBall;
			cueBall2 = gameController.cueBall2;

			var forceAmplitude = gameController.maxForce - gameController.minForce;

            //플레이어  1의 순서 일경우
			if (PoolGameController.playerCheck == 1) {
				var relativeDistance = (Vector3.Distance (cue.transform.position, cueBall.transform.position) - PoolGameController.MIN_DISTANCE) / (PoolGameController.MAX_DISTANCE - PoolGameController.MIN_DISTANCE);
				force = forceAmplitude * relativeDistance + gameController.minForce;
                //플레이어 2의 순서 일 경우
			} else if(PoolGameController.playerCheck == 2) {
				var relativeDistance2 = (Vector3.Distance (cue.transform.position, cueBall2.transform.position) - PoolGameController.MIN_DISTANCE) / (PoolGameController.MAX_DISTANCE - PoolGameController.MIN_DISTANCE);
				force = forceAmplitude * relativeDistance2 + gameController.minForce;
			}
		}

		public override void FixedUpdate () {
			var distance = Vector3.Distance(cue.transform.position, cueBall.transform.position); //큣대와 공사이의 거리
			var distance2 = Vector3.Distance(cue.transform.position, cueBall2.transform.position);

			if (PoolGameController.playerCheck == 1) {
                //큐가 공을 쳐서 기본 큐와 공의 거리가 늘어 날 경우 (공이 움직인다는 것을 알게 된다.)
				if (distance < PoolGameController.MIN_DISTANCE) {
					cueBall.GetComponent<Rigidbody> ().AddForce (gameController.strikeDirection * force);
					//공에 AddForce로 물리적 힘 가함
					cue.GetComponent<Renderer> ().enabled = false;
					//큣대 안보이게 함
					cue.transform.Translate (Vector3.down * speed * Time.fixedDeltaTime);
					//자신의 축 기준으로 이동 중력
					gameController.currentState = new GameStates.WaitingForNextTurnState (gameController);
				} else {
					cue.transform.Translate (Vector3.down * speed * -1 * Time.fixedDeltaTime);
				}
			} else if(PoolGameController.playerCheck == 2) {
				if (distance2 < PoolGameController.MIN_DISTANCE) {
					cueBall2.GetComponent<Rigidbody> ().AddForce (gameController.strikeDirection * force);
					//공에 AddForce로 물리적 힘 가함
					cue.GetComponent<Renderer> ().enabled = false;
					//큣대 안보이게 함
					cue.transform.Translate (Vector3.down * speed * Time.fixedDeltaTime);
					//자신의 축 기준으로 이동 중력
					gameController.currentState = new GameStates.WaitingForNextTurnState (gameController);
				} else {
					cue.transform.Translate (Vector3.down * speed * -1 * Time.fixedDeltaTime);
				}
			}
		}
	}
}