using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

	// Use this for initialization
    public float searchTurnSpeed = 120.0f;
    public float searchDuration = 4.0f;
    public Transform[] waypoints;
    public Transform eyes;//raypoint start
    public float sightRange = 20f;
    public Vector3 offset = new Vector3(0, 0.5f, 0); //lookoffset
    public MeshRenderer meshRendererFlag;

    public float attackRange = 4.0f;
    public float attackWindUp = 0.4f;
    public float attackDuration = 1.0f;
    public float attackDownTime = 1.4f;

    [HideInInspector]public Transform chaseTarget;
    [HideInInspector]public IEnemyState currentState;
    [HideInInspector]public AlertState alertState;
    [HideInInspector]public ChaseState chaseState;
    [HideInInspector]public PatrolState patrolState;
    [HideInInspector]public AttackState attackState;
    [HideInInspector]public RetreatState retreatState;
    [HideInInspector]public NavMeshAgent navMeshAgent;
    private void Awake()
    {
        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);
        attackState = new AttackState(this);
        retreatState = new RetreatState(this);

        attackState.Start(); // note to self call start on states that it is needed
        
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
	void Start () {
        currentState = patrolState;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
	}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (waypoints.Length > 0)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawCube(waypoints[i].position, new Vector3(1, 1, 1));
            }
        }  
    }
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
