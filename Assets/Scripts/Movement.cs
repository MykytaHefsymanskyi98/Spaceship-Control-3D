using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float zAngle = 0f;
    [SerializeField] Vector3 moveVector = new Vector3(0f, 0f, 0f);
    [SerializeField] float moveForwardVelocity = 5f;
    [SerializeField] float moveSideWaysVelocity = 5f;
    [SerializeField] ParticleSystem rocketJetVFX;
    [SerializeField] ParticleSystem sideThrustLeftVFX;
    [SerializeField] ParticleSystem sideThrustRightVFX;

    Rigidbody myRigidBody;
    SFXPlayer mySFXPlayer;

    float particleSystemTimeDelta = 4f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mySFXPlayer = FindObjectOfType<SFXPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        AddingForce();
        ProccessSteering();
    }

    void AddingForce()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidBody.AddRelativeForce(moveVector * moveForwardVelocity * Time.deltaTime);
            mySFXPlayer.PlayMainEngineSFX();
            if(!rocketJetVFX.isPlaying)
            {
                StartRocketJetVFX();
            }
        }
        else
        {
            mySFXPlayer.StopMainEngineSFX();
            Invoke("StopRocketJet", particleSystemTimeDelta);
        }    
    }

    void ProccessSteering()
    {
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            MoveSideways(-zAngle);
            sideThrustRightVFX.Stop();
            StartSideThrustLeftVFX();
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            MoveSideways(zAngle);
            sideThrustLeftVFX.Stop();
            StartSideThrustRightVFX();
        }
    }

    void MoveSideways(float newZAngle)
    {
        myRigidBody.freezeRotation = true;
        transform.Rotate(0f, 0f, newZAngle * moveSideWaysVelocity * Time.deltaTime);
        myRigidBody.freezeRotation = false;
    }

    void StartRocketJetVFX()
    {
        rocketJetVFX.Play();
    }

    void StopRocketJet()
    {
        rocketJetVFX.Stop();
    }

    void StartSideThrustLeftVFX()
    {
        sideThrustLeftVFX.Play();
    }

    void StartSideThrustRightVFX()
    {
        sideThrustRightVFX.Play();
    }
}
