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

    void Start()
    {
        if(Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        charMove = GetComponent<scr_CharacterMovement>();
        anim = GetComponent<Animator>();
    }
    void LateUpdate()
    {
        aim = Input.GetMouseButton(1);
        aimingWheight = Mathf.MoveTowards(aimingWheight, (aim) ? 1.0f : 0.0f, Time.deltaTime * 5);

        Vector3 normalState = new Vector3(0, 0, -1f);
        Vector3 aiminngState = new Vector3(0, 0, 0.5f);

        Vector3 pos = Vector3.Lerp(normalState, aiminngState, aimingWheight);
        cam.transform.localPosition = pos;
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
