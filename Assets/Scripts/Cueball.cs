using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cueball : MonoBehaviour
{

    void Start()
    {
        GetComponent<Rigidbody>().sleepThreshold = 0.15f;
    }

    //노란공이 칠때의 물리적 힘
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

    // 노란공이 물체를 타격했을때 발생하는 이벤트
    public void OnCollisionEnter(Collision col)
    {
        //빨간공1을 맞췄을 경우 
        if (col.gameObject.name == "Redball")
            PoolGameController.GameInstance.cueCollUp();

        //빨간공2를 맞췄을 경우
        else if (col.gameObject.name == "Redball2")
            PoolGameController.GameInstance.cueColl2Up();

        //상대방의 공을 맞췄을 경우
        else if (col.gameObject.name == "CueBall2")
            PoolGameController.GameInstance.cueCheckDown();

        //공의 맞은 값 계산
        PoolGameController.GameInstance.BallColl();
    }
}