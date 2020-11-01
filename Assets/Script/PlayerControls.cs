using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControls : NetworkBehaviour
{
    // Grabbing the Screen Width for finding the center of the screen
    private float screenW;
    // position of touch on the screen
    private Vector2 touchPos;

    void Start()=>screenW = Screen.width;

    void Update()
    {
        
        if (isLocalPlayer) {
            // This is to get the Ball if its active
            if (GameObject.FindGameObjectWithTag("Ball") == null) return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // getting the touch position 
                touchPos = Input.mousePosition;

                // raycast to check if its a jump input or else, a move ball as per the touch position
                if (Physics.Raycast(ray,out hit,100)) {

                    if (hit.collider.gameObject.CompareTag("Ball")) {
                        CmdMoveBall(0);
                    }
                    else if (touchPos.x >= screenW / 2)
                    {
                        CmdMoveBall(1);
                    }
                    else if (touchPos.x < screenW / 2)
                    {
                        CmdMoveBall(-1);
                    }
                }
            }
        }
    }
    // push call from client to server
    [Command]
    void CmdMoveBall(int state) =>RpcMoveBall(state);
    // pushAll from server to all clients
    [ClientRpc]
    void RpcMoveBall(int state) {
        //Adding the force to move the ball
        switch (state) {

            case 0:
                GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().AddForce(Vector3.up * 5,ForceMode.Impulse);
                break;
            case -1:
                GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().AddForce(Vector3.left * 10, ForceMode.Force);
                break;
            case 1:
                GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().AddForce(Vector3.right * 10, ForceMode.Force);
                break;

            default:break;
        }
    }
}
