using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Thrust
            rigidBody.AddRelativeForce(Vector3.up);
        }
        if(Input.GetKey(KeyCode.A))
        {
            //Rotate left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Rotate right
        } 
    }
}