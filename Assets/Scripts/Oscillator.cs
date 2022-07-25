using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        
        if (period <= Mathf.Epsilon) {return;}

        //define a cycle - once every period seconds
        float cycles = Time.time / period;
        //define tau (6.28 or pi x2)
        const float tau = Mathf.PI * 2;
        //get a value between -1 and 1 every cycle
        float rawSinWave = Mathf.Sin(cycles * tau);
        //get a value between 0 and 1
        movementFactor = (rawSinWave + 1f) / 2f;
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
