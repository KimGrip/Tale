using UnityEngine;
using UnityEditor;

public class scr_FreeCameraLook : scr_Pivot {

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float turnSpeed = 1.5f;
    [SerializeField]
    private float turnsmoothing = .1f;
    [SerializeField]
    private float tiltMax = 75f;
    [SerializeField]
    private float tiltMin = 45f;
    [SerializeField]
    private bool lockCursor = false;


    private float lookAngle;
    private float tiltAngle;

    private const float LookDistance = 100f;

    private float smoothX = 0;
    private float smoothY = 0;
    private float smoothXvelocity = 0;
    private float smoothYvelocity = 0;

    public Vector3 FollowCameraOffset;

    //if no input, player moving, bigger than resettimer'
    private bool m_takingInput;
    private bool m_autoFollowPlayer;
    public float m_CameraResetTime;
    private float m_CameraResetCounter;

    private Rigidbody m_playerRB;

    protected override void Awake()
    {
        base.Awake();
        m_playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        Cursor.visible = lockCursor;
        //Add so visible and locked happends togheter


        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        cam = GetComponentInChildren<Camera>().transform;

        pivot = cam.parent;
    }



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        HandleRotationMovement();
        if(!m_takingInput)
        {
            m_CameraResetCounter += Time.deltaTime;
            if(m_CameraResetCounter > m_CameraResetTime && m_playerRB.velocity != Vector3.zero)
            {
                TurnCameraTowardsPlayerForward();
            }
        }
        else
        {
            m_CameraResetCounter = 0;
        }
        if (lockCursor && Input.GetMouseButtonUp(0))
        {
            Cursor.visible = lockCursor;

        }
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    protected override void Follow(float deltaTime)
    {
        transform.position = Vector3.Lerp(transform.position , target.position , deltaTime * moveSpeed);
    }


    float offsetX = 0;
    float offsetY = 0;
    void TurnCameraTowardsPlayerForward()
    {
        //Debug.Log("Turning towards player");
        Vector3 velocity = Vector3.zero;
        Vector3 forward = target.transform.forward;
        Vector3 needPos = target.transform.position - forward;
        transform.position = Vector3.SmoothDamp(transform.position, needPos,
                                                ref velocity, 0.5f);
        transform.LookAt(target.transform);
        transform.rotation = target.transform.rotation;
    }

    void handleOffsets()
    {
        if (offsetX != 0)
        {
            offsetX = Mathf.MoveTowards(offsetX, 0, Time.deltaTime);
        }

        if (offsetY != 0)
        {
            offsetY = Mathf.MoveTowards(offsetY, 0, Time.deltaTime);
        }
    }


    void HandleRotationMovement()
    {
        handleOffsets();

        float x = Input.GetAxis("Mouse X") + offsetX;
        float y = Input.GetAxis("Mouse Y") + offsetY;
         // float x = Input.GetAxis("xbox_rightstick_x") + offsetX;
       // float y = Input.GetAxis("xbox_rightstick_y") + offsetX;
        if(x != 0 ||y != 0)
            m_takingInput = true;
        else
            m_takingInput = false;
        

        if (turnsmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, x, ref smoothXvelocity, turnsmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, y, ref smoothYvelocity, turnsmoothing);
        }
        else
        {
            smoothX = x;
            smoothY = y;
        }

        lookAngle += smoothX * turnSpeed;

        transform.rotation = Quaternion.Euler(0f, lookAngle, 0);

        tiltAngle -= smoothY * turnSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, -tiltMin, tiltMax);

        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

    }

}
