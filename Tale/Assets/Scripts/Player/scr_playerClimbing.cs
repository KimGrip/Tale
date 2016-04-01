using UnityEngine;
using System.Collections;
[RequireComponent(typeof(scr_PSM))]
public class scr_playerClimbing : MonoBehaviour {

	// Use this for initialization
    public float climbingSpeed;
    public int ropeLayer;
    private GameObject jointParent;
    public Transform previousJoint, currentJoint, nextJoint;
    scr_PSM m_PSM;
    string currentJointName;
    public float pullForce;
    public ForceMode pullPushForceMode;
    [SerializeField]
    private KeyCode k_traverseForward, k_traverseBackwards, k_holdOntoRope, k_pullRope, k_pushRope;

	void Start () {
        ropeLayer = LayerMask.NameToLayer("RopeLayer");
        m_PSM = GetComponent<scr_PSM>();
	}
	
	// Update is called once per frame
	void Update () {
       /* switch (m_ropeState)
        {
            case ropeState.ropestate_climbing:
                break;
            case ropeState.ropestate_skimming:
                break;
            case ropeState.ropestate_swinging:
                break;
        }*/
        if (Input.GetKey(k_traverseForward))
        {
            TraverseForward();
            m_PSM.SetPlayerPose(scr_PSM.PlayerPose.pose_climbing);

        }
        else if (Input.GetKey(k_traverseBackwards))
        {
            TraverseBackward();
            m_PSM.SetPlayerPose(scr_PSM.PlayerPose.pose_climbing);
        }
        if (Input.GetKey(k_pullRope))
        {
            PullRope();
            m_PSM.SetRopeState(scr_PSM.RopeState.ropestate_pulling);
        }
        if (Input.GetKey(k_pushRope))
        {
            PushRope();
        }
        if(Input.GetKey(k_holdOntoRope))
        {
            HoldOntoRope();
            m_PSM.SetPlayerPose(scr_PSM.PlayerPose.pose_climbing);
            m_PSM.SetRopeState(scr_PSM.RopeState.ropestate_hanging);
        }
        else if (Input.GetKeyUp(k_holdOntoRope))
        {
            m_PSM.SetRopeState(scr_PSM.RopeState.ropestate_none);
        }
	}
    void OnTriggerStay(Collider colli)
    {
        if (colli.gameObject.layer == ropeLayer)
        {
            UpdateJoints(colli.transform);
            /*switch (m_playerState)
            {
                case playerstate.state_airborne:
                    HandleRopeCollisionAirBorne();
                    break;
                case playerstate.state_grounded:
                    HandleRopeCollisionGrounded();
                    break;
            }*/
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
            this.transform.position = Vector3.MoveTowards(this.transform.position, nextJoint.position, climbingSpeed*Time.deltaTime);//get axis later, +=movement
        }
    }
    void TraverseBackward(){
        if (previousJoint != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, previousJoint.position, climbingSpeed*Time.deltaTime);
        }
        
    }
    void PullRope()
    {
        currentJoint.GetComponent<Rigidbody>().AddForce(-Vector3.up * pullForce,pullPushForceMode); //get players backwards
    }
    void PushRope()
    {
        currentJoint.GetComponent<Rigidbody>().AddForce(Vector3.up * pullForce, pullPushForceMode);
    }
    void HoldOntoRope()
    {
        this.transform.position = currentJoint.transform.position;
        //this.transform.rotation = currentJoint.transform.rotation;
    }

    void JumpOffRope()
    {
        //let go somehow or add normal script and remove this script?
    }
}
