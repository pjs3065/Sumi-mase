using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player {
	public int score = 0;

    //플레이어의 이름을 넣어주고
	public Player(string name) {
		Name = name;
	}

	public string Name {
		get;
		private set;
	}

	public int Points {
		get { return score; }
	}

    //점수를 낼 경우 10점이 올라간다.
	public void GetScore() {
		score += 10;
	}

    //점수를 못 낼경우 10점이 내려간다.
    public void minusScore()
    {
        //스코어가 0일 경우 마이너스를 방지
        if (score != 0)
        {
            score -= 10;
        }
    }
}
