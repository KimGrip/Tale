using UnityEngine;
using System.Collections;

public class Scr_arrow_direction : MonoBehaviour {

    //public Transform weapon;

    //// Update is called once per frame
    //void Update()
    //{

    //    transform.position = weapon.TransformPoint(Vector3.zero);
    //    transform.forward = weapon.TransformDirection(Vector3.forward);
    //    Debug.Log("Test");
    //}

    //void OnParticleCollision(GameObject other)
    //{
    //    if (other.gameObject.GetComponent<Rigidbody>())
    //    {
    //        Vector3 direction = other.transform.position - transform.position;
    //        direction = direction.normalized;

    //        other.GetComponent<Rigidbody>().AddForce(direction * 50);
    //    }
    //}

    private Transform weapon;
    private Rigidbody m_rb;
   // public float arrowSpeed;
   // public Vector3 m_arrowVelocity;

    public LayerMask m_layer;
    // Update is called once per frame
    void Start()
    {
        //  m_rb = this.GetComponent<Rigidbody>();
        weapon = GameObject.FindGameObjectWithTag("arrowSpawnPoint").GetComponent<Transform>();

    }
    void Update()
    {

        transform.forward = weapon.transform.TransformDirection(Vector3.forward);
        transform.position += weapon.TransformPoint(Vector3.forward);

        // m_rb.velocity = m_arrowVelocity * arrowSpeed;

    }
    void OnTriggerEnter(Collider colli)
    {
        Destroy(this.gameObject);
    }
}
