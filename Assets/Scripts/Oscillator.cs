using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 MovementVector;
    [SerializeField] [Range(0, 1)] private float MovementFactor;
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
        float Cycles = Time.time / Period;
        const float tau = Mathf.PI * 2.0f;
        float RawSinWave = Mathf.Sin(Cycles * tau);

        MovementFactor = (RawSinWave + 1.0f) / 2.0f;

        Vector3 Offset = MovementVector * MovementFactor;
        transform.position = StartingPosition + Offset;
    }
}
