using UnityEngine;
using System.Collections;

public class scr_attachRopeTo : MonoBehaviour
{

    // Use this for initialization
    Transform attachmentPoint;
    Collider m_collider;
    Vector3 velocity;
    Vector3 position;
    Vector3 acceleration;
    public float maxVelocity;
    public float minVelocity;
    public Transform tetherObject;
    public bool amITethered;
    Vector3 prevPosition;
    Vector3 prevVelocity;
    Vector3 newVelocity;
    Transform thisOffsetTransform;
    public float speedMultiplier;
    public float reelInMultiplier;
    public bool autoShortenRope;
    public bool triesToReachDesired;
    public float desiredLength;
    public float maxTetherLength;
    public float tetherLength;
    [SerializeField]
    private float swingSpeed;
    public float downwardAcc;
     LineRenderer m_lineRenderer;
     Rigidbody m_rgd;
     public float vInput=0;
     public float hInput=0;
    [SerializeField]
     private float playerHeigthTetherOffset;

    void Start()
    {
        m_collider = GetComponent<Collider>();
        position = this.transform.position;
        m_lineRenderer = GetComponent<LineRenderer>();
        m_rgd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        VariousInputs();
        if (amITethered)
        {
            UpdateLineRenderer();
        }
    }
    void FixedUpdate()
    {
        if (amITethered)
        {
            HandleMoveInput(vInput, hInput);
          
        }
        NewVelocity();
        if (amITethered)
        {
            this.transform.Translate(prevVelocity * speedMultiplier * Time.deltaTime,Space.World);
            m_rgd.velocity = prevVelocity * speedMultiplier * Time.deltaTime;
            TetherRestriction();
        }
        PrevVelocity();
        PrevPosition();
        if (amITethered)
        {
           
            if (autoShortenRope)
            {
                ShrinkRope();
            }
            if (triesToReachDesired)
            {
                ResizeTowardsDesired();
            }
        }
        //CheckCollision();
    }
    public void SetAttachTarget(Transform p_transform)
    {
        tetherLength = Vector3.Distance(this.transform.position, p_transform.position) - playerHeigthTetherOffset; //add a little length so it doesnt restrict immideatly // probably - tether if on ground so that it doesnt hit the ground. 
        attachmentPoint = p_transform;
        
    }
    public void SetTeatherObject(Transform p_transform)
    {
        tetherLength = Vector3.Distance(this.transform.position, p_transform.position) -playerHeigthTetherOffset;
        tetherObject = p_transform;
        
    } 
    void ResizeTowardsDesired()
    {
        if (desiredLength < tetherLength)
        {
            tetherLength -= Time.deltaTime*reelInMultiplier;
        }
        else if (desiredLength > tetherLength)
        {
            tetherLength += Time.deltaTime*reelInMultiplier;
        }
    }
    public void SetAmITethered(bool trueOrFalse)
    {
        amITethered = trueOrFalse;
    }
    void ShrinkRope(){
        tetherLength =Vector3.Distance(this.transform.position,tetherObject.transform.position);
    }
    void FlipForces()
    {
        Vector3 newVel=new Vector3(-GetComponent<Rigidbody>().velocity.x,-GetComponent<Rigidbody>().velocity.y, -GetComponent<Rigidbody>().velocity.z);
        GetComponent<Rigidbody>().velocity =VelocityMaintainer( newVel) ;
        
    }
   
    void TetherRestriction()
    {
        if (amITethered)
        {
            if (Vector3.Distance(this.transform.position, tetherObject.transform.position) > tetherLength)
            {
                // we're past the end of our rope
                // pull the avatar back in.
                //this.transform.Translate(prevVelocity); 
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
    void UpdateLineRenderer()
    {
        if (amITethered)
        {
            m_lineRenderer.SetPosition(0, this.transform.position);
            m_lineRenderer.SetPosition(1, tetherObject.transform.position);
        }
    }
    void VariousInputs()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            amITethered = true;  
            tetherLength= Vector3.Distance(this.transform.position, tetherObject.transform.position);
            ///m_rgd.useGravity = false;
            /// //downwardAcc = -0.01f;
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            amITethered = false;
           // m_rgd.useGravity = true;
           // downwardAcc = 0;
        }
        if (amITethered)
        {
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
           
            
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            amITethered = false;
            print("DEATTACH");
        }
      
        if(Input.GetKey(KeyCode.H)){
            //if (tetherLength > 0)
            //{
            //    tetherLength -= Time.deltaTime*reelInMultiplier;
            //}
            if (desiredLength > 2.0f)
            {
                desiredLength -= Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.G))
        {
            //if (tetherLength < maxTetherLength)
            //{
            //    tetherLength += Time.deltaTime*reelInMultiplier;
            //}
            if (desiredLength < maxTetherLength)
            {
                desiredLength += Time.deltaTime;
            }
        }
    }
    void HandleMoveInput(float vInput, float hInput)
    {
        this.transform.Translate(new Vector3(0, hInput * swingSpeed, vInput * swingSpeed) * Time.deltaTime, Space.World);
        this.transform.Translate(new Vector3(0,downwardAcc,0)*Time.deltaTime,Space.World);
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
    Vector3 VelocityMaintainer(Vector3 p_velocity)
    {
        //check max
        Vector3 newVelocity = p_velocity;
        if (p_velocity.x > maxVelocity)
        {
            newVelocity = new Vector3(maxVelocity, newVelocity.y, newVelocity.z);
        }
        if (p_velocity.y > maxVelocity)
        {
            newVelocity = new Vector3(newVelocity.x, maxVelocity,newVelocity.z);
        }
        if (p_velocity.z > maxVelocity)
        {
            newVelocity = new Vector3(newVelocity.x, newVelocity.y, maxVelocity);
        }
        //check min
        if (p_velocity.x < minVelocity)
        {
            newVelocity = new Vector3(minVelocity, newVelocity.y, newVelocity.z);
        }
        if (p_velocity.y < minVelocity)
        {
            newVelocity = new Vector3(newVelocity.x, minVelocity, newVelocity.z);
        }
        if (p_velocity.z < minVelocity)
        {
            newVelocity = new Vector3(newVelocity.x, newVelocity.y, minVelocity);
        }
       // Debug.Log(newVelocity);
        return newVelocity;
    }
    //void OnCollisionEnter(Collision colli)
    //{
    //    if (colli.gameObject.CompareTag("Obstacle"))
    //    {
           
    //      //  FlipForces();
    //    }
        
    //}
}
