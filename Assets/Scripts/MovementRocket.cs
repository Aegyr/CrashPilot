using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRocket : MonoBehaviour
{
    [SerializeField] AudioClip mainEngine;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1f;

    [SerializeField] ParticleSystem leftJetParticleSystem;
    [SerializeField] ParticleSystem rightJetParticleSystem;
    [SerializeField] ParticleSystem mainJetParticleSystem;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();  
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    private void StartThrust()
    {
        if (!mainJetParticleSystem.isPlaying)
        {
            mainJetParticleSystem.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.PlayOneShot(mainEngine, 1.0f);
        }
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }


    private void StopThrust()
    {
        mainJetParticleSystem.Stop();
        audioSource.Stop();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            leftJetParticleSystem.Stop();
            if(!rightJetParticleSystem.isPlaying)
            {
                rightJetParticleSystem.Play();
            }
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rightJetParticleSystem.Stop();
            if(!leftJetParticleSystem.isPlaying)
            {
                leftJetParticleSystem.Play();
            }
            ApplyRotation(-rotationThrust);
        }
        else
        {
            leftJetParticleSystem.Stop();
            rightJetParticleSystem.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {   
        rigidBody.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidBody.freezeRotation = false;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
}
