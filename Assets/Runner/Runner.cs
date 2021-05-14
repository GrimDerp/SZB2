using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//stuff &* things

public class Runner : MonoBehaviour
{

    public static float distanceTraveled;

    public Vector3 jumpVelocity;
    public float m_Thrust = 20f;
    

    public float acceleration;

    private bool touchingPlatform;

    public Rigidbody rb;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   

    // Update is called once per frame
    void Update()
    {
             distanceTraveled = transform.localPosition.x;

             if(touchingPlatform && Input.GetButtonDown("Jump")) {
                 rb.AddForce(jumpVelocity, ForceMode.VelocityChange);
             }
    }



    void FixedUpdate() {
        if(touchingPlatform){
            rb.AddForce(m_Thrust, 0f, 0f, ForceMode.Acceleration);
        }

          
    }



    void OnCollisionEnter() {
        touchingPlatform = true;
    }

    void OnCollisionExit() {
        touchingPlatform = false;
    }

}
