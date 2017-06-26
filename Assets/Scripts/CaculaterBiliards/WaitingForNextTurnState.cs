using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameStates
{
    public class WaitingForNextTurnState : AbstractGameObjectState
    {
        private PoolGameController gameController;

        private GameObject cue;
        private GameObject cueBall;
        private GameObject cueBall2;
        private GameObject redBall;
        private GameObject redBall2;
        private GameObject mainCamera;

        private Vector3 cameraOffset;
        private Vector3 cueOffset;
        private Quaternion cameraRotation;
        private Quaternion cueRotation;

        public WaitingForNextTurnState(MonoBehaviour parent)
            : base(parent)
        {
            //공이 처지고 카메라 공 따라가고 멈춤 기다림.
            gameController = (PoolGameController)parent;

            cue = gameController.cue;
            cueBall = gameController.cueBall;
            cueBall2 = gameController.cueBall2;
            redBall = gameController.redBall;
            redBall2 = gameController.redBall2;
            mainCamera = gameController.mainCamera;

            if (PoolGameController.playerCheck == 1)
            {
                cameraOffset = cueBall.transform.position - mainCamera.transform.position;
                cueOffset = cueBall.transform.position - cue.transform.position;
            }
            else if (PoolGameController.playerCheck == 2)
            {
                cameraOffset = cueBall2.transform.position - mainCamera.transform.position;
                cueOffset = cueBall2.transform.position - cue.transform.position;
            }
            //움직일 경우 카메라를 로테이션한다.
            cameraRotation = mainCamera.transform.rotation;
            cueRotation = cue.transform.rotation;
        }

        public override void FixedUpdate()
        {
            var cueBallBody = cueBall.GetComponent<Rigidbody>();
            var cueBall2Body = cueBall2.GetComponent<Rigidbody>();
            var redBallBody = redBall.GetComponent<Rigidbody>();
            var redBall2Body = redBall2.GetComponent<Rigidbody>();
            //공모두 멈추면 끝
            if (!(cueBallBody.IsSleeping() || cueBallBody.velocity == Vector3.zero))
                return;
            if (!(cueBall2Body.IsSleeping() || cueBall2Body.velocity == Vector3.zero))
                return;
            if (!(redBall2Body.IsSleeping() || redBall2Body.velocity == Vector3.zero))
                return;
            if (!(redBallBody.IsSleeping() || redBallBody.velocity == Vector3.zero))
                return;

            //Debug.Log("ALL ball is not moving");

            gameController.NextPlayer();    //플레이어 바꿈.

            //플레이어가 바뀐후 카메라를 한번더 확인을 해준다. 카메라를 이동시키기위함
            if (PoolGameController.playerCheck == 1)
            {
                mainCamera.transform.position = cueBall.transform.position - cameraOffset;
                cue.transform.position = cueBall.transform.position - cueOffset;
            }
            else if (PoolGameController.playerCheck == 2)
            {
                mainCamera.transform.position = cueBall2.transform.position - cameraOffset;
                cue.transform.position = cueBall2.transform.position - cueOffset;
            }

            gameController.currentState = new WaitingForStrikeState(gameController);
            //스테이트 바꿈.
        }

        public override void LateUpdate()
        {
            //Debug.Log(PoolGameController.playerCheck + " Change cue position");

            //나중 업데이트 초기화
            if (PoolGameController.playerCheck == 1)
            {
                mainCamera.transform.position = cueBall.transform.position - cameraOffset;
                cue.transform.position = cueBall.transform.position - cueOffset;
            }
            else if (PoolGameController.playerCheck == 2)
            {
                mainCamera.transform.position = cueBall2.transform.position - cameraOffset;
                cue.transform.position = cueBall2.transform.position - cueOffset;
            }
            mainCamera.transform.rotation = cameraRotation;
            cue.transform.rotation = cueRotation;
        }
    }
}