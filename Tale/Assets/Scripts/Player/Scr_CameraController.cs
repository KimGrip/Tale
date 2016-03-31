﻿using UnityEngine;
using System.Collections;

public class Scr_CameraController : MonoBehaviour {

	// Use this for initialization


    public Transform target;
    [System.Serializable]
    public class CameraPositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0, 3.4f, 0);
        public float m_lookSmooth = 100;
        public float m_distanceFromTarget = -8;
        public float m_zoomSpeed = 10;
        public float m_maxZoom = -2;
        public float minZoom = -15;

        public float m_CameraReturnTime;
    }
    [System.Serializable]
    public class AimingSettings
    {
        public float xRotation = -20;
        public float yRotation = -180;
        public float maxRotation = 25;
        public float minRotation = -25;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = -150;
    }
    [System.Serializable]
    public class InputSettings
    {
        public string AIM_HORIZONTAL_SNAP = "AimHorizontalSnap";
        public string AIM_HORIZONTAL = "Mouse X";
        public string AIM_VERTICAL = "Mouse Y";
        public string ZOOM = "Fire2";
    }
    public CameraPositionSettings m_cameraPositionSettings = new CameraPositionSettings();
    public AimingSettings m_aimSettings = new AimingSettings();
    public InputSettings m_inputSettings = new InputSettings();

    Vector3 m_targetPos = Vector3.zero;
    Vector3 m_destination = Vector3.zero;
    scr_PSM playerStateManager;
    scr_playerController m_charController;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;

    private float cameraReturnCounter;
    private bool m_returnCamera;

	void Start ()
    {
        SetCameraTarget(target);
        playerStateManager = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_PSM>();
        m_targetPos = target.position + m_cameraPositionSettings.targetPosOffset;
        m_destination = Quaternion.Euler(m_aimSettings.xRotation, m_aimSettings.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * m_cameraPositionSettings.m_distanceFromTarget;
        m_destination += m_targetPos;
        transform.position = m_destination;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
        BowCamera();

        if(vOrbitInput == 0 && hOrbitInput == 0 && playerStateManager.GetPlayerPose() == scr_PSM.PlayerPose.pose_running)
        {
            cameraReturnCounter += Time.deltaTime;
            if(cameraReturnCounter > m_cameraPositionSettings.m_CameraReturnTime)
            {
                m_returnCamera = true;
            }
        }
        else
        {
            cameraReturnCounter = 0;
            OrbitTarget();
        }
	}
    void ResetCamera()
    {
        Debug.Log("Return cameraia");


        Mathf.MoveTowardsAngle(m_aimSettings.xRotation, -20, 30);
        Mathf.MoveTowardsAngle(m_aimSettings.yRotation, -20, 30);


        //Mathf.Lerp(m_aimSettings.xRotation, -20, m_cameraPositionSettings.m_CameraReturnTime);
        //Mathf.Lerp(m_aimSettings.yRotation, -180, m_cameraPositionSettings.m_CameraReturnTime);


    }
    public void SetCameraTarget(Transform t)
    {
        target = t;
        if(target != null)
        {
            if (target.GetComponent<scr_playerController>())
            {
                m_charController = target.GetComponent<scr_playerController>();
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
    void GetInput()
    {
        vOrbitInput = Input.GetAxis(m_inputSettings.AIM_VERTICAL);
        hOrbitInput = Input.GetAxis(m_inputSettings.AIM_HORIZONTAL);
        zoomInput = Input.GetAxisRaw(m_inputSettings.ZOOM);
    }
    void LateUpdate()
    {
        MoveToTarget();
        LookAtTarget();
    }
    void MoveToTarget()
    {
        m_targetPos = target.position + m_cameraPositionSettings.targetPosOffset;
        m_destination = Quaternion.Euler(m_aimSettings.xRotation, m_aimSettings.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * m_cameraPositionSettings.m_distanceFromTarget; 
        m_destination += m_targetPos;
        transform.position = m_destination;

    }
    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(m_targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_cameraPositionSettings.m_lookSmooth * Time.deltaTime);
    }
    void OrbitTarget()
    {
        m_aimSettings.xRotation += vOrbitInput * m_aimSettings.vOrbitSmooth * Time.deltaTime;
        m_aimSettings.yRotation += -hOrbitInput * m_aimSettings.hOrbitSmooth * Time.deltaTime;

        if(m_aimSettings.xRotation > m_aimSettings.maxRotation)
        {
            m_aimSettings.xRotation = m_aimSettings.maxRotation;
        }
        else if (m_aimSettings.xRotation < m_aimSettings.minRotation)
        {
            m_aimSettings.xRotation = m_aimSettings.minRotation;
        }

    }
    void BowCamera()
    {
        if(zoomInput != 0)
        {
            m_cameraPositionSettings.m_distanceFromTarget += zoomInput * m_cameraPositionSettings.m_zoomSpeed * Time.deltaTime;
        }
        else if(zoomInput == 0  )
        {
            m_cameraPositionSettings.m_distanceFromTarget -= 1 * m_cameraPositionSettings.m_zoomSpeed * Time.deltaTime;
        }


        if(m_cameraPositionSettings.m_distanceFromTarget > m_cameraPositionSettings.m_maxZoom)
        {
            m_cameraPositionSettings.m_distanceFromTarget = m_cameraPositionSettings.m_maxZoom;
        }
        if (m_cameraPositionSettings.m_distanceFromTarget < m_cameraPositionSettings.minZoom)
        {
            m_cameraPositionSettings.m_distanceFromTarget = m_cameraPositionSettings.minZoom;
        }
    }
}
