using UnityEngine;
using System.Collections;

public class scr_projectileMovement : MonoBehaviour {

	// Use this for initialization
    Rigidbody m_rgd;
    Collider m_collider;
    public LayerMask obstacleLayer;
    public LayerMask vurnerableLayer;
    [SerializeField]
    private float arrowStuckDuration;

	void Start () {
        m_collider = GetComponent<Collider>();
        m_rgd = GetComponent<Rigidbody>();
	}
    public void AddVelocity(Vector3 dir,float velocity)
    {
        m_rgd.AddForce(dir*velocity);
    }
    public void OnProjectileSpawn()
    {
        m_rgd = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision colli)
    {
        if ((obstacleLayer.value & 1 << colli.gameObject.layer) == 1 << colli.gameObject.layer)
        {
            Destroy(this.gameObject);
        }
        else if ((vurnerableLayer.value & 1 << colli.gameObject.layer) == 1 << colli.gameObject.layer){
            MakeArrowIntoProp(colli.transform);
            StartCoroutine(DestroyProjectile(arrowStuckDuration));
        }
    }
    void MakeArrowIntoProp(Transform p_targetParent)
    {
        this.transform.parent = p_targetParent;
        m_rgd.velocity = new Vector3(0, 0, 0);
        Destroy(m_rgd);
        Destroy(m_collider);
    }
    IEnumerator DestroyProjectile(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);

    }
}
