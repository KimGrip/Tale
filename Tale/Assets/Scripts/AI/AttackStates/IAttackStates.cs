using UnityEngine;
using System.Collections;

public interface IAttackStates{
    void UpdateState();
    void ToAttackWindUp();
    void ToAttackActive();
    void ToAttackDownTime();
}
