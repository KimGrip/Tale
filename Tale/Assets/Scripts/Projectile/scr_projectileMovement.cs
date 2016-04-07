using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class scr_projectileMovement : MonoBehaviour {

	// Use this for initialization
    Rigidbody m_rgd;
	void Start () {
        
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
        //Destroy(m_rgd); 
    }
	// Update is called once per frame
}
