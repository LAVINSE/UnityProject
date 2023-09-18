using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region 변수
    #endregion // 변수


    #region 프로퍼티
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager { get; private set; } = null;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        Instance = this;
        PoolManager = CFactory.CreateObject<ObjectPoolManager>("ObjectPoolManager", this.gameObject,
            Vector3.zero, Vector3.one, Vector3.zero);
    }

    /** 초기화 */
    private void Start()
    {
        // 게임을 시작한다
        StartGame();
    }

    /** 초기화 >> 상태를 갱신한다 */
    private void Update()
    {
        // 에디터에서 사용가능한 치트키
#if UNITY_EDITOR
        InputCheatKey();
#endif // UNITY_EDITOR
    }

    /** 치트키 키 입력 */
    private void InputCheatKey()
    {
        // 앞면 카드 추가
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnManager.IsOnAddCard?.Invoke(true);
        }

        // 뒷면 카드 추가
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnManager.IsOnAddCard?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnManager.Instacne.NextTurn();
        }
    }

    /** 게임을 시작한다 */
    public void StartGame()
    {
        StartCoroutine(TurnManager.Instacne.StartGameCo());
    }
    #endregion // 함수
}
