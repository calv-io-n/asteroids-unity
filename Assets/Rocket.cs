using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource fart;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;


    void Start()
    {
        //Init Unity Components
        rigidBody = GetComponent<Rigidbody>();
        fart = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        Thrust();
        Rotate();
	}

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Launch":
                //Start
                print("Begin");
                break;
            case "Land":
                //Win
                break;
            default:
                //Loss
                break;
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //Take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            //Rotate left
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Rotate right
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //Resume Natural physics
    }

    private void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            //If sound is already playing don't layer sounds
            if (!fart.isPlaying)
            {
                fart.Play();
            }
            //Thrust
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        }
        else
        {
            //Stop sound effect when not depressed
            fart.Stop();
        }
    }
}