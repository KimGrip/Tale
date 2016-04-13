﻿using UnityEngine;
using System.Collections;

public class scr_playerStateFunctions : MonoBehaviour {

	// Use this for initialization
    public GameObject m_player;
    scr_attachRopeTo pAttach;
    DontGoThroughThings DGTT;
    scr_UserInput pInput;
    Rigidbody pRigidbody;
    scr_CharacterMovement pMove;
	void Start () {
        m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player == null)
        {
            print("Couldnt Find Player");
            Destroy(this);
            return;
        }
        pRigidbody = m_player.GetComponent<Rigidbody>();
        pAttach = m_player.GetComponent<scr_attachRopeTo>();
        DGTT = m_player.GetComponent<DontGoThroughThings>();
        pInput = m_player.GetComponent<scr_UserInput>();
        pMove = m_player.GetComponent<scr_CharacterMovement>();
	}
    public void SetSwinging(Collider p_attachPoint)
    {
        pAttach.enabled = true;
        //pInput.enabled = false;
        pMove.enabled = false;
        DGTT.enabled = true;
       
        //FixRigidbodyConstraints(true, true, true);
        pRigidbody.constraints = RigidbodyConstraints.None;
        pRigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        pRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        SetRigidBody(1, 0.1f, 0.05f, false, false);
        pAttach.SetTeatherObject(p_attachPoint.gameObject.transform);
        pAttach.SetAmITethered(true);
        print("AttachedAgain");
    }
    public void SetRunning()
    {
        pAttach.enabled = false;
        pAttach.SetAmITethered(false);
        pInput.enabled = true;
        pMove.enabled = true;
        DGTT.enabled = false;

        //FixRigidbodyConstraints(true, true, true);
        pRigidbody.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY;
        pRigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        pRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        pRigidbody.isKinematic = true;
        SetRigidBody(1, 0.1f, 0.05f, false, false);
        print("RunningAgain");
    }
    void SetRigidBody(float mass, float drag, float angDrag, bool useGravity, bool isKinematic)
    {
        pRigidbody.mass = mass;
        pRigidbody.drag = drag;
        pRigidbody.angularDrag = angDrag;
        pRigidbody.useGravity = useGravity;
        pRigidbody.isKinematic = isKinematic;
    }
    void FixRigidbodyConstraints(bool xLocked, bool yLocked, bool ZLocked) //check if working not confirmed
    {
        if (xLocked)
        {
            pRigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        }
        if (yLocked)
        {
            pRigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        }
        if (ZLocked)
        {
            pRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
      
    }
    public void SetClimbing()
    {

    }
 
}
