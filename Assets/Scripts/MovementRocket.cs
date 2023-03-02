using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();  
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Add Thrust" + mainThrust * Time.deltaTime);
            rigid_body.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
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
        rigid_body.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigid_body.freezeRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1f;
    private Rigidbody rigid_body;
}
