using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Test");
        rigid_body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private const int V = 0;

    void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            speed += acceleration * Time.deltaTime;
        }
        else
        {
            float decelerationPerFrame = deceleration * Time.deltaTime;
            if(speed > decelerationPerFrame)
            {
                speed -= decelerationPerFrame;
            }
            else if(speed < decelerationPerFrame)
            {
                speed = V;
            }

        }
       
        bool elevatorUp = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
        bool elevatorDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);

        float x = speed;
        float y = 0;

        if(elevatorUp)
        {
            y = speed * speed * upwardFactor;
        }

        if(elevatorDown)
        {
           y = -1 * speed * speed * upwardFactor; 
        }

        Debug.Log("Add relativ force : x=" + x + " y=" + y);
        rigid_body.AddRelativeForce(x,y,0.0f);
    }

    [SerializeField] private float upwardFactor = 0;
    private float speed = 0.0f;
    [SerializeField] private float acceleration = 0.0f;
    [SerializeField]private float deceleration = 0.0f;
    private Rigidbody rigid_body;
};