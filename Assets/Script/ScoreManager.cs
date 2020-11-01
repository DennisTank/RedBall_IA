using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : NetworkBehaviour
{
    //This is to reset the ball if the ball hit the left or right goal.


    public GameObject Ball;
    public Vector3 ballStartPos;
    public Text left, right;

    // This Score will be Sync though all the players
    [SyncVar]int leftScore, rightScore;

    private void Start()=>leftScore = rightScore = 0;
    private void Update()
    {
        left.text = leftScore.ToString();
        right.text = rightScore.ToString();
    }
    private void ResetBall() {

        Ball.SetActive(true);
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Ball.transform.position = ballStartPos;
        Debug.Log(leftScore+" "+rightScore);
    }
    // This functions will only be called by the server
    // but, all other values will be updated
    [ClientRpc] public void RpcResetBall() => ResetBall();
    [ClientRpc] public void LeftScored() => leftScore++;
    [ClientRpc] public void RightScored() => rightScore++;

}
