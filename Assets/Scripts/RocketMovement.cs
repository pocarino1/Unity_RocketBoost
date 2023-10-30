﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private float BoostPower = 100.0f;
    [SerializeField] private float RotationPower = 0.05f;

    private Rigidbody RocketRigidbody = null;

    private AudioSource RocketBoostSound = null;

    // Start is called before the first frame update
    void Start()
    {
        RocketRigidbody = GetComponent<Rigidbody>();
        RocketBoostSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            RocketRigidbody.AddRelativeForce(Vector3.up * BoostPower * Time.deltaTime);

            if(RocketBoostSound != null && !RocketBoostSound.isPlaying)
            {
                RocketBoostSound.Play();
            }
        }
        else
        {
            if(RocketBoostSound != null && RocketBoostSound.isPlaying)
            {
                RocketBoostSound.Stop();
            }
        } 
    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RocketRotation(RotationPower);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RocketRotation(-RotationPower);
        }
    }

    private void RocketRotation(float RotationValue)
    {
        RocketRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * RotationValue * Time.deltaTime);
        RocketRigidbody.freezeRotation = false;
    }
}
