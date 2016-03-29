﻿using UnityEngine;
using System.Collections;
public enum PlayerState
{
    Idle, Running, Jumping, Climbing, Aiming, 
};
public class scr_playerController : MonoBehaviour {


    [System.Serializable]
    public class MoveSettings
    {
        public float m_ForwardSpeed;
        public float m_RotationSpeed;
        public float m_JumpSpeed;
        public float m_DistanceToGround;

        public LayerMask m_groundLayer;

    }
    [System.Serializable]
    public class PhysSettings
    {
        public float m_DownAccel;
    }
    [System.Serializable]
    public class InputSettings
    {
        public float m_inputDelay;
        public string FORWARD_AXSIS = "Vertical";
        public string TURN_AXSIS = "Horizontal";
        public string JUMP_AXSIS = "Jump";
    }


    public MoveSettings m_moveSettings = new MoveSettings();
    public PhysSettings m_physSeetings = new PhysSettings();
    public InputSettings m_inputSettings = new InputSettings();

    Vector3 velocity = Vector3.zero;

    Quaternion m_targetRotation;
    Rigidbody m_rb;

    float forwardInput, turnInput, jumpInput;

    public Quaternion GetTargetRotation
    {
        get { return m_targetRotation; }
    }
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, m_moveSettings.m_DistanceToGround);
    }
	// Use this for initialization
	void Start () 
    {
        m_targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
        {
            m_rb = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("The player needs a rigidbody");
        }
        forwardInput = 0; turnInput = 0; jumpInput = 0;
	}
    void GetInput()
    {
        forwardInput = Input.GetAxis(m_inputSettings.FORWARD_AXSIS); 
        turnInput = Input.GetAxis(m_inputSettings.TURN_AXSIS); // ^ + this = interpolated
        jumpInput = Input.GetAxisRaw(m_inputSettings.JUMP_AXSIS); // not interpoaltesds
    }

	// Update is called once per frame
	void Update () 
    {
        GetInput();
        Turn();
	}
    void FixedUpdate()
    {
        Run();
        Jump();

        m_rb.velocity = transform.TransformDirection(velocity);
    }
    void Run()
    {
        if(Mathf.Abs(forwardInput) > m_inputSettings.m_inputDelay)
        {
            velocity.z = m_moveSettings.m_ForwardSpeed * forwardInput;
        }
        else
        {
            velocity.z = 0;
        }
    }
    void Turn()
    {
        m_targetRotation *= Quaternion.AngleAxis(m_moveSettings.m_RotationSpeed * turnInput * Time.deltaTime, Vector3.up);
        transform.rotation = m_targetRotation;
    }
    void Jump()
    {
        if(jumpInput > 0 && Grounded())
        {
            velocity.y = m_moveSettings.m_JumpSpeed;
        }
        else if(jumpInput == 0 && Grounded())
        {
            velocity.y = 0;
        }
        else   //Gravity
        {
            velocity.y -= m_physSeetings.m_DownAccel;
        }
    }
}
