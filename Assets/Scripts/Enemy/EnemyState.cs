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
    }

    #region 변수
    private State[] States;
    private State CurrentState;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        // States가 가질 수 있는 상태 개수만큼 메모리 할당, 각 상태에 클래스 메모리 할당
        States = new State[3];
        States[(int)eEnemyState.WAIT] = new EnemyWaitState();
        States[(int)eEnemyState.ATTACK] = new EnemyAttackState();

        // WAIT 상태로 기본설정
        EnemyChangeState(eEnemyState.WAIT);
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.EnemyStateUpdate(this);
        }
    }

    /** 적 상태를 바꾼다 */
    public void EnemyChangeState(eEnemyState NewState)
    {
        // 새로 바꾸려는 상태가 비어있으면 상태를 바꾸지 않는다
        if (States[(int)NewState] == null) return;

        // 현재 재생중인 상태가 있으면 Exit() 함수 호출
        if (CurrentState != null)
        {
            CurrentState.EnemyStateExit(this);
        }

        // 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 함수 호출
        CurrentState = States[(int)NewState];
        CurrentState.EnemyStateEnter(this);
    }

    #endregion // 함수
}

/** 적 대기 클래스 */
public class EnemyWaitState : State
{
    #region 함수
    /** 적 상태 시작 */
    public override void EnemyStateEnter(EnemyState eState)
    {
        // 적 패턴 선택
        EnemyManager.Instance.EnemySelectPattern();
    }

    /** 적 상태 갱신 */
    public override void EnemyStateUpdate(EnemyState eState)
    {
        // 플레이어가 죽었을 경우
        if(TurnManager.Instance.oIsPlayerDie == true)
        {
            return;
        }

        // 적이 살아있을 경우
        if (TurnManager.Instance.oIsEnemyDie == false)
        {
            // 적 턴일 경우, 로딩중이 아닐 경우
            if (TurnManager.Instance.oIsMyTurn == false && TurnManager.Instance.oIsLoading == false)
            {
                eState.EnemyChangeState(EnemyState.eEnemyState.ATTACK);
            }
        }
        else
        {
            return;
        }
    }

    /** 적 상태 종료 */
    public override void EnemyStateExit(EnemyState eState)
    {
        Debug.Log("대기 종료");
    }
    #endregion // 함수
}

/** 적 공격 클래스 */
public class EnemyAttackState : State
{
    #region 함수
    /** 적 상태 시작 */
    public override void EnemyStateEnter(EnemyState eState)
    {
        Debug.Log("공격 시작");
        EnemyManager.Instance.IsEnemyAttackReady = true;
        // 적 턴일 경우
        if (TurnManager.Instance.oIsMyTurn == false)
        {
            // 적이 공격 준비 상태 일 경우
            if (EnemyManager.Instance.IsEnemyAttackReady == true)
            {
                EnemyManager.Instance.EnemyExecutePattern();
            }
        }
    }

    /** 적 상태 갱신 */
    public override void EnemyStateUpdate(EnemyState eState)
    {
        if(EnemyManager.Instance.IsEnemyAttackReady == false)
        {
            eState.EnemyChangeState(EnemyState.eEnemyState.WAIT);

            if(TurnManager.Instance.oIsPlayerDie == false)
            {
                TurnManager.Instance.NextTurn();
            }
        }
    }

    /** 적 상태 종료 */
    public override void EnemyStateExit(EnemyState eState)
    {
        Debug.Log("공격 종료");
    }
    #endregion // 함수
}