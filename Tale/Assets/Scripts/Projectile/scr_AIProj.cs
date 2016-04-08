﻿using UnityEngine;
using System.Collections;

public class scr_AIProj : MonoBehaviour
{

    // Use this for initialization
    Rigidbody m_rgd;
    Collider m_collider;
    public LayerMask obstacleLayer;
    public LayerMask vurnerableLayer;
    [SerializeField]
    private float arrowStuckDuration;
    private bool stuck = false;

    RaycastHit hit;
    void Start()
    {
        m_collider = GetComponent<Collider>();
        m_rgd = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //RayCastingCollision();
    }
    public void AddVelocity(Vector3 dir, float velocity)
    {
        m_rgd.AddForce(dir * velocity);
    }
    public void OnProjectileSpawn()
    {
        m_rgd = GetComponent<Rigidbody>();
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public bool GetStuck()
    {
        return stuck;
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
            StartCoroutine(DestroyProjectile(arrowStuckDuration));
        }
    }
    void MakeArrowIntoProp(Transform p_targetParent)
    {
        this.transform.parent = p_targetParent;
        if (m_rgd != null)
        {
            m_rgd.velocity = new Vector3(0, 0, 0);
            Destroy(m_rgd);
        }

        Destroy(m_collider);
        stuck = true;
    }
    IEnumerator DestroyProjectile(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);

    }
    void RayCastingCollision()
    {
        Debug.DrawRay(transform.position, transform.forward);


        if (Physics.Raycast(transform.position, transform.forward, out hit, vurnerableLayer))
        {
            MakeArrowIntoProp(hit.transform);
            StartCoroutine(DestroyProjectile(arrowStuckDuration));
        }

    }
}
