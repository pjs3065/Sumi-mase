using UnityEngine;
using System.Collections;

namespace GameStates
{
    public class StrikingState : AbstractGameObjectState
    {
        private PoolGameController gameController;

        private GameObject cue;
        private GameObject cueBall;
        private GameObject cueBall2;

        private float cueDirection = -1;
        private float speed = 10;

        public StrikingState(MonoBehaviour parent)
            : base(parent)
        {
            gameController = (PoolGameController)parent;
            cue = gameController.cue;
            cueBall = gameController.cueBall;
            cueBall2 = gameController.cueBall2;

        }

        public override void Update()
        {
            //마우스 때면
            if (Input.GetButtonUp("Fire1"))
            {
                gameController.currentState = new GameStates.StrikeState(gameController);
                //스트라이크로 감
            }
        }

        public override void FixedUpdate()
        {
            //일정 주기로 업데이트 ex리스너
            if (PoolGameController.playerCheck == 1)
            {

                //a, b의 상대거리 큣대와 공의 거리
                var distance = Vector3.Distance(cue.transform.position, cueBall.transform.position);
                if (distance < PoolGameController.MIN_DISTANCE || distance > PoolGameController.MAX_DISTANCE)
                {
                    cueDirection *= -1; //큣대 이동방향 앞뒤 변경
                }
                cue.transform.Translate(Vector3.down * speed * cueDirection * Time.fixedDeltaTime);
                //큣대 이동 주기적 이동
            }
            else if (PoolGameController.playerCheck == 2)
            {
                var distance = Vector3.Distance(cue.transform.position, cueBall2.transform.position);
                //a, b의 상대거리 큣대와 공의 거리
                if (distance < PoolGameController.MIN_DISTANCE || distance > PoolGameController.MAX_DISTANCE)
                {
                    cueDirection *= -1; //큣대 이동방향 앞뒤 변경
                }
                cue.transform.Translate(Vector3.down * speed * cueDirection * Time.fixedDeltaTime);
                //큣대 이동 주기적 이동
            }
        }
    }
}