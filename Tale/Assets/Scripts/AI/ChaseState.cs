using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private float chaseTimer;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    public void UpdateState()
    {
        Look();
        Chase();
    }
    public void OnTriggerEnter(Collider colli)
    {

    }
    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }
    public void ToPatrolState()
    {

    }
    public void ToChaseState()
    {

    }
    public void ToAttackState()
    {

    }
    public void ToRetreatState()
    {

    }
    private void Look()
    {
        RaycastHit hit;
        Vector3 enemyToTarget = (enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position;
        if (Physics.Raycast(enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;

        }
        else
        {
            ToAlertState();
        }
        
    }
    private void Chase()
    {
        enemy.meshRendererFlag.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();
    }
}
