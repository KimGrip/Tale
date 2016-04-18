using UnityEngine;
using System.Collections;

public class scr_projectileMovement : MonoBehaviour {

	// Use this for initialization
    Rigidbody m_rgd;
    Collider m_collider;
    public LayerMask obstacleLayer;
    public LayerMask vurnerableLayer;
    public LayerMask livingLayer;
    [SerializeField]
    private float arrowStuckDuration;
    public int arrowDamage;
    scr_shooting_rope shooting_rope;
    RaycastHit hit;
    private GameObject projectileOriginator;
    DontGoThroughThings DGTT;
    [SerializeField]
    private GameObject player;
    private scr_attachRopeTo playerAttach;
    public GameObject singletonHandler; 
    scr_playerStateFunctions PSF;
	void Start () 
    {
        m_collider = GetComponent<Collider>();
        m_rgd = GetComponent<Rigidbody>();
        shooting_rope = GetComponent<scr_shooting_rope>();
        DGTT = GetComponent<DontGoThroughThings>();
        singletonHandler = GameObject.FindGameObjectWithTag("SingletonHandler");
        PSF = singletonHandler.GetComponent<scr_playerStateFunctions>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
    void Update()
    {
        if (m_rgd)
        {
            if (m_rgd.velocity.magnitude > 0.1)
            {
                Quaternion dirQ = Quaternion.LookRotation(m_rgd.velocity);
                Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, m_rgd.velocity.magnitude * 3.0f * Time.deltaTime);
                m_rgd.MoveRotation(slerp);
            }
        }
    }
    public void SetProjectileOriginator(GameObject p_originator)
    {
        projectileOriginator = p_originator;
    }
    public void AddVelocity(Vector3 dir,float velocity)
    {
        m_rgd.AddForce(dir*velocity);
    }
    public void OnProjectileSpawn()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttach = player.GetComponent<scr_attachRopeTo>();
        m_rgd = GetComponent<Rigidbody>();
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    void OnTriggerEnter(Collider colli)
    {
        if ((obstacleLayer.value & 1 << colli.gameObject.layer) == 1 << colli.gameObject.layer)
        {
            Destroy(this.gameObject);
        }
        else if ((vurnerableLayer.value & 1 << colli.gameObject.layer) == 1 << colli.gameObject.layer)
        {
            MakeArrowIntoProp(colli.transform);
            //attach to thing make func if bigger
            PSF.SetSwinging(colli);
            //<<
            StartCoroutine(DestroyProjectile(arrowStuckDuration));
        }
        else if ((livingLayer.value & 1 << colli.gameObject.layer) == 1 << colli.gameObject.layer)
        {
            scr_healthManager hitHealth = colli.gameObject.GetComponent<scr_healthManager>();
            hitHealth.DealDamage(arrowDamage);
        }

    }
    void MakeArrowIntoProp(Transform p_targetParent)
    {
        if (shooting_rope == null)
        {
            print("No shooting rope scrpt attached to player");
        }
        else
        {
            shooting_rope.SetTarget(transform);
        }
     
        this.transform.parent = p_targetParent;
        if(m_rgd != null)
        {
            m_rgd.velocity = new Vector3(0, 0, 0);
            Destroy(m_rgd);
        }
        DGTT.enabled = false;
        Destroy(m_collider);
        if (p_targetParent != null)
        {

            shooting_rope.SetTarget(transform);

            this.transform.parent = p_targetParent;
            if (m_rgd != null)
            {
                m_rgd.velocity = new Vector3(0, 0, 0);
                Destroy(m_rgd);
            }

            Destroy(m_collider);
        }
    }
    IEnumerator DestroyProjectile(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);

    }
    void RayCastingCollision()
    {
        Debug.DrawRay(transform.position,  transform.forward);


        if(Physics.Raycast(transform.position, transform.forward, out hit, vurnerableLayer))
        {
            MakeArrowIntoProp(hit.transform);
            StartCoroutine(DestroyProjectile(arrowStuckDuration));
        }

    }
}
