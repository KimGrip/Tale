using UnityEngine;
using System.Collections;

public class AlertStateRanged : IEnemyState
{
  private readonly StatePatternEnemyRanged enemy;
  private float searchTimer;
    public void Start()
   {

   }
    public AlertStateRanged(StatePatternEnemyRanged statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }
    public void OnTriggerEnter(Collider colli)
    {

    }
    public void ToAlertState()
    {
        Debug.Log("Unable to go to same state(AI)");
    }
    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
        searchTimer = 0f;
    }
    public void ToChaseState()
    {
        enemy.currentState = enemy.detectedState;
        searchTimer = 0f;
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
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.transform.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }
    private void Search()
    {
        enemy.meshRendererFlag.material.color = Color.yellow;
        enemy.navMeshAgent.Stop();
        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime,0); //rotates on this points and it stopped above.
        searchTimer += Time.deltaTime;
        if (searchTimer >= enemy.searchDuration)
        {
            ToPatrolState();
        }
    }
    public void OnTriggerStay(Collider colli)
    {

    }
}
