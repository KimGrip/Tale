using UnityEngine;
using System.Collections;

public class Scr_CameraController : MonoBehaviour {

	// Use this for initialization

    public Transform target;
    public float lookSmooth;
    public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
    public float xTilt = 10;

    Vector3 destination = Vector3.zero;
    scr_playerController charController;
    float rotateLevel = 0;

	void Start ()
    {
        SetCameraTarget(target);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetCameraTarget(Transform t)
    {
        target = t;
        if(target != null)
        {
            if (target.GetComponent<scr_playerController>())
            {
                charController = target.GetComponent<scr_playerController>();
            }
            else
            {
                Debug.LogError("The camers target needs a characterController");
            }
        }
        else
        {
            Debug.LogError("Your camera needs a target");
        }
    }
    void LateUpdate()
    {
        MoveToTarget();
        LookAtTarget();
    }
    void MoveToTarget()
    {
        destination = charController.GetTargetRotation * offsetFromTarget;
        destination += target.position;
        transform.position = destination;

    }
    void LookAtTarget()
    {
        float euluerYangle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateLevel, lookSmooth);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, euluerYangle, 0);
    }
}
