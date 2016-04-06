using UnityEngine;
using System.Collections;

public class Scr_arrow_direction : MonoBehaviour {

  
    private Transform weapon;
    private Transform weaponTF;
    private Rigidbody m_rb;
   // public float arrowSpeed;
   // public Vector3 m_arrowVelocity;

    public LayerMask m_layer;
    private bool DoOnce = false;
    // Update is called once per frame
    void OnEnable()
    {        
        weapon = GameObject.FindGameObjectWithTag("arrowSpawnPoint").GetComponent<Transform>();

        if (DoOnce == false)
        {

            weaponTF = weapon.transform;
            DoOnce = true;
        }
    }
    void Update()
    {


       // transform.forward = weapon.transform.TransformDirection(Vector3.forward);
    //    transform.position += weaponTF.TransformPoint(Vector3.forward) / 4;

        // m_rb.velocity = m_arrowVelocity * arrowSpeed;

    }


    void OnTriggerEnter(Collider colli)
    {
       // Debug.Log("arrow deleted");

        Destroy(this.gameObject);
    }
}
