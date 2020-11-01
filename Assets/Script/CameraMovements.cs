using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    //Basic camera follow scrip
    // camera will only follow though the X-axis

    public GameObject Ball;
    private float Xpos;

    void Start()=>Xpos = 0;

    private void LateUpdate(){

        Xpos = Mathf.Lerp(transform.position.x,Ball.transform.position.x,0.05f);

        transform.position = new Vector3(Xpos, transform.position.y, transform.position.z);
    }
}
