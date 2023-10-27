using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : CSingleton<GameManager>
{
    [System.Serializable]
    public struct StageInfo
    {
        public enum EnemyType
        {
            // NONE 사용 X
            NONE = -1,

            NORMAL,
            ELITE,
            BOSS,
        }

        public string StageInfoTitleName;
        public Sprite StageInfoImg;
        public EnemyType StageEnemyType;
    }

    #region 변수
    [Header("=====> 정보 <=====")]
    [SerializeField] private string PlayerName = string.Empty;

    [Header("=====> 인스펙터 확인용 <=====")]
    [SerializeField] private StageInfo.EnemyType StageEnemyType = StageInfo.EnemyType.NONE;

    [Header("=====> 플레이어 카드 덱 <=====")]
    [SerializeField] private List<CardScirptTable> CardBasicTableDeck = new List<CardScirptTable>();
    #endregion // 변수

    #region 프로퍼티
    public ObjectPoolManager PoolManager { get; private set; } = null;
    public bool IsGameStart { get; set; } = false;
    public bool IsChangeName { get; set; } = false;

    public string oPlayerName
    {
        get => PlayerName;
        set => PlayerName = value;
    }

    public StageInfo.EnemyType oStageEnemyType
    {
        get => StageEnemyType;
        set => StageEnemyType = value;
    }
    public List<CardScirptTable> oCardBasicTableDeck
    {
        get => CardBasicTableDeck;
        set => CardBasicTableDeck = value;
    }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        PlayerName = PlayerPrefs.GetString("CurrentPlayerName");
        PoolManager = CFactory.CreateObject<ObjectPoolManager>("ObjectPoolManager", this.gameObject,
            Vector3.zero, Vector3.one, Vector3.zero);
    }

    /** 초기화 >> 상태를 갱신한다 */
    private void Update()
    {
        // 치트키
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
            TurnManager.Instance.NextTurn();
        }
    }

    /** 게임을 시작한다 */
    public void StartGame(StageInfo.EnemyType StageEnemyTypeInfo)
    {
        StartCoroutine(TurnManager.Instance.StartGameCo(StageEnemyTypeInfo));
    }

    /** 씬을 이동한다 */
    public void ChangeScene(string SceneName)
    {
        LoadingScene.LoadScene(SceneName);
    }

    /** 플레이어 이름을 저장한다 */
    public void PlayerNameSave()
    {
        PlayerName = PlayerPrefs.GetString("CurrentPlayerName");
    }
    #endregion // 함수
}
