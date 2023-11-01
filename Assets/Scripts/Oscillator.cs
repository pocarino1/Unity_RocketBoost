using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 MovementVector = Vector3.zero;
    private float MovementFactor = 0.0f;
    [SerializeField] private float Period = 2.0f;

    Vector3 StartingPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOscillator();
    }

    private void UpdateOscillator()
    {
        if(Period <= Mathf.Epsilon)
        {
            return;
        }

        float Cycles = Time.time / Period;
        const float tau = Mathf.PI * 2.0f;
        float RawSinWave = Mathf.Sin(Cycles * tau);

        MovementFactor = (RawSinWave + 1.0f) / 2.0f;

        Vector3 Offset = MovementVector * MovementFactor;
        transform.position = StartingPosition + Offset;
    }
}
