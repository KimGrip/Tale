using UnityEngine;
using System.Collections;
public enum PlayerState
{
    Idle, Running, Jumping, Climbing, Aiming, 
};
[RequireComponent(typeof(Rigidbody))]
public class scr_playerController : MonoBehaviour {

    public float m_inputDelay;
    public float m_ForwardSpeed;
    public float m_RotationSpeed;

    Quaternion m_targetRotation;
    Rigidbody m_rb;

    float forwardInput, turnInput;

    public Quaternion GetTargetRotation
    {
        get { return m_targetRotation; }
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
        forwardInput = 0; turnInput = 0;
	}
    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
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
    }
    void Run()
    {
        if(Mathf.Abs(forwardInput) > m_inputDelay)
        {
            m_rb.velocity = transform.forward * forwardInput * m_ForwardSpeed;
        }
        else
        {
            m_rb.velocity = Vector3.zero;
        }
    }
    void Turn()
    {
        m_targetRotation *= Quaternion.AngleAxis(m_RotationSpeed * turnInput * Time.deltaTime,Vector3.up  );
        transform.rotation = m_targetRotation;
    }
}
