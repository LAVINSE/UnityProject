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

    [Header("=====> 턴 관리자 <=====")]
    [SerializeField] private PlayerData oPlayerData; // 플레이어 데이터
    [SerializeField] private GameObject DropObject;

    private WaitForSeconds oDelay = new WaitForSeconds(0.5f); // 딜레이 시간
    public static Action<bool> IsOnAddCard; // 카드 뽑는 델리게이트
    public static Action<StageInfo.EnemyType> IsSpawnEnemy; // 적 소환 델리게이트
    #endregion // 변수

    #region 프로퍼티
    public bool bIsLoading
    {
        get => IsLoading;
        set => IsLoading = value;
    }
     
    public bool bIsMyTurn
    {
        get => IsMyTurn;
        set => IsMyTurn = value;
    }
    
    public static TurnManager Instacne { get; private set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        Instacne = this;
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
    public IEnumerator StartGameCo(StageInfo.EnemyType StageEnemyTypeInfo)
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

        // TODO : 적 출현 이펙트 추가 예정
        IsSpawnEnemy(StageEnemyTypeInfo);

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
            // TODO : 나의 턴 이라고 직접 작성하지말고 데이터를 불러와서 대입하는 형식으로 코드 수정해야됨
            UIManager.Instance.Notification("나의 턴");

            yield return oDelay;

            oPlayerData.oCurrentCost = oPlayerData.oMaxCost; // 최대 마나랑 같게 설정
            IsOnAddCard?.Invoke(IsFrontCard); // 턴 시작시 앞면 카드 한장 뽑기
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

    /** 턴을 넘기는 버튼을 활성화 한다 */
    public void ClickNextTurnButton()
    {
        if(IsMyTurn == true)
        {
            NextTurn();
        }
    }
    #endregion // 함수
}
