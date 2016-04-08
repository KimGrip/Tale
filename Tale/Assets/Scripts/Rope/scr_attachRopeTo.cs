using UnityEngine;
using System.Collections;

public class scr_attachRopeTo : MonoBehaviour
{

    // Use this for initialization
    Transform attachmentPoint;
    Collider2D m_collider;
    Vector2 velocity;
    Vector2 position;
    Vector2 acceleration;
    public LayerMask hitLayer;
    public float maxVelocity;
    public float minVelocity;
    public Transform tetherObject;
    public bool amITethered;
    public float gravityLimit;
    Vector3 prevPosition;
    Vector3 prevVelocity;
    Vector3 newVelocity;
    public float reelInMultiplier;
    public bool autoShortenRope;
    public bool triesToReachDesired;
    public float desiredLength;
    public float maxTetherLength;
    public float tetherLength;
    void Start()
    {
        m_collider = GetComponent<Collider2D>();
        position = this.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        VariousInputs();
        NewVelocity();
        //this.transform.Translate(velocity);
        this.transform.Translate(prevVelocity);
           TetherRestriction();
           PrevVelocity();
           PrevPosition();
           if (autoShortenRope)
           {
               ShrinkRope();
           }
           if (triesToReachDesired)
           {
               ResizeTowardsDesired();
           }
           //if (newVelocity.y > gravityLimit)
           //{
           //    newVelocity.y = gravityLimit;
           //}
        //CheckCollision();
    }
    void UpdateMovement()
    {
        //velocity = velocity + acceleration * Time.deltaTime;
        //velocity = VelocityMaintainer();
        //position = position + velocity * Time.deltaTime;
        //this.transform.position = position;

    }
    void ResizeTowardsDesired()
    {
        if (desiredLength < tetherLength)
        {
            tetherLength -= Time.deltaTime;
        }
        else if (desiredLength > tetherLength)
        {
            tetherLength += Time.deltaTime;
        }
    }
    void ShrinkRope(){
        tetherLength =Vector2.Distance(this.transform.position,tetherObject.transform.position);
    }
    void FlipForces()
    {
        Vector2 newVel=new Vector2(-GetComponent<Rigidbody2D>().velocity.x,-GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity =VelocityMaintainer( newVel) ;
        
    }
   
    void TetherRestriction()
    {
        if (amITethered)
        {
            if (Vector3.Distance(this.transform.position, tetherObject.transform.position) > tetherLength)
            {
                // we're past the end of our rope
                // pull the avatar back in.
               
                this.transform.position =tetherObject.transform.position+ (this.transform.position - tetherObject.transform.position).normalized * tetherLength;
                //FlipForces();
            }//if vel.y<0 when teather restriction mean reset gravity?
        }
    }
    void PrevVelocity()
    {
        prevVelocity=VelocityMaintainer(this.transform.position- prevPosition);
    }
    void PrevPosition()
    {
        prevPosition = this.transform.position;
    }
    void NewVelocity()
    {
        newVelocity=VelocityMaintainer( prevPosition-this.transform.position);
    }
    void VariousInputs()
    {
        if(Input.GetKey(KeyCode.H)){
            if (tetherLength > 0)
            {
                tetherLength -= Time.deltaTime*reelInMultiplier;
            }
        }
        if (Input.GetKey(KeyCode.G))
        {
            if (tetherLength < maxTetherLength)
            {
                tetherLength += Time.deltaTime*reelInMultiplier;
            }
        }
    }
    //void OnCollisionEnter2D(Collision2D colli)
    //{
    //    Vector2 testPosition = position + velocity * Time.deltaTime;
    //    RaycastHit2D colHit = Physics2D.Raycast(position, testPosition, hitLayer);
    //    if (colHit)
    //    {

    //        // we went through a wall, let's pull the character back,
    //        // using the normal of the wall
    //        // with a 1-unit space for breathing room
    //        testPosition = colHit.point + colHit.normal;

    //        velocity = (testPosition - position) / Time.deltaTime;//not sure where the fuck to do this
    //        position = testPosition;
    //    }
    //}

    void GetInput()
    {
        acceleration.x = Input.GetAxis("Horizontal");
        acceleration.y = Input.GetAxis("Vertical");
    }
    Vector2 VelocityMaintainer(Vector2 p_velocity)
    {
        //check max
        Vector2 newVelocity = p_velocity;
        if (p_velocity.x > maxVelocity)
        {
            newVelocity = new Vector2(maxVelocity, newVelocity.y);
        }
        if (p_velocity.y > maxVelocity)
        {
            newVelocity = new Vector2(newVelocity.x, maxVelocity);
        }
        //check min
        if (p_velocity.x < minVelocity)
        {
            newVelocity = new Vector2(minVelocity, newVelocity.y);
        }
        if (p_velocity.y < minVelocity)
        {
            newVelocity = new Vector2(newVelocity.x, minVelocity);
        }
       // Debug.Log(newVelocity);
        return newVelocity;
    }
    void OnCollisionEnter2D(Collision2D colli)
    {
        if (colli.gameObject.CompareTag("Obstacle"))
        {
           
          //  FlipForces();
        }
        
    }
}
