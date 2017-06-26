using UnityEngine;
using System.Collections;

public class PoolGameController : MonoBehaviour
{
    public GameObject cue;

    public GameObject cueBall;
    public GameObject cueBall2;

    public GameObject redBall;
    public GameObject redBall2;

    public GameObject mainCamera;
    public GameObject scoreBar;
    public GameObject winnerMessage;

    public float maxForce;
    public float minForce;
    
    public Vector3 strikeDirection;

    public const float MIN_DISTANCE = 27.5f;
    public const float MAX_DISTANCE = 32f;

    public IGameObjectState currentState;

    public Player CurrentPlayer;
    public Player OtherPlayer;

    private bool currentPlayerContinuesToPlay = false;
    public static int playerCheck = 1; //1=민성 2=재성

    //노란공이 맞출시 사용될 충돌체크 변수
    private int cueColl = 0; //1번빨강 충돌체크
    private int cueColl2 = 0; //2번빨강 충돌체크
    private int cueCheck = 0; //흰공 충돌 체크

    //흰공이 맞출시 사용될 충돌체크 변수
    private int cue2Coll = 0; //1번빨강 충돌체크
    private int cue2Coll2 = 0;// 2번빨강 충돌체크
    private int cue2Check = 0; //흰공 충돌 체크

    static public PoolGameController GameInstance
    {
        get;
        private set;
    }

    //현재 선수를 초기화해주고 현재 상태를 업데이트해준다.
    void Start()
    {
        strikeDirection = Vector3.forward;
        CurrentPlayer = new Player("Minsung");
        OtherPlayer = new Player("Jaesung");

        GameInstance = this;
        winnerMessage.GetComponent<Canvas>().enabled = false;

        currentState = new GameStates.WaitingForStrikeState(this);
    }

    void Update()
    {
        currentState.Update();
    }

    void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    void LateUpdate()
    {
        currentState.LateUpdate();
    }

    public void cueCollUp()
    {
        cueColl = 1;
    }
    public void cueColl2Up()
    {
        cueColl2 = 1;
    }
    public void cueCheckDown()
    {
        cueCheck = -1;
        currentPlayerContinuesToPlay = false;
    }
    public void cue2CollUp()
    {
        cue2Coll = 1;
    }
    public void cue2Coll2Up()
    {
        cue2Coll2 = 1;
    }
    public void cue2CheckDown()
    {
        cue2Check = -1;
        currentPlayerContinuesToPlay = false;
    }

    //볼을 맞춘후에 계산하는 함수 (빨간공 두개를 맞출시 currentPlayer를 true하여 턴을 넘기지 않게 만들어준다
    public void BallColl()
    {
        if (playerCheck == 1)
        {
            //Debug.Log(cueColl + " " + cueColl2 + " " + cueCheck + " " + currentPlayerContinuesToPlay + " player1");
            if (cueColl > 0 && cueColl2 > 0 && cueCheck == 0)
            {

                currentPlayerContinuesToPlay = true;
            }


        }
        else if (playerCheck == 2)
        {
            if (cue2Coll > 0 && cue2Coll2 > 0 && cue2Check == 0)
            {
                //Debug.Log(cue2Coll + " " + cue2Coll2 + " " + cue2Check + " player2");
                currentPlayerContinuesToPlay = true;
            }
        }
    }

    //다음턴으로 넘기는 함수
    public void NextPlayer()
    {
        //점수를 냈을 경우 플레이어 계속 유지와 점수 증가
        if (currentPlayerContinuesToPlay)
        {
            CurrentPlayer.GetScore();
            currentPlayerContinuesToPlay = false;

            if (playerCheck == 1)
            {
                cueColl = 0;
                cueColl2 = 0;
                cueCheck = 0;
            }

            else if (playerCheck == 2)
            {
                cue2Coll = 0;
                cue2Coll2 = 0;
                cue2Check = 0;
            }
            
            // Debug.Log(CurrentPlayer.Name + " continues to play");
            return;
        }


        //모든 공을 칠경우 플레이어의 점수를 감소하고 다음 턴으로 넘긴다.
        else
        {
            if (playerCheck == 1)
            {
                if (cueColl == 0 && cueColl2 == 0 || cueCheck != 0)
                {
                    CurrentPlayer.minusScore();
                }
            }

            else if (playerCheck == 2)
            {
                if (cue2Coll == 0 && cue2Coll2 == 0 || cue2Check != 0)
                {
                    CurrentPlayer.minusScore();
                }
            }

        }

        //공통적으로 초기화 해주기 위한 함수
        if (playerCheck == 1)
        {
            playerCheck = 2;
            cueColl = 0;
            cueColl2 = 0;
            cueCheck = 0;
        }
        else if (playerCheck == 2)
        {
            playerCheck = 1;
            cue2Coll = 0;
            cue2Coll2 = 0;
            cue2Check = 0;
        }

        // Debug.Log(OtherPlayer.Name + " will play");

        //이름을 바꿔주기 위해서 swap한다.
        var aux = CurrentPlayer;
        CurrentPlayer = OtherPlayer;
        OtherPlayer = aux;
    }

    //먼저 20점을 먼저 낼 경우 점수 계산을 해서 승자를 string값으로 넘겨준다.
    public void EndMatch()
    {
        Player winner = null;
        if (CurrentPlayer.Points > OtherPlayer.Points)
            winner = CurrentPlayer;
        else if (CurrentPlayer.Points < OtherPlayer.Points)
            winner = OtherPlayer;

        var msg = "Game Over\n";

        if (winner != null)
            msg += string.Format("The winner is '{0}'", winner.Name);
        else
            msg += "It was a draw!";

        var text = winnerMessage.GetComponentInChildren<UnityEngine.UI.Text>();
        text.text = msg;
        winnerMessage.GetComponent<Canvas>().enabled = true;
    }
}
