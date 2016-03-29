using UnityEngine;
using System.Collections;

public class scr_playerClimbing : MonoBehaviour {

	// Use this for initialization
    public float climbingSpeed;
    public int ropeLayer;
    private GameObject jointParent;
    public Transform previousJoint, currentJoint, nextJoint;
    string currentJointName;
    public float pullForce;
    [SerializeField]
    private KeyCode k_traverseForward, k_traverseBackwards, k_holdOntoRope, k_pullRope;
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
        if (Input.GetKey(k_traverseForward))
        {
            TraverseForward();
        }
        else if (Input.GetKey(k_traverseBackwards))
        {
            TraverseBackward();
        }
        if (Input.GetKey(k_pullRope))
        {
            PullRope();
        }
        if(Input.GetKey(k_holdOntoRope))
        {
            HoldOntoRope();
        }
       
	}
    void OnTriggerEnter(Collider colli)
    {
        if (colli.gameObject.layer == ropeLayer)
        {
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
        else if (currentJoint == null)
        {
            int jointNumber = int.Parse(StringManip(newJoint));
            jointParent = newJoint.transform.parent.gameObject;
            currentJoint = newJoint;
            nextJoint = jointParent.transform.FindChild("Joint_" + (jointNumber + 1).ToString());
            previousJoint = jointParent.transform.FindChild("Joint_" + (jointNumber - 1).ToString());
        }
    }
    void CheckIfAtEndOfRope()
    {
        if (previousJoint == null)
        {

        }
        if (nextJoint == null)
        {

        }
        if (currentJoint == null)
        {

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
         //print(name);
         return name;
    }
    void TraverseForward(){
        if (nextJoint != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, nextJoint.position, climbingSpeed);//get axis later, +=movement
        }
    }
    void TraverseBackward(){
        if (previousJoint != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, previousJoint.position, climbingSpeed);
        }
        
    }
    void PullRope()
    {
        currentJoint.GetComponent<Rigidbody>().AddForce(-transform.forward*pullForce); //get players backwards
    }
    void HoldOntoRope()
    {
        this.transform.position = currentJoint.transform.position;
        this.transform.rotation = currentJoint.transform.rotation;
    }
    void JumpOffRope()
    {
        //let go somehow or add normal script and remove this script?
    }
}
