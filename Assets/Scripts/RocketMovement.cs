using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private float BoostPower = 100.0f;
    [SerializeField] private float RotationPower = 0.05f;
    [SerializeField] private AudioClip RocketThrustSound = null;
    [SerializeField] private ParticleSystem RocketThrustParticleEffect = null;

    private Rigidbody RocketRigidbody = null;

    private AudioSource RocketAudioSource = null;

    private AudioClip CurrentPlayAudioClip = null;

    // Start is called before the first frame update
    void Start()
    {
        enabled = true;

        RocketRigidbody = GetComponent<Rigidbody>();
        RocketAudioSource = GetComponent<AudioSource>();

        if (RocketThrustParticleEffect != null)
        {
            RocketThrustParticleEffect.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        RocketRigidbody.AddRelativeForce(Vector3.up * BoostPower * Time.deltaTime);

        PlayEffectSound(RocketThrustSound);

        if (RocketThrustParticleEffect != null && !RocketThrustParticleEffect.isPlaying)
        {
            RocketThrustParticleEffect.Play();
        }
    }

    private void StopThrusting()
    {
        StopEffectSound();

        if (RocketThrustParticleEffect != null && RocketThrustParticleEffect.isPlaying)
        {
            RocketThrustParticleEffect.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RocketRotation(RotationPower);
        }
        else if (Input.GetKey(KeyCode.D))
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

    public void SetDisableComponent()
    {
        enabled = false;

        if (RocketAudioSource != null && RocketAudioSource.isPlaying)
        {
            RocketAudioSource.Stop();
        }

        if (RocketThrustParticleEffect != null && RocketThrustParticleEffect.isPlaying)
        {
            RocketThrustParticleEffect.Stop();
        }
    }

    public void PlayEffectSound(AudioClip EffectSoundClip)
    {
        if (CurrentPlayAudioClip == EffectSoundClip)
        {
            return;
        }

        if (RocketAudioSource != null)
        {
            StopEffectSound();

            if (EffectSoundClip != null)
            {
                RocketAudioSource.PlayOneShot(EffectSoundClip);
                CurrentPlayAudioClip = EffectSoundClip;
            }
        }
    }

    public void StopEffectSound()
    {
        if (RocketAudioSource != null)
        {
            if (RocketAudioSource.isPlaying)
            {
                RocketAudioSource.Stop();
            }

            CurrentPlayAudioClip = null;
        }
    }
}
