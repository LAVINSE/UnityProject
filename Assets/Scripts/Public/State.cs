using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyOwnedState
{
    public abstract class State
    {
        /** 적 상태 시작 */
        public abstract void EnemyStateEnter(EnemyState eState);
        /** 적 상태 갱신 */
        public abstract void EnemyStateUpdate(EnemyState eState);
        /** 적 상태 종료 */
        public abstract void EnemyStateExit(EnemyState eState);
    }
}
