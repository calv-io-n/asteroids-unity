using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    AudioSource fart;
    Rigidbody rigidBody;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip nextLevelSound;

    [SerializeField] ParticleSystem thrusterFlame;
    [SerializeField] ParticleSystem crash;
    [SerializeField] ParticleSystem last;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    enum State { Dying, LevelUp, Alive }
    State state = State.Alive;
    
    void Start()
    {
        //Init Unity Components
        rigidBody = GetComponent<Rigidbody>();
        fart = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if(state == State.Alive)
        {
            Thrust();
            Rotate();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Launch":
                //Start
                break;
            case "Land":
                //Win
                last.Play();
                state = State.LevelUp;
                PlayCollisionSound();
                Invoke("LoadScene", 1f);
                break;
            default:
                //Loss
                crash.Play();
                state = State.Dying;
                PlayCollisionSound();
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void PlayCollisionSound()
    {
        fart.Stop();
        fart.pitch = 1f;
        if (state == State.LevelUp)
        {
            fart.PlayOneShot(nextLevelSound);
        }
        else
        {
            fart.PlayOneShot(deathSound);
        }
    }

    private void LoadScene()
    {
        //Fixme: Add more levels
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
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
            ThrustSound();
            thrusterFlame.Play();
            //Thrust
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        }
        else if (state == State.Alive)
        {
            fart.Stop();
            thrusterFlame.Stop();
        }
    }

    private void ThrustSound()
    {
        // wet farts
        // fart.pitch = UnityEngine.Random.Range(0.32f, 1.23f);
        //If sound is already playing don't layer sounds
        if (!fart.isPlaying)
        {
            fart.pitch = UnityEngine.Random.Range(0.32f, 1.23f);
            fart.PlayOneShot(mainEngine);
        }
    }
}