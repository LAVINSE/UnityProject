using EnemyOwnedState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum eEnemyState
    {
        WAIT = 0,
        ATTACK,
        DIE,
    }

    private State[] States;
    private State CurrentState;

    private void Awake()
    {
        States = new State[3];
        States[(int)eEnemyState.WAIT] = new EnemyWait();
        States[(int)eEnemyState.ATTACK] = new EnemyAttack();

        EnemyChangeState(eEnemyState.WAIT);
        CurrentState = States[(int)eEnemyState.WAIT];
    }

    private void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.EnemyStateUpdate(this);
        }
    }

    public void EnemyChangeState(eEnemyState NewState)
    {
        // 새로 바꾸려는 상태가 비어있으면 상태를 바꾸지 않는다
        if (States[(int)NewState] == null) return;

        // 현재 재생중인 상태가 있으면 Exit() 함수 호출
        if(CurrentState != null)
        {
            CurrentState.EnemyStateExit(this);
        }

        // 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 함수 호출
        CurrentState = States[(int)NewState];
        CurrentState.EnemyStateEnter(this);
    }
}

/** 적 대기 클래스 */
public class EnemyWait : State
{
    /** 적 상태 시작 */
    public override void EnemyStateEnter(EnemyState eState)
    {
        Debug.Log("대기 시작");
    }

    /** 적 상태 갱신 */
    public override void EnemyStateUpdate(EnemyState eState)
    {
        Debug.Log("대기 갱신");
        eState.EnemyChangeState(EnemyState.eEnemyState.ATTACK);
    }

    /** 적 상태 종료 */
    public override void EnemyStateExit(EnemyState eState)
    {
        Debug.Log("대기 종료");
    }
}

/** 적 공격 클래스 */
public class EnemyAttack : State
{
    public override void EnemyStateEnter(EnemyState eState)
    {
        Debug.Log("공격 시작");
    }

    public override void EnemyStateUpdate(EnemyState eState)
    {
        Debug.Log("공격 갱신");
        eState.EnemyChangeState(EnemyState.eEnemyState.ATTACK);
    }

    public override void EnemyStateExit(EnemyState eState)
    {
        Debug.Log("공격 종료");
    }
}

/** 적 죽음 클래스 */
public class EnemyDie : State
{
    public override void EnemyStateEnter(EnemyState eState)
    {
        Debug.Log("죽음 시작");
    }

    public override void EnemyStateUpdate(EnemyState eState)
    {
        Debug.Log("죽음 갱신");
        eState.EnemyChangeState(EnemyState.eEnemyState.WAIT);
    }

    public override void EnemyStateExit(EnemyState eState)
    {
        Debug.Log("죽음 종료");
    }
}