using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    const float TAU = Mathf.PI * 2f; // 2 pi radians representing the roation

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    float movementFactor; //0 for not moved, 1 for fully moved
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Handle Divide by zero exception in the unity editor
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * TAU);

        movementFactor = rawSinWave;  // from -1 to 1 amplitude of 1.. edit these to translate and transform the wave
        Vector3 movement = movementVector * movementFactor;
        transform.position = startPos + movement;
    }
}
