using UnityEngine;
using System.Collections;

public class AttackDownTime : IAttackStates {

	// Use this for initialization
    private readonly StatePatternEnemy enemy;
    private readonly AttackState attackState;
    public AttackDownTime(StatePatternEnemy statePatternEnemy,AttackState p_attackState)
    {
        enemy = statePatternEnemy;
        attackState = p_attackState;
    }
    public void Start()
    {
	
	}
    public void UpdateState()
    {

    }
    public void ToAttackWindUp()
    {

    }
    public void ToAttackActive()
    {

    }
    public void ToAttackDownTime()
    {

    }
}
