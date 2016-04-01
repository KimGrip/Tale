using UnityEngine;
using System.Collections;

public class AttackState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    [HideInInspector]public IAttackStates currentAttackState;
    [HideInInspector]public AttackWindUp windUpAttackState;
    [HideInInspector]public AttackOnGoing ongoingAttackState;
    [HideInInspector]public AttackDownTime downtimeAttackState;

    //I have been making a multiplayer brawler, and I go for a variant of solution 2. 
    //What I do is for every weapon, have a set of empty transforms which define a simplified 'edge' for 
    //the weapon, and then while it is being swung, I remember the difference between the positions of each empty and their
    //positions last frame, and use Physics.Linecast to find out if anything got hit in between. This provides a very accurate 
    //measure of where the weapon is going, because it doesn't rely on an object not 'phasing' through because of the animation. The only thing to check here, is to make sure that the weapon
    //doesn't hit something twice in the same swing- maybe when it hits an enemy, make the enemy stop receiving damage for half a second or so.
    public void Start()
    {
        Debug.Log("asfas");
        windUpAttackState = new AttackWindUp(enemy, this);
        ongoingAttackState = new AttackOnGoing(enemy, this);
        downtimeAttackState = new AttackDownTime(enemy, this);
        currentAttackState = windUpAttackState;
    }
    public AttackState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    public void UpdateState()
    {
        currentAttackState.UpdateState();
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
