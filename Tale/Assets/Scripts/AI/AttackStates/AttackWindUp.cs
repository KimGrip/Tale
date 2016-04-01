using UnityEngine;
using System.Collections;

public class AttackWindUp : IAttackStates {
    private readonly StatePatternEnemy enemy;
    private readonly AttackState attackState;
     public AttackWindUp(StatePatternEnemy statePatternEnemy,AttackState p_attackState)
    {
        enemy = statePatternEnemy;
        attackState = p_attackState;
    }
    public void UpdateState()
    {
        Debug.Log("YO IM TRYING TO ATTACK WHOHOO");
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
