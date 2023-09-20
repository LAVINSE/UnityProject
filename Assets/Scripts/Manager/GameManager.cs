using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region 변수
    [SerializeField] private List<GameObject> StageUIList = new List<GameObject>();
    [SerializeField] private List<GameObject> StageObjectList = new List<GameObject>();
    #endregion // 변수


    #region 프로퍼티
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager { get; private set; } = null;
    public bool IsGameStart { get; set; } = false;
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
        
    }

    /** 초기화 >> 상태를 갱신한다 */
    private void Update()
    {
        if (IsGameStart == true)
        {
            // 게임을 시작한다
            StartGame();
            IsGameStart = false;
        }

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

    /** 게임 스테이지UI를 활성화 한다 */
    public void ActiveStageUI(string StageUIName)
    {
        for(int i=0; i< StageUIList.Count; i++)
        {
            if (StageUIList[i].gameObject.name == StageUIName)
            {
                StageUIList[i].SetActive(true);
            }
        }
    }

    /** 게임 스테이지Object를 활성화 한다 */
    public void ActiveStageObject(string StageObjectName)
    {
        for (int i = 0; i < StageObjectList.Count; i++)
        {
            if (StageObjectList[i].gameObject.name == StageObjectName)
            {
                StageObjectList[i].SetActive(true);
            }
        }
    }
    #endregion // 함수
}
