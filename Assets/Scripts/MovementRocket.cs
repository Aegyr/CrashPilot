using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRocket : MonoBehaviour
{
    [SerializeField] AudioClip mainEngine;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1f;
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
            //Debug.Log("Add Thrust" + mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
                audioSource.PlayOneShot(mainEngine, 1.0f);
            }
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else
        {
          audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
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
