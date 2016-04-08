using UnityEngine;
using System.Collections;

public class scr_Player_Pulling : MonoBehaviour {

    public GameObject currentlyAttachedObject;
    // Use this for initialization
    private float attachedMass; //should alter pulling speed
    private bool isAttached;
    public LineRenderer m_line;
    float distanceFromPlayer;
    public float closestPullRange;//move to player //should be changed depending of size of object
    public float pushForce;
    public LayerMask pushableLayer;
	// Use this for initialization
	void Start () {
        m_line.GetComponent<LineRenderer>();
        if (pushForce == 0)
        {
            Debug.Log("Note, pushforce is 0");
        }
	}
	
	// Update is called once per frame
    void Update () {
    //    AtAttach();
    //    PullRope(-GetDirectionTo(currentlyAttachedObject.transform),pushForce);
    //    //Push(GetDirectionToAttached(), pushForce);
    //    UpdateLineRenderer();
	}
    void AtAttach()
    {
        closestPullRange = currentlyAttachedObject.transform.GetComponent<BoxCollider>().bounds.size.x;
        //mass=currentlyAttachedObject.GetComponentmasscxrips <<TODO>>
    }
    public void PullRope(Vector3 p_dir, float pullforce)
    {
        if (distanceFromPlayer >= closestPullRange)
        {
           currentlyAttachedObject.transform.Translate((p_dir * pullforce) * Time.deltaTime, Space.World);
        }
    }
    public void Push(Transform target,Vector3 p_dir, float pushforce)
    {
        target.transform.Translate((p_dir * pushforce) * Time.deltaTime, Space.World);
    }
    Vector3 GetDirectionTo(Transform target)
    {
        Vector3 heading = target.transform.position - this.transform.position;
        distanceFromPlayer = Vector3.Distance(this.transform.position, target.transform.position);
        Vector3 directionToAttached = heading / distanceFromPlayer;
        return directionToAttached;
    }
    void OnCollisionStay(Collision colli)
    {
        //add some input cooldown or smth   
          if((pushableLayer.value & 1<<colli.gameObject.layer) == 1<<colli.gameObject.layer){
            Push(colli.transform, GetDirectionTo(colli.transform), pushForce);
            print("Pushing");
           
        }
    }
    void UpdateLineRenderer()
    {
        m_line.SetPosition(0, this.transform.position);
        m_line.SetPosition(1, currentlyAttachedObject.transform.position);
    }
}
