using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private enum ETurnMode
    {
        RandomTurn =0,
        MyTurn,
        OtherTurn,
    }

    #region 변수
    [Header("=====> 개발자 모드 <=====")]
    [SerializeField][Tooltip("시작 턴 모드를 정합니다")] private ETurnMode TurnMode; // 턴 관리하는 열거형 변수
    [SerializeField][Tooltip("시작 카드 개수를 정합니다")] private int StartCardCount = 0; // 시작 카드 개수
    [SerializeField][Tooltip("카드 배분이 매우 빨라집니다")] private bool IsCardFastMode = false; // 카드 배분 패스트 모드

    [Header("=====> 턴 관리자 <=====")]
    [SerializeField] private bool IsMyTurn; // 자신의 턴인지 확인하는 변수
    [SerializeField] private bool IsFrontCard = true; // 앞면 카드, 뒷면 카드 확인하는 변수
    [SerializeField] private bool IsLoading; // 로딩중일때 클릭 못하게 하는 변수

    [Header("=====> 턴 데이터 관리자 <=====")]
    [SerializeField] private PlayerData oPlayerData; // 플레이어 데이터
    [SerializeField] private string MyTurn;
    [SerializeField] private string EnemyTurn;
    [SerializeField] private bool IsPlayerDie = false;
    [SerializeField] private bool IsEnemyDie = false;

    private WaitForSeconds oDelay = new WaitForSeconds(0.5f); // 딜레이 시간
    public static Action<bool> IsOnAddCard; // 카드 뽑는 델리게이트
    #endregion // 변수

    #region 프로퍼티
    public static TurnManager Instance { get; private set; }
    public bool oIsLoading
    {
        get => IsLoading;
        set => IsLoading = value;
    }
     
    public bool oIsMyTurn
    {
        get => IsMyTurn;
        set => IsMyTurn = value;
    }

    public bool oIsPlayerDie
    {
        get => IsPlayerDie;
        set => IsPlayerDie = value;
    }

    public bool oIsEnemyDie
    {
        get => IsEnemyDie;
        set => IsEnemyDie = value;
    }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        Instance = this;
        oIsEnemyDie = false;
        oIsPlayerDie = false;
    }

    /** 초기화 */
    private void Update()
    {
        if (GameManager.Inst.IsGameStart == true)
        {
            // 게임을 시작한다
            StartCoroutine(StartGameCo(GameManager.Inst.oStageEnemyType));
            GameManager.Inst.IsGameStart = false;
        }
    }

    /** 카드 배분을 관리한다 */
    private void GameSetup()
    {
        // 카드 배분 Fast모드가 true이면
        if(IsCardFastMode == true)
        {
            oDelay = new WaitForSeconds(0.05f);
        }

        switch(TurnMode)
        {
            case ETurnMode.RandomTurn:
                // 0 이면 MyTurn
                IsMyTurn = UnityEngine.Random.Range(0, 2) == 0;
                break;
            case ETurnMode.MyTurn:
                IsMyTurn = true;
                break;
            case ETurnMode.OtherTurn:
                IsMyTurn = false;
                break;
        }
    }

    /** 게임 시작을 관리한다 */
    public IEnumerator StartGameCo(GameManager.StageInfo.EnemyType StageEnemyTypeInfo)
    {
        GameSetup();
        IsLoading = true;

        // 카드를 배분한다
        for (int i=0; i< StartCardCount; i++)
        {
            // 앞면 카드
            yield return oDelay;
            IsOnAddCard?.Invoke(IsFrontCard);
        }

        // 배경음 세팅
        StageBGMSetting(StageEnemyTypeInfo);

        // 적 소환이펙트, 적 소환
        StartCoroutine(EnemyManager.Instance.SpawnEnemy(StageEnemyTypeInfo));

        // 카드 배분이 끝나면
        StartCoroutine(StartTurnCo());
    }

    /** 턴을 시작한다 */
    private IEnumerator StartTurnCo()
    {
        IsLoading = true;
        oDelay = new WaitForSeconds(0.7f);
        
        // 내 턴일 경우
        if(IsMyTurn == true)
        {
            CSceneManager.Instance.Notification(MyTurn);

            yield return oDelay;

            oPlayerData.oCurrentCost = oPlayerData.oMaxCost; // 최대 마나랑 같게 설정
            IsOnAddCard?.Invoke(IsFrontCard); // 턴 시작시 앞면 카드 한장 뽑기
        }
        else
        {
            CSceneManager.Instance.Notification(EnemyTurn);
            yield return oDelay;
        }

        yield return oDelay;
        IsLoading = false;
    }

    /** 턴을 넘긴다 */
    public void NextTurn()
    {
        IsMyTurn = !IsMyTurn;
        StartCoroutine(StartTurnCo());
    }

    /** 스테이지 배경음을 세팅한다 */
    private void StageBGMSetting(GameManager.StageInfo.EnemyType StageEnemyTypeInfo)
    {
        switch (StageEnemyTypeInfo)
        {
            case GameManager.StageInfo.EnemyType.NORMAL:
                AudioManager.Inst.PlayBGM(AudioManager.BGMEnum.Battle_BGM_Normal);
                break;
            case GameManager.StageInfo.EnemyType.ELITE:
                AudioManager.Inst.PlayBGM(AudioManager.BGMEnum.Battle_BGM_Elite);
                break;
            case GameManager.StageInfo.EnemyType.BOSS:
                AudioManager.Inst.PlayBGM(AudioManager.BGMEnum.Battle_BGM_Boss);
                break;
        }
    }
    #endregion // 함수
}
