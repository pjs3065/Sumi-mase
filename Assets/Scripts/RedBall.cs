using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redball : MonoBehaviour {

	void Start() {
		GetComponent<Rigidbody>().sleepThreshold = 0.15f;
	}
    //빨간공1의 충돌 속도 계산
	void FixedUpdate () {
		var rigidbody = GetComponent<Rigidbody>();
		if (rigidbody.velocity.y > 0) {
			var velocity = rigidbody.velocity;
			velocity.y *= 0.3f;
			rigidbody.velocity = velocity;
		}
	}
}
