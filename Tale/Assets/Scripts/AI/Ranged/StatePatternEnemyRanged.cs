using UnityEngine;
using System.Collections;

public class StatePatternEnemyRanged : MonoBehaviour
{

    // Use this for initialization
    public float searchTurnSpeed = 240.0f;
    public float searchDuration = 4.0f;
    public Transform[] waypoints;
    public Transform eyes;//raypoint start
    public float sightRange = 20f;
    public Vector3 offset = new Vector3(0, 0.5f, 0); //lookoffset
    public MeshRenderer meshRendererFlag;

    public float attackRange = 4.0f;
    public float attackWindUpDuration = 0.4f;
    public float attackDuration = 1.0f;
    public float attackDownDuration = 1.4f;

    public Transform swingSimulationPoint;
    public Transform[] weaponWayPoints;
    public LayerMask playerLayer;
    public bool isReloaded;
    public float comfortZoneRange;
    public float FOV_angle;
    public SphereCollider m_sphereCol;
    [HideInInspector]public float distanceFromPlayer;
    [HideInInspector] public float retreatLength;
    public GameObject projectilePrefab;
    [HideInInspector]
    public LineRenderer weaponSwingRenderer;

    public bool drawGizmos;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public IEnemyState currentState;
    [HideInInspector]
    public AlertStateRanged alertState;
    [HideInInspector]
    public DetectedStateRanged detectedState;
    [HideInInspector]
    public PatrolStateRanged patrolState;
    [HideInInspector]
    public AttackStateRanged attackState;
    [HideInInspector]
    public RetreatStateRanged retreatState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    private void Awake()
    {
        detectedState = new DetectedStateRanged(this);
        alertState = new AlertStateRanged(this);
        patrolState = new PatrolStateRanged(this);
        attackState = new AttackStateRanged(this);
        retreatState = new RetreatStateRanged(this);

        attackState.Start(); // note to self call start on states that it is needed
        m_sphereCol = GetComponent<SphereCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        weaponSwingRenderer = GetComponent<LineRenderer>();
    }

    public bool IsPlayerInsideComfortZone()
    {
        if (Vector3.Distance(transform.position, chaseTarget.position) < comfortZoneRange)
        {
            return true;
        }
        return false;
    }
    public Vector3 GetDirectionTo(Transform target)
    {
        Vector3 heading = target.transform.position - this.transform.position;
        distanceFromPlayer = Vector3.Distance(this.transform.position, target.transform.position);
        Vector3 directionToTarget = heading / distanceFromPlayer;
        return directionToTarget;
    }
    public void RotateToward(){
         Vector3 targetDir = chaseTarget.transform.position - transform.position;
        float step = 30 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    public void SetReloaded(bool trueOrFalse)
    {
        isReloaded = trueOrFalse;
    }
    void Start()
    {
        currentState = patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }
    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.blue;
            if (waypoints.Length > 0)
            {
                for (int i = 0; i < waypoints.Length; i++)
                {
                    Gizmos.DrawCube(waypoints[i].position, new Vector3(1, 1, 1));
                }
            }
            Gizmos.color = Color.red;
            if (weaponWayPoints.Length > 0)
            {
                for (int i = 0; i < weaponWayPoints.Length - 1; i++)
                {
                    Gizmos.DrawCube(weaponWayPoints[i].position, new Vector3(0.5f, 0.5f, 0.5f));
                    if (weaponWayPoints.Length > 1)
                    {
                        if (i < weaponWayPoints.Length + 1)
                        {
                            Gizmos.DrawLine(weaponWayPoints[i].position, weaponWayPoints[i + 1].position);
                        }
                    }
                }
            }
            Gizmos.color = Color.grey;
            Gizmos.DrawSphere(this.transform.position, comfortZoneRange);
        }
    }
    public GameObject SpawnProjectile()
    {
        GameObject newProj= Instantiate(projectilePrefab) as GameObject;
        return newProj;
    }
    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
        
    }
}
