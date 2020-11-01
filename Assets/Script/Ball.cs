using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    // reference to the ScoreManaget to increment the score
    public ScoreManager score;

    // This collisoion will only be detected by the server
    // ans the results will be assigned to all the clients
    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("left")){
            score.LeftScored();
            score.RpcResetBall();
        }
        else if (collision.gameObject.CompareTag("right")) {
            score.RightScored();
            score.RpcResetBall();
        }
    }
}
