using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private float BoostPower = 100.0f;
    [SerializeField] private float RotationPower = 0.05f;
    [SerializeField] private AudioClip RocketThrustSound = null;
    [SerializeField] private ParticleSystem RocketThrustParticleEffect = null;
    [SerializeField] private VariableJoystick RocketJoystick = null;
    [SerializeField] private Button BoostButton = null;

    private Rigidbody RocketRigidbody = null;

    private AudioSource RocketAudioSource = null;

    private AudioClip CurrentPlayAudioClip = null;

    private bool BoosterButtonDown = false;

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

        BoosterButtonDown = false;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if(BoostButton != null)
        {
            BoostButton.onClick.RemoveListener(StartThrusting);
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
        if (Input.GetKey(KeyCode.Space) || BoosterButtonDown)
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

        // if (RocketJoystick != null)
        // {
        //     float HorizontalValue = RocketJoystick.Horizontal;
        //     if (HorizontalValue < 0.0f)
        //     {
        //         RocketRotation(RotationPower);
        //     }
        //     else if (HorizontalValue > 0.0f)
        //     {
        //         RocketRotation(-RotationPower);
        //     }
        // }
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

    public void StartBooster()
    {
        BoosterButtonDown = true;
    }

    public void StopBooster()
    {
        BoosterButtonDown = false;
    }
}
