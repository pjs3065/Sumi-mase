using UnityEngine;
using System.Collections;

namespace GameStates {
	public class WaitingForStrikeState  : AbstractGameObjectState {
		private GameObject cue;
		private GameObject cueBall;
		private GameObject cueBall2;
		private GameObject mainCamera;

		private PoolGameController gameController;

		public WaitingForStrikeState(MonoBehaviour parent) : base(parent) { 
			//처음스테이트 큣대보임, 게임오브젝트 받아옴

			gameController = (PoolGameController)parent;
			cue = gameController.cue;
			cueBall = gameController.cueBall;
			cueBall2 = gameController.cueBall2;
			mainCamera = gameController.mainCamera;


			//큣대 매시 렌더링 보임
			cue.GetComponent<Renderer>().enabled = true;

            //스코어가 20점이 넘으면 게임 끝
            if (gameController.CurrentPlayer.score >= 10)
                gameController.EndMatch();
		}

		public override void Update() { //Update는 이벤트 발생시 실시간 업데이트
			var x = Input.GetAxis("Horizontal");
			//키보드 left, right, a, d로 x값 얻어옴
			
			if (x != 0) {
				var angle = x * 75 * Time.deltaTime;
				//사원수 회전 Quaternion.AngleAxis
				gameController.strikeDirection = Quaternion.AngleAxis(angle, Vector3.up) * gameController.strikeDirection;
				if (PoolGameController.playerCheck == 1) {
					mainCamera.transform.RotateAround (cueBall.transform.position, Vector3.up, angle);
					cue.transform.RotateAround (cueBall.transform.position, Vector3.up, angle);
				} else if(PoolGameController.playerCheck == 2) {
					mainCamera.transform.RotateAround (cueBall2.transform.position, Vector3.up, angle);
					cue.transform.RotateAround (cueBall2.transform.position, Vector3.up, angle);
				}
			}
			if (PoolGameController.playerCheck == 1) {
				Debug.DrawLine (cueBall.transform.position, cueBall.transform.position + gameController.strikeDirection * 10);
			} else if(PoolGameController.playerCheck == 2) {
				Debug.DrawLine (cueBall2.transform.position, cueBall2.transform.position + gameController.strikeDirection * 10);
			}

			if (Input.GetButtonDown("Fire1")) {
				gameController.currentState = new GameStates.StrikingState(gameController);
				//스트라이킹 으로 감
			}
		}
	}
}