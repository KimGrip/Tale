using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState{
    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Patrol();
    }
    public void OnTriggerEnter(Collider colli)
    {
        if (colli.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }
    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }
    public void ToPatrolState()
    {
        Debug.Log("Unable to go to same state(AI)");
    }
    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }
    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
    }
    public void ToRetreatState()
    {
        enemy.currentState = enemy.retreatState;
    }
    private void Look()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange)&& hit.transform.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }
    private void Patrol()
    {
        enemy.meshRendererFlag.material.color = Color.green;
        enemy.navMeshAgent.destination = enemy.waypoints[nextWayPoint].position;
        enemy.navMeshAgent.Resume();

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.waypoints.Length; //goes back to zero when gone to last
        }
    }
}
