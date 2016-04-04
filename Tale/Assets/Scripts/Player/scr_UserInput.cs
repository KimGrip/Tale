using UnityEngine;
using System.Collections;

public class scr_UserInput : MonoBehaviour {

    public bool WalkByDefault = false;
    private scr_CharacterMovement charMove;
    private Transform cam;
    private Vector3 camForward;
    private Vector3 move;

    public bool aim;
    public float aimingWheight;

    public bool lookInCameraDirection;
    Vector3 lookPosition;

    Animator anim;

    public GameObject m_arrow;
    private Transform m_arrowSpawnpoint;
    //These variables need to be detailed and set specifically for ALVA
    public Transform spine;
    public float aimingZ = 213.46f;
    public float aimingX = -65.93f;
    public float aimingY = 20.1f;
    public float point = 30;

    void Start()
    {
        if(Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        charMove = GetComponent<scr_CharacterMovement>();
        anim = GetComponent<Animator>();
        m_arrowSpawnpoint = GameObject.FindGameObjectWithTag("arrowSpawnPoint").transform;
      
    }
    void Update()
    {
        aim = Input.GetMouseButton(1);
        if(aim)
        {
            if(Input.GetMouseButton(0))
            {
                anim.SetTrigger("Fire");
                Instantiate(m_arrow,m_arrowSpawnpoint.position,Quaternion.identity);
                //Instantiate(m_arrow, m_arrowSpawnPoint.position, Quaternion.identity);
            }
        }
    }
    void LateUpdate()
    {
        aimingWheight = Mathf.MoveTowards(aimingWheight, (aim) ? 1.0f : 0.0f, Time.deltaTime * 5);

        Vector3 normalState = new Vector3(0, 0, -1f);
        Vector3 aiminngState = new Vector3(0, 0, 0.5f);

        Vector3 pos = Vector3.Lerp(normalState, aiminngState, aimingWheight);
        cam.transform.localPosition = pos;

        if(aim)
        {
            Vector3 eulerAngleOffset = Vector3.zero;

            eulerAngleOffset = new Vector3(aimingX, aimingY, aimingZ);

            Ray ray = new Ray(cam.position, cam.forward);

            Vector3 lookPosition = ray.GetPoint(point);
            Debug.Log(lookPosition);
            spine.LookAt(lookPosition);
            spine.Rotate(eulerAngleOffset, Space.Self);
        }

    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (!aim)
        {
            if (cam != null)
            {
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                move = vertical * camForward + horizontal * cam.right;
            }
            else
            {
                move = vertical * Vector3.forward + horizontal * Vector3.right;
            }
        }
        else
        {
            move = Vector3.zero;

            Vector3 dir = lookPosition - transform.position;
            dir.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            anim.SetFloat("Forward", vertical);
            anim.SetFloat("Turn", horizontal);

            

        }
        if (move.magnitude > 1)
            move.Normalize();

        bool walkToggle = Input.GetKey(KeyCode.LeftShift) || aim;



        float walkMultiplier = 1;
        if(WalkByDefault)
        {
            if(walkToggle)
            {
                walkMultiplier = 1;
            }
            else
            {
                walkMultiplier = 0.5f;
            }
        }
        else
        {
            if(walkToggle)
            {
                walkMultiplier = 0.5f;
            }
            else
            {
                walkMultiplier = 1;
            }
        }
        
        lookPosition = lookInCameraDirection && cam != null ? transform.position + cam.forward * 100
            : transform.position + transform.forward * 100;

        move *= walkMultiplier;
        charMove.Move(move,aim,lookPosition);
    }
}
