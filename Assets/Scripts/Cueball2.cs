using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cueball2 : MonoBehaviour
{

    void Start()
    {
        GetComponent<Rigidbody>().sleepThreshold = 0.15f;
    }

    //흰공의 물리적 힘(속도)
    void FixedUpdate()
    {
        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody.velocity.y > 0)
        {
            var velocity = rigidbody.velocity;
            velocity.y *= 0.3f;
            rigidbody.velocity = velocity;
        }
    }

    //흰공이 공을 맞췄을 때 발생하는 이벤트
    public void OnCollisionEnter(Collision col)
    {
        //빨간공1을 맞출 경우
        if (col.gameObject.name == "Redball")
            PoolGameController.GameInstance.cue2CollUp();
        
        //빨간공2를 맞출 경우
        else if (col.gameObject.name == "Redball2")
            PoolGameController.GameInstance.cue2Coll2Up();
        
        //상대팀의 공을 맞출 경우
        else if (col.gameObject.name == "CueBall")
            PoolGameController.GameInstance.cue2CheckDown();

        //맞은 결과를 계산
        PoolGameController.GameInstance.BallColl();
    }

}