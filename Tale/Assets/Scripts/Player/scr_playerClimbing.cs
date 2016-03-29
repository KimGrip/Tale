using UnityEngine;
using System.Collections;

public class scr_playerClimbing : MonoBehaviour {

	// Use this for initialization
    public float climbingSpeed;
    public int ropeLayer;
    private GameObject jointParent;
    public Transform previousJoint, currentJoint, nextJoint;
    string currentJointName;
    enum playerstate
    {
        state_airborne,
        state_grounded,
    }
    enum ropeState
    {
        ropestate_climbing,
        ropestate_skimming,
        ropestate_swinging,
    }
    enum equipstate
    {
        equip_rope,
        equip_bow,
        equip_other,
    }
    playerstate m_playerState;
    equipstate m_equipState;
    ropeState m_ropeState;

	void Start () {
        ropeLayer = LayerMask.NameToLayer("RopeLayer");
	}
	
	// Update is called once per frame
	void Update () {
        switch (m_ropeState)
        {
            case ropeState.ropestate_climbing:
                break;
            case ropeState.ropestate_skimming:
                break;
            case ropeState.ropestate_swinging:
                break;
        }
        if (Input.GetKey(KeyCode.W))
        {
            TraverseForward();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            TraverseBackward();
        }
       
	}
    void OnTriggerEnter(Collider colli)
    {
        if (colli.gameObject.layer == ropeLayer)
        {
            if (currentJoint == null)
            {
                currentJoint = colli.gameObject.transform;
            }
            UpdateJoints(colli.transform);
            switch (m_playerState)
            {
                case playerstate.state_airborne:
                    HandleRopeCollisionAirBorne();
                    break;
                case playerstate.state_grounded:
                    HandleRopeCollisionGrounded();
                    break;
            }
        }
    }

    void UpdateJoints(Transform newJoint){
        if (newJoint != currentJoint)
        {
            int jointNumber= int.Parse(StringManip(newJoint));
            jointParent = newJoint.transform.parent.gameObject;
            currentJoint = newJoint;
            nextJoint = jointParent.transform.FindChild("Joint_" + (jointNumber + 1).ToString());
            previousJoint = jointParent.transform.FindChild("Joint_" + (jointNumber - 1).ToString());
        }
    }
    void HandleRopeCollisionAirBorne()
    {
        //set climbRopeAirBorne 
    }
    void HandleRopeCollisionGrounded()
    {
        //set holding rope on input? climb on input?
    }
    string StringManip(Transform getNameFromTransform)
    {
         string name = getNameFromTransform.name;
         int foundS1= name.IndexOf("_");
         name = name.Substring(foundS1+1, name.Length - 1 - foundS1);
         print(name);
         return name;
    }
    void TraverseForward(){
        this.transform.position = Vector3.MoveTowards(this.transform.position, nextJoint.position, climbingSpeed);
    }
    void TraverseBackward(){
        this.transform.position = Vector3.MoveTowards(this.transform.position, previousJoint.position, climbingSpeed);
    }
}
