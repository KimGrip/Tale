using UnityEngine;
using System.Collections;

public class AttackState: IEnemyState
{
    private readonly StatePatternEnemy enemy;

    public AttackState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    public void UpdateState()
    {

    }
    public void OnTriggerEnter(Collider colli)
    {

    }
    public void ToAlertState()
    {

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
}
